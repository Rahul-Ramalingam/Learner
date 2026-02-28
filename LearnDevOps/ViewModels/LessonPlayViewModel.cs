using LearnDevOps.Models.Domain;
using LearnDevOps.Models.Session;

namespace LearnDevOps.ViewModels;

public class LessonPlayViewModel
{
    public Lesson Lesson { get; set; } = null!;
    public Track Track { get; set; } = null!;
    public LessonSession LessonSession { get; set; } = null!;
    public int TotalQuestions { get; set; }
}
