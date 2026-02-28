using LearnDevOps.Data;
using LearnDevOps.Extensions;
using LearnDevOps.Models.Domain;
using LearnDevOps.Models.Session;
using LearnDevOps.Services;
using LearnDevOps.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace LearnDevOps.Controllers;

public class LessonController : Controller
{
    private readonly AppDbContext _db;
    private readonly IProgressService _progress;
    private readonly IGamificationService _gamification;

    public LessonController(AppDbContext db, IProgressService progress, IGamificationService gamification)
    {
        _db = db; _progress = progress; _gamification = gamification;
    }

    // GET /lesson/{id}/start
    public async Task<IActionResult> Start(int id)
    {
        var lesson = await _db.Lessons
            .Include(l => l.Questions)
            .Include(l => l.Unit).ThenInclude(u => u.Track)
            .FirstOrDefaultAsync(l => l.Id == id);

        if (lesson == null) return NotFound();

        var progress = _progress.GetProgress();
        var rec = progress.CompletedLessons.GetValueOrDefault(lesson.Id);

        return View(new LessonStartViewModel
        {
            Lesson = lesson,
            Track = lesson.Unit.Track,
            QuestionCount = lesson.Questions.Count,
            AlreadyCompleted = rec != null,
            PreviousXpEarned = rec?.XpEarned ?? 0
        });
    }

    // POST /lesson/{id}/begin
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Begin(int id)
    {
        var lesson = await _db.Lessons
            .Include(l => l.Questions)
            .Include(l => l.Unit)
            .FirstOrDefaultAsync(l => l.Id == id);

        if (lesson == null) return NotFound();

        var questionIds = lesson.Questions
            .OrderBy(_ => Guid.NewGuid())   // shuffle
            .Select(q => q.Id)
            .ToList();

        var session = new LessonSession
        {
            LessonId = id,
            TrackId = lesson.Unit.TrackId,
            QuestionIds = questionIds,
            CurrentQuestionIndex = 0,
            LivesRemaining = 3,
            MistakeCount = 0,
            XpEarnedThisLesson = 0,
            StartedAt = DateTime.UtcNow
        };

        HttpContext.Session.Set(SessionKeys.LessonSession, session);

        return RedirectToAction(nameof(Play), new { id });
    }

    // GET /lesson/{id}/play
    public async Task<IActionResult> Play(int id)
    {
        var lessonSession = HttpContext.Session.Get<LessonSession>(SessionKeys.LessonSession);
        if (lessonSession == null || lessonSession.LessonId != id)
            return RedirectToAction(nameof(Start), new { id });

        var lesson = await _db.Lessons
            .Include(l => l.Unit).ThenInclude(u => u.Track)
            .FirstOrDefaultAsync(l => l.Id == id);

        if (lesson == null) return NotFound();

        return View(new LessonPlayViewModel
        {
            Lesson = lesson,
            Track = lesson.Unit.Track,
            LessonSession = lessonSession,
            TotalQuestions = lessonSession.QuestionIds.Count
        });
    }

    // GET /lesson/{id}/question  (AJAX)
    [HttpGet]
    [ActionName("question")]
    public async Task<IActionResult> GetQuestion(int id)
    {
        var lessonSession = HttpContext.Session.Get<LessonSession>(SessionKeys.LessonSession);
        if (lessonSession == null || lessonSession.LessonId != id)
            return BadRequest("No active lesson session.");

        if (lessonSession.CurrentQuestionIndex >= lessonSession.QuestionIds.Count)
            return Json(new { complete = true });

        var qId = lessonSession.QuestionIds[lessonSession.CurrentQuestionIndex];
        var question = await _db.Questions
            .Include(q => q.AnswerOptions)
            .FirstOrDefaultAsync(q => q.Id == qId);

        if (question == null) return NotFound();

        var options = question.AnswerOptions
            .OrderBy(_ => Guid.NewGuid())
            .Select(a => new AnswerOptionViewModel { Id = a.Id, Text = a.Text, PairMatchId = a.PairMatchId })
            .ToList();

        var vm = new QuestionViewModel
        {
            QuestionId = question.Id,
            Type = question.Type,
            Prompt = question.Prompt,
            CodeSnippet = question.CodeSnippet,
            HintText = question.HintText,
            Options = options,
            QuestionNumber = lessonSession.CurrentQuestionIndex + 1,
            TotalQuestions = lessonSession.QuestionIds.Count,
            LivesRemaining = lessonSession.LivesRemaining,
            ProgressPercent = (double)lessonSession.CurrentQuestionIndex / lessonSession.QuestionIds.Count * 100,
            LeftPairs = options.Where(o => question.AnswerOptions.Any(a => a.Id == o.Id && a.PairGroup == 1)).ToList(),
            RightPairs = options.Where(o => question.AnswerOptions.Any(a => a.Id == o.Id && a.PairGroup == 2)).ToList()
        };

        return Json(vm);
    }

