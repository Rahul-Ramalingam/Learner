using LearnDevOps.Models.Domain;

namespace LearnDevOps.ViewModels;

public class LessonCompleteViewModel
{
    public Lesson Lesson { get; set; } = null!;
    public Track Track { get; set; } = null!;
    public int XpEarned { get; set; }
    public int MistakeCount { get; set; }
    public bool IsPerfect { get; set; }
    public int LivesRemaining { get; set; }
    public int NewTotalXp { get; set; }
    public int OldLevel { get; set; }
    public int NewLevel { get; set; }
    public bool LeveledUp => NewLevel > OldLevel;
    public List<Achievement> NewlyEarnedAchievements { get; set; } = new();
    public int CurrentStreak { get; set; }
    public int? NextLessonId { get; set; }
    public bool OutOfLives { get; set; }
    public string LevelTitle { get; set; } = string.Empty;
    public double LevelProgressPercent { get; set; }
}
