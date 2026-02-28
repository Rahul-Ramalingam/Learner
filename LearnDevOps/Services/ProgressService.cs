using LearnDevOps.Data;
using LearnDevOps.Extensions;
using LearnDevOps.Models.Domain;
using LearnDevOps.Models.Session;

namespace LearnDevOps.Services;

public class ProgressService : IProgressService
{
    private readonly IHttpContextAccessor _httpContextAccessor;

    public ProgressService(IHttpContextAccessor httpContextAccessor)
    {
        _httpContextAccessor = httpContextAccessor;
    }

    private ISession Session => _httpContextAccessor.HttpContext!.Session;

    public GuestProgress GetProgress()
    {
        return Session.Get<GuestProgress>(SessionKeys.GuestProgress) ?? new GuestProgress();
    }

    public void SaveProgress(GuestProgress progress)
    {
        Session.Set(SessionKeys.GuestProgress, progress);
    }

    public bool IsLessonCompleted(int lessonId)
    {
        return GetProgress().CompletedLessons.ContainsKey(lessonId);
    }

    public void RecordLessonComplete(int lessonId, int trackId, int xpEarned, bool isPerfect)
    {
        var progress = GetProgress();
        var today = DateTime.UtcNow.Date;

        if (progress.LastActivityDate?.Date == today.AddDays(-1))
            progress.CurrentStreak++;
        else if (progress.LastActivityDate?.Date != today)
            progress.CurrentStreak = 1;

        progress.LastActivityDate = DateTime.UtcNow;

        if (!progress.CompletedLessons.ContainsKey(lessonId))
        {
            progress.TotalXp += xpEarned;
            progress.TrackXp[trackId] = progress.TrackXp.GetValueOrDefault(trackId) + xpEarned;
            progress.CompletedLessons[lessonId] = new LessonProgressRecord
            {
                LessonId = lessonId,
                XpEarned = xpEarned,
                IsPerfect = isPerfect,
                CompletedAt = DateTime.UtcNow,
                TimesCompleted = 1
            };
        }
        else
        {
            var halfXp = Math.Max(1, xpEarned / 2);
            progress.TotalXp += halfXp;
            progress.TrackXp[trackId] = progress.TrackXp.GetValueOrDefault(trackId) + halfXp;
            progress.CompletedLessons[lessonId].TimesCompleted++;
        }

        SaveProgress(progress);
    }

    public List<string> CheckAndAwardAchievements(GuestProgress progress, AppDbContext db)
    {
        var newlyEarned = new List<string>();
        var allAchievements = db.Achievements.ToList();

        foreach (var achievement in allAchievements)
        {
            if (progress.EarnedAchievements.Contains(achievement.Key))
                continue;

            bool earned = achievement.Trigger switch
            {
                AchievementTrigger.FirstLessonComplete =>
                    progress.CompletedLessons.Count >= 1,
                AchievementTrigger.LessonsCompleted =>
                    progress.CompletedLessons.Count >= achievement.TriggerThreshold,
                AchievementTrigger.StreakDays =>
                    progress.CurrentStreak >= achievement.TriggerThreshold,
                AchievementTrigger.XpEarned =>
                    progress.TotalXp >= achievement.TriggerThreshold,
                AchievementTrigger.PerfectLesson =>
                    progress.CompletedLessons.Values.Any(l => l.IsPerfect),
                AchievementTrigger.TrackComplete when achievement.TrackSlug != null =>
                    IsTrackComplete(achievement.TrackSlug, progress, db),
                AchievementTrigger.AllTracksComplete =>
                    db.Tracks.All(t => IsTrackComplete(t.Slug, progress, db)),
                _ => false
            };

            if (earned)
            {
                progress.EarnedAchievements.Add(achievement.Key);
                newlyEarned.Add(achievement.Key);
            }
        }

        if (newlyEarned.Count > 0)
            SaveProgress(progress);

        return newlyEarned;
    }

    public void ResetProgress()
    {
        Session.Remove(SessionKeys.GuestProgress);
    }

    private static bool IsTrackComplete(string trackSlug, GuestProgress progress, AppDbContext db)
    {
        var track = db.Tracks.FirstOrDefault(t => t.Slug == trackSlug);
        if (track == null) return false;
        var lessonIds = db.Lessons
            .Where(l => l.Unit.TrackId == track.Id)
            .Select(l => l.Id)
            .ToList();
        return lessonIds.Count > 0 && lessonIds.All(id => progress.CompletedLessons.ContainsKey(id));
    }
}
