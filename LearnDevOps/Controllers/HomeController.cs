using LearnDevOps.Data;
using LearnDevOps.Services;
using LearnDevOps.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace LearnDevOps.Controllers;

public class HomeController : Controller
{
    private readonly AppDbContext _db;
    private readonly IProgressService _progress;
    private readonly IGamificationService _gamification;

    public HomeController(AppDbContext db, IProgressService progress, IGamificationService gamification)
    {
        _db = db; _progress = progress; _gamification = gamification;
    }

    public async Task<IActionResult> Index()
    {
        var progress = _progress.GetProgress();
        var tracks = await _db.Tracks
            .Include(t => t.Units).ThenInclude(u => u.Lessons)
            .Include(t => t.PrerequisiteTrack)
            .OrderBy(t => t.SortOrder)
            .ToListAsync();

        var vm = new HomeIndexViewModel
        {
            Progress = progress,
            LevelTitle = _gamification.GetLevelTitle(progress.Level),
            LevelProgressPercent = _gamification.GetLevelProgressPercent(progress.TotalXp)
        };

        foreach (var track in tracks)
        {
            var allLessons = track.Units.SelectMany(u => u.Lessons).ToList();
            var completed = allLessons.Count(l => progress.CompletedLessons.ContainsKey(l.Id));
            var isLocked = !_gamification.IsTrackUnlocked(
                track.Id, track.PrerequisiteTrackId, track.UnlockThresholdPercent, progress, _db);

            vm.Tracks.Add(new TrackSummary
            {
                Track = track,
                TotalLessons = allLessons.Count,
                CompletedLessons = completed,
                TrackXp = progress.TrackXp.GetValueOrDefault(track.Id),
                ProgressPercent = allLessons.Count == 0 ? 0 : (double)completed / allLessons.Count * 100,
                IsLocked = isLocked,
                IsComplete = allLessons.Count > 0 && completed == allLessons.Count,
                PrerequisiteTrackName = track.PrerequisiteTrack?.Name
            });
        }

        return View(vm);
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error() => View();
}
