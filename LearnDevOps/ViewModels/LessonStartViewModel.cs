using LearnDevOps.Models.Domain;

namespace LearnDevOps.ViewModels;

public class LessonStartViewModel
{
    public Lesson Lesson { get; set; } = null!;
    public Track Track { get; set; } = null!;
    public int QuestionCount { get; set; }
    public bool AlreadyCompleted { get; set; }
    public int PreviousXpEarned { get; set; }
}
