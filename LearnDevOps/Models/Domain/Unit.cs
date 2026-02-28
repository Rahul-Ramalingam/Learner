namespace LearnDevOps.Models.Domain;

public class Unit
{
    public int Id { get; set; }
    public int TrackId { get; set; }
    public string Title { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public string Emoji { get; set; } = string.Empty;
    public int SortOrder { get; set; }
    public int XpToUnlock { get; set; } = 0;

    public Track Track { get; set; } = null!;
    public ICollection<Lesson> Lessons { get; set; } = new List<Lesson>();
}
