using LearnDevOps.Models.Domain;
using LearnDevOps.Models.Session;

namespace LearnDevOps.ViewModels;

public class TrackViewModel
{
    public Track Track { get; set; } = null!;
    public List<UnitViewModel> Units { get; set; } = new();
    public GuestProgress Progress { get; set; } = new();
    public bool IsLocked { get; set; }
    public string? PrerequisiteTrackName { get; set; }
}

public class UnitViewModel
{
    public Unit Unit { get; set; } = null!;
    public List<LessonStatus> Lessons { get; set; } = new();
    public bool IsUnlocked { get; set; }
    public double UnitProgressPercent { get; set; }
}

public class LessonStatus
{
    public Lesson Lesson { get; set; } = null!;
    public bool IsCompleted { get; set; }
    public bool IsPerfect { get; set; }
    public bool IsLocked { get; set; }
    public int XpEarned { get; set; }
    public int QuestionCount { get; set; }
}
