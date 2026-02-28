namespace LearnDevOps.Models.Domain;

public enum QuestionType
{
    MultipleChoice,
    TrueFalse,
    FillInTheBlank,
    MatchPairs
}

public class Question
{
    public int Id { get; set; }
    public int LessonId { get; set; }
    public QuestionType Type { get; set; }
    public string Prompt { get; set; } = string.Empty;
    public string? CodeSnippet { get; set; }
    public string? HintText { get; set; }
    public string? ExplanationText { get; set; }
    public int SortOrder { get; set; }

    public Lesson Lesson { get; set; } = null!;
    public ICollection<AnswerOption> AnswerOptions { get; set; } = new List<AnswerOption>();
}
