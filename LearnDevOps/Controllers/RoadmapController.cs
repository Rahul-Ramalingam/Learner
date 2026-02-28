using LearnDevOps.Services;
using Microsoft.AspNetCore.Mvc;

namespace LearnDevOps.Controllers;

public class RoadmapController : Controller
{
    private readonly IRoadmapService _roadmapService;
    private readonly IProgressService _progressService;

    public RoadmapController(IRoadmapService roadmapService, IProgressService progressService)
    {
        _roadmapService = roadmapService;
        _progressService = progressService;
    }

    // GET /roadmap/{slug}
    public IActionResult Index(string slug)
    {
        var roadmap = _roadmapService.GetRoadmap(slug);
        if (roadmap == null)
            return NotFound();

        var progress = _progressService.GetProgress();
        ViewBag.TopicStatus = progress.TopicStatus ?? new Dictionary<string, string>();

        return View(roadmap);
    }

    // GET /roadmap/{slug}/topic/{topicId}
    [HttpGet]
    public IActionResult Topic(string slug, string topicId)
    {
        var roadmap = _roadmapService.GetRoadmap(slug);
        if (roadmap == null) return NotFound();

        var topic = _roadmapService.GetTopic(slug, topicId);
        if (topic == null) return NotFound();

        var progress = _progressService.GetProgress();
        var statusKey = $"{slug}:{topicId}";
        var status = progress.TopicStatus?.GetValueOrDefault(statusKey, "not_started") ?? "not_started";

        ViewBag.Slug = slug;
        ViewBag.RoadmapTitle = roadmap.Title;
        ViewBag.RoadmapEmoji = roadmap.Emoji;
        ViewBag.ColorHex = roadmap.ColorHex;
        ViewBag.Status = status;

        return View(topic);
    }

    // POST /roadmap/{slug}/topic/{topicId}/status — AJAX
    [HttpPost]
    public IActionResult UpdateTopicStatus(string slug, string topicId, [FromBody] TopicStatusUpdate update)
    {
        var progress = _progressService.GetProgress();
        progress.TopicStatus ??= new Dictionary<string, string>();

        var statusKey = $"{slug}:{topicId}";
        progress.TopicStatus[statusKey] = update.Status;

        _progressService.SaveProgress(progress);

        return Json(new { success = true, status = update.Status });
    }
}

public class TopicStatusUpdate
{
    public string Status { get; set; } = "not_started";
}
