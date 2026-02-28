using LearnDevOps.Models.Domain;
using LearnDevOps.Models.Session;

namespace LearnDevOps.ViewModels;

public class HomeIndexViewModel
{
    public List<TrackSummary> Tracks { get; set; } = new();
    public GuestProgress Progress { get; set; } = new();
    public int Level => Progress.Level;
    public int TotalXp => Progress.TotalXp;
    public int TotalLessonsCompleted => Progress.CompletedLessons.Count;
    public int CurrentStreak => Progress.CurrentStreak;
    public int XpForNextLevel => GuestProgress.XpForNextLevel(Level);
    public int XpIntoCurrentLevel => GuestProgress.XpStartOfLevel(Level);
    public double LevelProgressPercent { get; set; }
    public string LevelTitle { get; set; } = string.Empty;
    public List<Achievement> RecentAchievements { get; set; } = new();
}

public class TrackSummary
{
    public Track Track { get; set; } = null!;
    public int TotalLessons { get; set; }
    public int CompletedLessons { get; set; }
    public int TrackXp { get; set; }
    public double ProgressPercent { get; set; }
    public bool IsLocked { get; set; }
    public bool IsComplete { get; set; }
    public string? PrerequisiteTrackName { get; set; }
    public int RequiredPercent => Track?.UnlockThresholdPercent ?? 0;
}
