namespace LearnDevOps.Models.Session;

public class LessonSession
{
    public int LessonId { get; set; }
    public int TrackId { get; set; }
    public List<int> QuestionIds { get; set; } = new();
    public int CurrentQuestionIndex { get; set; }
    public int LivesRemaining { get; set; } = 3;
    public int MistakeCount { get; set; }
    public int XpEarnedThisLesson { get; set; }
    public DateTime StartedAt { get; set; } = DateTime.UtcNow;
}
