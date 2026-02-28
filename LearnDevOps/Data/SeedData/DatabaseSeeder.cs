using LearnDevOps.Models.Domain;

namespace LearnDevOps.Data.SeedData;

public static class DatabaseSeeder
{
    public static async Task SeedAsync(IServiceProvider services)
    {
        using var scope = services.CreateScope();
        var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();

        await db.Database.EnsureCreatedAsync();

        if (db.Tracks.Any()) return;

        // Seed tracks in order
        ITrackSeedData[] seeders =
        [
            new DockerSeedData(),
            new KubernetesSeedData(),
            new AksSeedData(),
            new DotNetSeedData(),
            new MicroservicesSeedData(),
            new SystemDesignSeedData()
        ];

        foreach (var seeder in seeders)
            seeder.Seed(db);

        // Wire up prerequisite tracks now that all tracks exist
        var docker = db.Tracks.First(t => t.Slug == "docker");
        var k8s = db.Tracks.First(t => t.Slug == "kubernetes");
        var dotnet = db.Tracks.First(t => t.Slug == "dotnet");

        k8s.PrerequisiteTrackId = docker.Id;
        var aks = db.Tracks.First(t => t.Slug == "aks");
        aks.PrerequisiteTrackId = k8s.Id;
        var micro = db.Tracks.First(t => t.Slug == "microservices");
        micro.PrerequisiteTrackId = dotnet.Id;

        // Seed achievements
        db.Achievements.AddRange(
            new Achievement { Key = "first_steps", Title = "First Steps", Description = "Complete your first lesson", Emoji = "👣", BadgeColorHex = "#58CC02", Trigger = AchievementTrigger.FirstLessonComplete },
            new Achievement { Key = "on_fire", Title = "On Fire!", Description = "Maintain a 7-day streak", Emoji = "🔥", BadgeColorHex = "#FF9600", Trigger = AchievementTrigger.StreakDays, TriggerThreshold = 7 },
            new Achievement { Key = "perfectionist", Title = "Perfectionist", Description = "Complete a lesson with zero mistakes", Emoji = "💯", BadgeColorHex = "#FFD900", Trigger = AchievementTrigger.PerfectLesson },
            new Achievement { Key = "docker_master", Title = "Docker Master", Description = "Complete all Docker lessons", Emoji = "🐳", BadgeColorHex = "#0db7ed", Trigger = AchievementTrigger.TrackComplete, TrackSlug = "docker" },
            new Achievement { Key = "k8s_pilot", Title = "K8s Pilot", Description = "Complete all Kubernetes lessons", Emoji = "☸️", BadgeColorHex = "#326ce5", Trigger = AchievementTrigger.TrackComplete, TrackSlug = "kubernetes" },
            new Achievement { Key = "cloud_native", Title = "Cloud Native", Description = "Complete all AKS lessons", Emoji = "☁️", BadgeColorHex = "#0078d4", Trigger = AchievementTrigger.TrackComplete, TrackSlug = "aks" },
            new Achievement { Key = "dotnet_dev", Title = ".NET Developer", Description = "Complete all .NET lessons", Emoji = "🔷", BadgeColorHex = "#512bd4", Trigger = AchievementTrigger.TrackComplete, TrackSlug = "dotnet" },
            new Achievement { Key = "micro_architect", Title = "Micro Architect", Description = "Complete all Microservices lessons", Emoji = "🔌", BadgeColorHex = "#FF6B35", Trigger = AchievementTrigger.TrackComplete, TrackSlug = "microservices" },
            new Achievement { Key = "systems_guru", Title = "Systems Guru", Description = "Complete all System Design lessons", Emoji = "🏗️", BadgeColorHex = "#6C5CE7", Trigger = AchievementTrigger.TrackComplete, TrackSlug = "system-design" },
            new Achievement { Key = "xp_50", Title = "Getting Started", Description = "Earn 50 XP", Emoji = "⭐", BadgeColorHex = "#FFD900", Trigger = AchievementTrigger.XpEarned, TriggerThreshold = 50 },
            new Achievement { Key = "xp_200", Title = "Rising Star", Description = "Earn 200 XP", Emoji = "🌟", BadgeColorHex = "#FFD900", Trigger = AchievementTrigger.XpEarned, TriggerThreshold = 200 },
            new Achievement { Key = "xp_500", Title = "XP Champion", Description = "Earn 500 XP", Emoji = "🏆", BadgeColorHex = "#FFD900", Trigger = AchievementTrigger.XpEarned, TriggerThreshold = 500 },
            new Achievement { Key = "five_lessons", Title = "Warmed Up", Description = "Complete 5 lessons", Emoji = "🎯", BadgeColorHex = "#1CB0F6", Trigger = AchievementTrigger.LessonsCompleted, TriggerThreshold = 5 },
            new Achievement { Key = "twenty_lessons", Title = "Dedicated Learner", Description = "Complete 20 lessons", Emoji = "📚", BadgeColorHex = "#1CB0F6", Trigger = AchievementTrigger.LessonsCompleted, TriggerThreshold = 20 }
        );

        await db.SaveChangesAsync();
    }
}
