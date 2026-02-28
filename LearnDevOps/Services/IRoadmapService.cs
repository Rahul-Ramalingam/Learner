namespace LearnDevOps.Services;

public interface IRoadmapService
{
    RoadmapData? GetRoadmap(string slug);
    RoadmapTopic? GetTopic(string slug, string topicId);
    List<string> GetAvailableSlugs();
}

// ---------- DTOs for JSON deserialization ----------

public class RoadmapData
{
    public string Slug { get; set; } = "";
    public string Title { get; set; } = "";
    public string Description { get; set; } = "";
    public string Emoji { get; set; } = "";
    public string ColorHex { get; set; } = "#58CC02";
    public List<RoadmapSection> Sections { get; set; } = new();
}

public class RoadmapSection
{
    public string Id { get; set; } = "";
    public string Title { get; set; } = "";
    public int Order { get; set; }
    public List<RoadmapTopic> Topics { get; set; } = new();
}

public class RoadmapTopic
{
    public string Id { get; set; } = "";
    public string Title { get; set; } = "";
    public string Description { get; set; } = "";
    public bool IsOptional { get; set; }
    public List<YouTubeVideo> YoutubeVideos { get; set; } = new();
    public List<Resource> Resources { get; set; } = new();
    public List<InterviewQA> InterviewQuestions { get; set; } = new();
    public List<string> Tips { get; set; } = new();
    public List<RoadmapTopic> Subtopics { get; set; } = new();
}

public class YouTubeVideo
{
    public string Title { get; set; } = "";
    public string Url { get; set; } = "";
    public string Channel { get; set; } = "";
    public string Duration { get; set; } = "";
}

public class Resource
{
    public string Title { get; set; } = "";
    public string Url { get; set; } = "";
    public string Type { get; set; } = "article"; // official, article, course, tool
}

public class InterviewQA
{
    public string Q { get; set; } = "";
    public string A { get; set; } = "";
}
