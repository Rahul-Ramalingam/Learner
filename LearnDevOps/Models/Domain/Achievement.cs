namespace LearnDevOps.Models.Domain;

public enum AchievementTrigger
{
    FirstLessonComplete,
    LessonsCompleted,
    TrackComplete,
    StreakDays,
    XpEarned,
    PerfectLesson,
    AllTracksComplete
}

public class Achievement
{
    public int Id { get; set; }
    public string Key { get; set; } = string.Empty;
    public string Title { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public string Emoji { get; set; } = string.Empty;
    public string BadgeColorHex { get; set; } = "#FFD900";
    public AchievementTrigger Trigger { get; set; }
    public int TriggerThreshold { get; set; } = 0;
    public string? TrackSlug { get; set; }
}
