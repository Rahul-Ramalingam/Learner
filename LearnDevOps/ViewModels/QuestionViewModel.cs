using LearnDevOps.Models.Domain;

namespace LearnDevOps.ViewModels;

public class QuestionViewModel
{
    public int QuestionId { get; set; }
    public QuestionType Type { get; set; }
    public string Prompt { get; set; } = string.Empty;
    public string? CodeSnippet { get; set; }
    public string? HintText { get; set; }
    public List<AnswerOptionViewModel> Options { get; set; } = new();
    public int QuestionNumber { get; set; }
    public int TotalQuestions { get; set; }
    public int LivesRemaining { get; set; }
    public double ProgressPercent { get; set; }
    public List<AnswerOptionViewModel> LeftPairs { get; set; } = new();
    public List<AnswerOptionViewModel> RightPairs { get; set; } = new();
}

public class AnswerOptionViewModel
{
    public int Id { get; set; }
    public string Text { get; set; } = string.Empty;
    public int? PairMatchId { get; set; }
}

public class AnswerResult
{
    public bool IsCorrect { get; set; }
    public string? Explanation { get; set; }
    public string? HintText { get; set; }
    public int LivesRemaining { get; set; }
    public bool IsLessonComplete { get; set; }
    public bool OutOfLives { get; set; }
    public int CorrectAnswerId { get; set; }
}

public class AnswerSubmission
{
    public int QuestionId { get; set; }
    public int? AnswerId { get; set; }
    public string? AnswerText { get; set; }
    public List<PairMatch>? PairMatches { get; set; }
}

public class PairMatch
{
    public int LeftId { get; set; }
    public int RightId { get; set; }
}