    // POST /lesson/{id}/answer  (AJAX)
    [HttpPost]
    [ActionName("answer")]
    public async Task<IActionResult> SubmitAnswer(int id, [FromBody] AnswerSubmission submission)
    {
        var lessonSession = HttpContext.Session.Get<LessonSession>(SessionKeys.LessonSession);
        if (lessonSession == null || lessonSession.LessonId != id)
            return BadRequest("No active lesson session.");

        var question = await _db.Questions
            .Include(q => q.AnswerOptions)
            .FirstOrDefaultAsync(q => q.Id == submission.QuestionId);

        if (question == null) return NotFound();

        bool isCorrect = question.Type switch
        {
            QuestionType.MultipleChoice or QuestionType.TrueFalse =>
                submission.AnswerId.HasValue &&
                question.AnswerOptions.Any(a => a.Id == submission.AnswerId && a.IsCorrect),

            QuestionType.FillInTheBlank =>
                question.AnswerOptions
                    .Where(a => a.IsCorrect)
                    .Any(a => string.Equals(
                        a.Text.Trim(),
                        submission.AnswerText?.Trim() ?? "",
                        StringComparison.OrdinalIgnoreCase)),

            QuestionType.MatchPairs =>
                submission.PairMatches != null &&
                submission.PairMatches.All(pm =>
                {
                    var left = question.AnswerOptions.FirstOrDefault(a => a.Id == pm.LeftId);
                    return left != null && left.PairMatchId == pm.RightId;
                }),

            _ => false
        };

        if (!isCorrect)
        {
            lessonSession.MistakeCount++;
            lessonSession.LivesRemaining = Math.Max(0, lessonSession.LivesRemaining - 1);
        }
        else
        {
            lessonSession.CurrentQuestionIndex++;
        }

        bool outOfLives = lessonSession.LivesRemaining == 0;
        bool lessonComplete = lessonSession.CurrentQuestionIndex >= lessonSession.QuestionIds.Count || outOfLives;

        if (lessonComplete && !outOfLives)
        {
            var lesson = await _db.Lessons.FindAsync(id);
            if (lesson != null)
            {
                lessonSession.XpEarnedThisLesson = _gamification.CalculateXpReward(
                    lesson.XpReward, lesson.XpPerfectBonus, lessonSession.MistakeCount);
            }
        }

        HttpContext.Session.Set(SessionKeys.LessonSession, lessonSession);

        var correctAnswerId = question.AnswerOptions.FirstOrDefault(a => a.IsCorrect)?.Id ?? 0;

        return Json(new AnswerResult
        {
            IsCorrect = isCorrect,
            Explanation = question.ExplanationText,
            HintText = !isCorrect ? question.HintText : null,
            LivesRemaining = lessonSession.LivesRemaining,
            IsLessonComplete = lessonComplete && !outOfLives,
            OutOfLives = outOfLives,
            CorrectAnswerId = correctAnswerId
        });
    }

    // GET /lesson/{id}/complete
    public async Task<IActionResult> Complete(int id)
    {
        var lessonSession = HttpContext.Session.Get<LessonSession>(SessionKeys.LessonSession);
        if (lessonSession == null || lessonSession.LessonId != id)
            return RedirectToAction(nameof(Start), new { id });

        var lesson = await _db.Lessons
            .Include(l => l.Unit).ThenInclude(u => u.Track)
            .Include(l => l.Questions)
            .FirstOrDefaultAsync(l => l.Id == id);

        if (lesson == null) return NotFound();

        var progress = _progress.GetProgress();
        int oldLevel = progress.Level;
        bool outOfLives = lessonSession.LivesRemaining == 0;

        int xpEarned = outOfLives ? 0 : lessonSession.XpEarnedThisLesson > 0
            ? lessonSession.XpEarnedThisLesson
            : _gamification.CalculateXpReward(lesson.XpReward, lesson.XpPerfectBonus, lessonSession.MistakeCount);

        bool isPerfect = lessonSession.MistakeCount == 0 && !outOfLives;

        if (!outOfLives)
            _progress.RecordLessonComplete(id, lessonSession.TrackId, xpEarned, isPerfect);

        progress = _progress.GetProgress();
        int newLevel = progress.Level;

        var newAchievementKeys = _progress.CheckAndAwardAchievements(progress, _db);
        var newAchievements = await _db.Achievements
            .Where(a => newAchievementKeys.Contains(a.Key))
            .ToListAsync();

        // Find next lesson
        var allLessons = await _db.Lessons
            .Where(l => l.Unit.TrackId == lesson.Unit.TrackId)
            .Include(l => l.Unit)
            .OrderBy(l => l.Unit.SortOrder).ThenBy(l => l.SortOrder)
            .ToListAsync();

        var currentIdx = allLessons.FindIndex(l => l.Id == id);
        var nextLessonId = currentIdx >= 0 && currentIdx < allLessons.Count - 1
            ? allLessons[currentIdx + 1].Id : (int?)null;

        // Clear lesson session
        HttpContext.Session.Remove(SessionKeys.LessonSession);

        return View(new LessonCompleteViewModel
        {
            Lesson = lesson,
            Track = lesson.Unit.Track,
            XpEarned = xpEarned,
            MistakeCount = lessonSession.MistakeCount,
            IsPerfect = isPerfect,
            LivesRemaining = lessonSession.LivesRemaining,
            NewTotalXp = progress.TotalXp,
            OldLevel = oldLevel,
            NewLevel = newLevel,
            NewlyEarnedAchievements = newAchievements,
            CurrentStreak = progress.CurrentStreak,
            NextLessonId = nextLessonId,
            OutOfLives = outOfLives,
            LevelTitle = _gamification.GetLevelTitle(newLevel),
            LevelProgressPercent = _gamification.GetLevelProgressPercent(progress.TotalXp)
        });
    }

    // POST /lesson/{id}/abandon
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Abandon(int id)
    {
        HttpContext.Session.Remove(SessionKeys.LessonSession);
        var lesson = await _db.Lessons
            .Include(l => l.Unit).ThenInclude(u => u.Track)
            .FirstOrDefaultAsync(l => l.Id == id);
        var slug = lesson?.Unit.Track.Slug ?? "docker";
        return RedirectToAction("Index", "Track", new { slug });
    }
}
