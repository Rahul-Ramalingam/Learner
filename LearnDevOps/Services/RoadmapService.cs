using System.Collections.Concurrent;
using System.Text.Json;

namespace LearnDevOps.Services;

public class RoadmapService : IRoadmapService
{
    private static readonly ConcurrentDictionary<string, RoadmapData> Cache = new();
    private static readonly JsonSerializerOptions JsonOpts = new()
    {
        PropertyNameCaseInsensitive = true,
        PropertyNamingPolicy = JsonNamingPolicy.CamelCase
    };

    private readonly IWebHostEnvironment _env;

    public RoadmapService(IWebHostEnvironment env)
    {
        _env = env;
    }

    public RoadmapData? GetRoadmap(string slug)
    {
        if (Cache.TryGetValue(slug, out var cached))
            return cached;

        var path = Path.Combine(_env.WebRootPath, "data", "roadmaps", $"{slug}.json");
        if (!File.Exists(path))
            return null;

        var json = File.ReadAllText(path);
        var data = JsonSerializer.Deserialize<RoadmapData>(json, JsonOpts);
        if (data != null)
            Cache[slug] = data;

        return data;
    }

    public RoadmapTopic? GetTopic(string slug, string topicId)
    {
        var roadmap = GetRoadmap(slug);
        if (roadmap == null) return null;

        foreach (var section in roadmap.Sections)
        {
            var found = FindTopicRecursive(section.Topics, topicId);
            if (found != null) return found;
        }

        return null;
    }

    public RoadmapProject? GetProject(string slug, string projectId)
    {
        var roadmap = GetRoadmap(slug);
        if (roadmap == null) return null;

        // Check capstone project
        if (roadmap.CapstoneProject != null && roadmap.CapstoneProject.Id == projectId)
            return roadmap.CapstoneProject;

        // Check section projects
        foreach (var section in roadmap.Sections)
        {
            if (section.Project != null && section.Project.Id == projectId)
                return section.Project;
        }

        return null;
    }

    public List<string> GetAvailableSlugs()
    {
        var dir = Path.Combine(_env.WebRootPath, "data", "roadmaps");
        if (!Directory.Exists(dir))
            return new List<string>();

        return Directory.GetFiles(dir, "*.json")
            .Select(f => Path.GetFileNameWithoutExtension(f))
            .OrderBy(s => s)
            .ToList();
    }

    private static RoadmapTopic? FindTopicRecursive(List<RoadmapTopic> topics, string topicId)
    {
        foreach (var topic in topics)
        {
            if (topic.Id == topicId)
                return topic;

            if (topic.Subtopics?.Count > 0)
            {
                var found = FindTopicRecursive(topic.Subtopics, topicId);
                if (found != null) return found;
            }
        }

        return null;
    }
}
