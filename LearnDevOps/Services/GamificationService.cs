using LearnDevOps.Data;
using LearnDevOps.Models.Session;

namespace LearnDevOps.Services;

public class GamificationService : IGamificationService
{
    private static readonly string[] LevelTitles =
    [
        "Container Newbie",
        "Docker Dabbler",
        "Image Builder",
        "Container Captain",
        "Compose Composer",
        "Network Navigator",
        "Volume Veteran",
        "Kubernetes Rookie",
        "Pod Pilot",
        "Cluster Commander",
        "Cloud Architect",
        "DevOps Virtuoso",
        "Platform Engineer",
        "Site Reliability Pro",
        "DevOps Legend"
    ];

    public int CalculateXpReward(int baseXp, int bonusXp, int mistakeCount)
    {
        var xp = baseXp;
        if (mistakeCount == 0) xp += bonusXp;
        if (mistakeCount > 2) xp = Math.Max(1, xp - 2);
        return xp;
    }

    public string GetLevelTitle(int level)
    {
        var index = Math.Min(level - 1, LevelTitles.Length - 1);
        return LevelTitles[index];
    }

    public double GetLevelProgressPercent(int totalXp)
    {
        var level = GuestProgress.CalculateLevel(totalXp);
        var xpStart = GuestProgress.XpStartOfLevel(level);
        var xpEnd = GuestProgress.XpForNextLevel(level);
        var xpInLevel = totalXp - xpStart;
        var xpNeeded = xpEnd - xpStart;
        return xpNeeded == 0 ? 100 : Math.Min(100, (double)xpInLevel / xpNeeded * 100);
    }

    public int GetXpIntoCurrentLevel(int totalXp)
    {
        var level = GuestProgress.CalculateLevel(totalXp);
        return totalXp - GuestProgress.XpStartOfLevel(level);
    }

    public bool IsTrackUnlocked(int trackId, int? prerequisiteTrackId, int unlockThreshold, GuestProgress progress, AppDbContext db)
    {
        if (prerequisiteTrackId == null || unlockThreshold == 0) return true;

        var prereqLessons = db.Lessons
            .Where(l => l.Unit.TrackId == prerequisiteTrackId)
            .Select(l => l.Id)
            .ToList();

        if (prereqLessons.Count == 0) return true;

        var completed = prereqLessons.Count(id => progress.CompletedLessons.ContainsKey(id));
        var percent = (double)completed / prereqLessons.Count * 100;
        return percent >= unlockThreshold;
    }
}
