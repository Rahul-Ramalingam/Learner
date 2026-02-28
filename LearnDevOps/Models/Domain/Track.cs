namespace LearnDevOps.Models.Domain;

public class Track
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Slug { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public string Emoji { get; set; } = string.Empty;
    public string ColorHex { get; set; } = "#58CC02";
    public int SortOrder { get; set; }
    public int? PrerequisiteTrackId { get; set; }
    public int UnlockThresholdPercent { get; set; } = 0;

    public Track? PrerequisiteTrack { get; set; }
    public ICollection<Track> DependentTracks { get; set; } = new List<Track>();
    public ICollection<Unit> Units { get; set; } = new List<Unit>();
}
