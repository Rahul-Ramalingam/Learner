using LearnDevOps.Data;
using LearnDevOps.Data.SeedData;
using LearnDevOps.Services;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllersWithViews();

builder.Services.AddDbContext<AppDbContext>(opt =>
    opt.UseSqlite("Data Source=learndevops.db",
        b => b.UseQuerySplittingBehavior(QuerySplittingBehavior.SplitQuery)));

builder.Services.AddSession(opt =>
{
    opt.IdleTimeout = TimeSpan.FromDays(30);
    opt.Cookie.HttpOnly = true;
    opt.Cookie.IsEssential = true;
    opt.Cookie.Name = ".LearnDevOps.Session";
});

builder.Services.AddHttpContextAccessor();
builder.Services.AddScoped<IProgressService, ProgressService>();
builder.Services.AddScoped<IGamificationService, GamificationService>();
builder.Services.AddSingleton<IRoadmapService, RoadmapService>();

var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();
app.UseSession();
app.UseAuthorization();

app.MapStaticAssets();

// Custom routes for clean URLs
app.MapControllerRoute(
    name: "roadmap-topic-status",
    pattern: "roadmap/{slug}/topic/{topicId}/status",
    defaults: new { controller = "Roadmap", action = "UpdateTopicStatus" });

app.MapControllerRoute(
    name: "roadmap-topic",
    pattern: "roadmap/{slug}/topic/{topicId}",
    defaults: new { controller = "Roadmap", action = "Topic" });

app.MapControllerRoute(
    name: "roadmap",
    pattern: "roadmap/{slug}",
    defaults: new { controller = "Roadmap", action = "Index" });

app.MapControllerRoute(
    name: "track",
    pattern: "track/{slug}",
    defaults: new { controller = "Track", action = "Index" });

app.MapControllerRoute(
    name: "lesson",
    pattern: "lesson/{id}/{action}",
    defaults: new { controller = "Lesson", action = "Start" });

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}")
    .WithStaticAssets();

await DatabaseSeeder.SeedAsync(app.Services);

app.Run();
