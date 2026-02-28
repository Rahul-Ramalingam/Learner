using LearnDevOps.Models.Domain;

namespace LearnDevOps.Data.SeedData;

public interface ITrackSeedData
{
    void Seed(AppDbContext db);
}
