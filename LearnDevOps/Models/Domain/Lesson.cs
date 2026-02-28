namespace LearnDevOps.Models.Domain;

public class Lesson
{
    public int Id { get; set; }
    public int UnitId { get; set; }
    public string Title { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public string Emoji { get; set; } = string.Empty;
    public int SortOrder { get; set; }
    public int XpReward { get; set; } = 10;
    public int XpPerfectBonus { get; set; } = 5;

    public Unit Unit { get; set; } = null!;
    public ICollection<Question> Questions { get; set; } = new List<Question>();
}
