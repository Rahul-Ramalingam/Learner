namespace LearnDevOps.Models.Session;

public class GuestProgress
{
    public string GuestId { get; set; } = Guid.NewGuid().ToString("N");
    public int TotalXp { get; set; }
    public int CurrentStreak { get; set; }
    public DateTime? LastActivityDate { get; set; }
    public int Level => CalculateLevel(TotalXp);
    public Dictionary<int, LessonProgressRecord> CompletedLessons { get; set; } = new();
    public List<string> EarnedAchievements { get; set; } = new();
    public Dictionary<int, int> TrackXp { get; set; } = new();
    public Dictionary<string, string> TopicStatus { get; set; } = new();

    public static int CalculateLevel(int xp)
    {
        return (int)Math.Floor(Math.Sqrt(xp / 10.0)) + 1;
    }

    public static int XpForNextLevel(int currentLevel)
    {
        return (int)Math.Pow(currentLevel, 2) * 10;
    }

    public static int XpStartOfLevel(int level)
    {
        return (int)Math.Pow(level - 1, 2) * 10;
    }
}

public class LessonProgressRecord
{
    public int LessonId { get; set; }
    public int XpEarned { get; set; }
    public bool IsPerfect { get; set; }
    public DateTime CompletedAt { get; set; }
    public int TimesCompleted { get; set; }
}
