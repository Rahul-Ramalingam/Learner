using LearnDevOps.Data;
using LearnDevOps.Services;
using LearnDevOps.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace LearnDevOps.Controllers;

public class TrackController : Controller
{
    private readonly AppDbContext _db;
    private readonly IProgressService _progress;
    private readonly IGamificationService _gamification;

    public TrackController(AppDbContext db, IProgressService progress, IGamificationService gamification)
    {
        _db = db; _progress = progress; _gamification = gamification;
    }

    public async Task<IActionResult> Index(string slug)
    {
        var track = await _db.Tracks
            .Include(t => t.Units).ThenInclude(u => u.Lessons).ThenInclude(l => l.Questions)
            .Include(t => t.PrerequisiteTrack)
            .FirstOrDefaultAsync(t => t.Slug == slug);

        if (track == null) return NotFound();

        var progress = _progress.GetProgress();
        var isLocked = !_gamification.IsTrackUnlocked(
            track.Id, track.PrerequisiteTrackId, track.UnlockThresholdPercent, progress, _db);

        var vm = new TrackViewModel
        {
            Track = track,
            Progress = progress,
            IsLocked = isLocked,
            PrerequisiteTrackName = track.PrerequisiteTrack?.Name
        };

        // Track-level XP earned so far (for unit unlock logic)
        int trackXpSoFar = progress.TrackXp.GetValueOrDefault(track.Id);

        foreach (var unit in track.Units.OrderBy(u => u.SortOrder))
        {
            bool unitUnlocked = !isLocked && trackXpSoFar >= unit.XpToUnlock;
            var unitVm = new UnitViewModel
            {
                Unit = unit,
                IsUnlocked = unitUnlocked
            };

            bool previousCompleted = true;
            foreach (var lesson in unit.Lessons.OrderBy(l => l.SortOrder))
            {
                var rec = progress.CompletedLessons.GetValueOrDefault(lesson.Id);
                bool lessonLocked = !unitUnlocked || !previousCompleted;

                unitVm.Lessons.Add(new LessonStatus
                {
                    Lesson = lesson,
                    IsCompleted = rec != null,
                    IsPerfect = rec?.IsPerfect ?? false,
                    IsLocked = lessonLocked,
                    XpEarned = rec?.XpEarned ?? 0,
                    QuestionCount = lesson.Questions.Count
                });

                previousCompleted = rec != null;
            }

            int done = unitVm.Lessons.Count(l => l.IsCompleted);
            unitVm.UnitProgressPercent = unitVm.Lessons.Count == 0 ? 0
                : (double)done / unitVm.Lessons.Count * 100;

            vm.Units.Add(unitVm);
        }

        return View(vm);
    }
}
