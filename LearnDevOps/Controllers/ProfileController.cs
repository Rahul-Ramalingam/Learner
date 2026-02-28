using LearnDevOps.Data;
using LearnDevOps.Models.Session;
using LearnDevOps.Services;
using LearnDevOps.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace LearnDevOps.Controllers;

public class ProfileController : Controller
{
    private readonly AppDbContext _db;
    private readonly IProgressService _progress;
    private readonly IGamificationService _gamification;

    public ProfileController(AppDbContext db, IProgressService progress, IGamificationService gamification)
    {
        _db = db; _progress = progress; _gamification = gamification;
    }

    // GET /profile
    public async Task<IActionResult> Index()
    {
        var progress = _progress.GetProgress();
        var tracks = await _db.Tracks
            .Include(t => t.Units).ThenInclude(u => u.Lessons)
            .OrderBy(t => t.SortOrder)
            .ToListAsync();

        var achievements = await _db.Achievements.ToListAsync();

        var trackStats = tracks.Select(t =>
        {
            var allLessons = t.Units.SelectMany(u => u.Lessons).ToList();
            var totalLessons = allLessons.Count;
            var completedLessons = allLessons.Count(l => progress.CompletedLessons.ContainsKey(l.Id));
            var trackXp = progress.TrackXp.GetValueOrDefault(t.Id, 0);
            var perfectLessons = allLessons.Count(l =>
                progress.CompletedLessons.TryGetValue(l.Id, out var rec) && rec.IsPerfect);

            return new TrackStatViewModel
            {
                Track = t,
                TotalLessons = totalLessons,
                CompletedLessons = completedLessons,
                XpEarned = trackXp,
                PerfectLessons = perfectLessons,
                CompletionPercent = totalLessons > 0
                    ? (double)completedLessons / totalLessons * 100 : 0
            };
        }).ToList();

        var earnedAchievements = achievements
            .Where(a => progress.EarnedAchievements.Contains(a.Key))
            .ToList();
        var lockedAchievements = achievements
            .Where(a => !progress.EarnedAchievements.Contains(a.Key))
            .ToList();

        return View(new ProfileViewModel
        {
            Progress = progress,
            LevelTitle = _gamification.GetLevelTitle(progress.Level),
            LevelProgressPercent = _gamification.GetLevelProgressPercent(progress.TotalXp),
            XpForNextLevel = GuestProgress.XpForNextLevel(progress.Level),
            XpAtStartOfLevel = GuestProgress.XpStartOfLevel(progress.Level),
            TrackStats = trackStats,
            EarnedAchievements = earnedAchievements,
            LockedAchievements = lockedAchievements
        });
    }

    // POST /profile/reset
    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Reset()
    {
        _progress.ResetProgress();
        return RedirectToAction(nameof(Index));
    }
}
