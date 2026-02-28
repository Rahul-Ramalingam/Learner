using LearnDevOps.Models.Domain;
using LearnDevOps.Models.Session;

namespace LearnDevOps.ViewModels;

public class ProfileViewModel
{
    public GuestProgress Progress { get; set; } = new();
    public string LevelTitle { get; set; } = string.Empty;
    public double LevelProgressPercent { get; set; }
    public int XpForNextLevel { get; set; }
    public int XpAtStartOfLevel { get; set; }
    public List<TrackStatViewModel> TrackStats { get; set; } = new();
    public List<Achievement> EarnedAchievements { get; set; } = new();
    public List<Achievement> LockedAchievements { get; set; } = new();

    public int TotalLessonsCompleted => Progress.CompletedLessons.Count;
    public int TotalPerfectLessons => Progress.CompletedLessons.Values.Count(r => r.IsPerfect);
}

public class TrackStatViewModel
{
    public Track Track { get; set; } = null!;
    public int CompletedLessons { get; set; }
    public int TotalLessons { get; set; }
    public int XpEarned { get; set; }
    public int PerfectLessons { get; set; }
    public double CompletionPercent { get; set; }
    public bool IsComplete => TotalLessons > 0 && CompletedLessons >= TotalLessons;
}
