namespace LearnDevOps.Models.Domain;

public class AnswerOption
{
    public int Id { get; set; }
    public int QuestionId { get; set; }
    public string Text { get; set; } = string.Empty;
    public bool IsCorrect { get; set; }
    public int? PairGroup { get; set; }
    public int? PairMatchId { get; set; }
    public int SortOrder { get; set; }

    public Question Question { get; set; } = null!;
}
