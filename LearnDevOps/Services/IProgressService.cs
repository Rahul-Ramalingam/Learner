using LearnDevOps.Data;
using LearnDevOps.Models.Session;

namespace LearnDevOps.Services;

public interface IProgressService
{
    GuestProgress GetProgress();
    void SaveProgress(GuestProgress progress);
    bool IsLessonCompleted(int lessonId);
    void RecordLessonComplete(int lessonId, int trackId, int xpEarned, bool isPerfect);
    List<string> CheckAndAwardAchievements(GuestProgress progress, AppDbContext db);
    void ResetProgress();
}
