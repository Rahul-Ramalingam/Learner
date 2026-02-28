using LearnDevOps.Data;
using LearnDevOps.Models.Session;

namespace LearnDevOps.Services;

public interface IGamificationService
{
    int CalculateXpReward(int baseXp, int bonusXp, int mistakeCount);
    string GetLevelTitle(int level);
    double GetLevelProgressPercent(int totalXp);
    int GetXpIntoCurrentLevel(int totalXp);
    bool IsTrackUnlocked(int trackId, int? prerequisiteTrackId, int unlockThreshold, GuestProgress progress, AppDbContext db);
}
