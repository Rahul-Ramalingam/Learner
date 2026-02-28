# LearnDevOps — Architecture & Design Reference

> **Purpose:** This document is the living reference for understanding, maintaining, and extending the LearnDevOps application. Update it whenever significant architectural decisions are made.

---

## Table of Contents

1. [Project Overview](#1-project-overview)
2. [Technology Stack](#2-technology-stack)
3. [Directory Structure](#3-directory-structure)
4. [Architecture Overview](#4-architecture-overview)
5. [Domain Model](#5-domain-model)
6. [Data Access Layer](#6-data-access-layer)
7. [Service Layer](#7-service-layer)
8. [Controllers & Routing](#8-controllers--routing)
9. [View Layer](#9-view-layer)
10. [Session & State Management](#10-session--state-management)
11. [Gamification System](#11-gamification-system)
12. [Question Engine](#12-question-engine)
13. [Lesson Flow (Request Lifecycle)](#13-lesson-flow-request-lifecycle)
14. [Seed Data System](#14-seed-data-system)
15. [Frontend Architecture](#15-frontend-architecture)
16. [Design Patterns](#16-design-patterns)
17. [Coding Conventions](#17-coding-conventions)
18. [Feature Extension Guide](#18-feature-extension-guide)
19. [Known Constraints & Trade-offs](#19-known-constraints--trade-offs)

---

## 1. Project Overview

**LearnDevOps** is a Duolingo-style gamified learning platform for DevOps, Cloud, and .NET technologies. Users progress through structured learning tracks, completing interactive quiz-based lessons to earn XP, unlock content, and collect achievement badges.

| Attribute | Value |
|-----------|-------|
| Solution | `r:\Learn\` |
| Project | `r:\Learn\LearnDevOps\` |
| Framework | ASP.NET Core 10.0 MVC |
| Database | SQLite (`learndevops.db`) |
| ORM | Entity Framework Core 9.0.2 |
| Default Port | `http://localhost:5003` |

**Learning Tracks (6 total):**
| Track | Emoji | Color | Prerequisites |
|-------|-------|-------|---------------|
| Docker | 🐳 | `#0db7ed` | None |
| Kubernetes | ☸️ | `#326ce5` | 50% Docker |
| AKS | ☁️ | `#0078d4` | 50% Kubernetes |
| .NET Essentials | 🔷 | `#512bd4` | None |
| Microservices | 🔌 | `#FF6B35` | .NET Essentials |
| System Design | 🏗️ | `#6C5CE7` | None |

---

## 2. Technology Stack

```
ASP.NET Core 10.0 MVC
  ├── Entity Framework Core 9.0.2 (SQLite provider)
  ├── Razor Views (.cshtml)
  ├── Bootstrap 5 (CSS framework)
  ├── jQuery 3 + jQuery Validation (client-side)
  └── Vanilla JS AJAX (lesson game loop)
```

**NuGet Packages:**
- `Microsoft.EntityFrameworkCore.Sqlite` 9.0.2
- `Microsoft.EntityFrameworkCore.Design` 9.0.2
- `Microsoft.EntityFrameworkCore.Tools` 9.0.2

---

## 3. Directory Structure

```
r:\Learn\
└── LearnDevOps\
    ├── Controllers\            # MVC Controllers (4 files)
    │   ├── HomeController.cs
    │   ├── TrackController.cs
    │   ├── LessonController.cs
    │   └── ProfileController.cs
    │
    ├── Models\
    │   ├── Domain\             # EF Core entity classes (6 files)
    │   │   ├── Track.cs
    │   │   ├── Unit.cs
    │   │   ├── Lesson.cs
    │   │   ├── Question.cs     # Includes QuestionType enum
    │   │   ├── AnswerOption.cs
    │   │   └── Achievement.cs  # Includes AchievementTrigger enum
    │   ├── Session\            # Session state models (3 files)
    │   │   ├── GuestProgress.cs
    │   │   ├── LessonSession.cs
    │   │   └── SessionKeys.cs
    │   └── ErrorViewModel.cs
    │
    ├── ViewModels\             # DTOs for View rendering (7 files)
    │   ├── HomeIndexViewModel.cs
    │   ├── TrackViewModel.cs
    │   ├── LessonStartViewModel.cs
    │   ├── LessonPlayViewModel.cs
    │   ├── LessonCompleteViewModel.cs
    │   ├── QuestionViewModel.cs
    │   └── ProfileViewModel.cs
    │
    ├── Services\               # Business logic interfaces + implementations
    │   ├── IProgressService.cs
    │   ├── ProgressService.cs
    │   ├── IGamificationService.cs
    │   └── GamificationService.cs
    │
    ├── Data\
    │   ├── AppDbContext.cs     # EF Core DbContext with Fluent API config
    │   └── SeedData\           # Database seeding (8 files)
    │       ├── ITrackSeedData.cs
    │       ├── DatabaseSeeder.cs
    │       ├── DockerSeedData.cs       (809 lines — most comprehensive)
    │       ├── KubernetesSeedData.cs
    │       ├── AksSeedData.cs
    │       ├── DotNetSeedData.cs
    │       ├── MicroservicesSeedData.cs
    │       └── SystemDesignSeedData.cs
    │
    ├── Extensions\
    │   └── SessionExtensions.cs    # Generic JSON session Get/Set
    │
    ├── Migrations\             # EF Core auto-generated migrations
    │
    ├── Views\                  # Razor templates (13 files)
    │   ├── Home\       Index.cshtml, Privacy.cshtml
    │   ├── Track\      Index.cshtml
    │   ├── Lesson\     Start.cshtml, Play.cshtml, Complete.cshtml
    │   ├── Profile\    Index.cshtml
    │   └── Shared\     _Layout.cshtml, _LayoutLesson.cshtml, Error.cshtml
    │
    ├── wwwroot\
    │   ├── css\        site.css (432L), gamification.css (537L), lesson.css (514L)
    │   ├── js\         site.js, lesson-player.js (335L), animations.js (135L), theme.js (53L)
    │   └── lib\        bootstrap, jquery, jquery-validation
    │
    ├── Program.cs              # Entry point, DI registration, middleware pipeline
    ├── appsettings.json
    ├── LearnDevOps.csproj
    └── learndevops.db          # SQLite database file
```

---

## 4. Architecture Overview

```
┌─────────────────────────────────────────────────────────┐
│                    Browser / Client                      │
│              HTML + CSS + Bootstrap + jQuery             │
│            AJAX (lesson game loop in JS)                 │
└────────────────────────┬────────────────────────────────┘
                         │ HTTP
┌────────────────────────▼────────────────────────────────┐
│                  ASP.NET Core MVC                        │
│  ┌─────────────────────────────────────────────────┐    │
│  │                  Controllers                     │    │
│  │  HomeController  TrackController  LessonController│   │
│  │  ProfileController                               │    │
│  └────────────┬─────────────────────────────────────┘    │
│               │ Constructor DI                            │
│  ┌────────────▼────────────────────────────────────┐    │
│  │               Service Layer                      │    │
│  │    IProgressService     IGamificationService     │    │
│  │    ProgressService      GamificationService      │    │
│  └────────────┬────────────────────────────────────┘    │
│               │ AppDbContext + ISession                   │
│  ┌────────────▼────────────────────────────────────┐    │
│  │             Data Access Layer                     │    │
│  │         AppDbContext (EF Core)                   │    │
│  └────────────┬────────────────────────────────────┘    │
└───────────────│─────────────────────────────────────────┘
                │
        ┌───────▼──────┐   ┌─────────────────┐
        │   SQLite DB   │   │  ASP.NET Session │
        │ learndevops.db│   │  (JSON in cookie)│
        └──────────────┘   └─────────────────┘
```

**Key Principle:** User progress is stored **entirely in server-side session** (not the database). The database only holds content (tracks, lessons, questions). This means the app works without authentication but progress is tied to session lifetime (30 days).

---

## 5. Domain Model

### Entity Hierarchy

```
Track
 ├── SortOrder, Slug (unique), Name, Emoji, ColorHex
 ├── PrerequisiteTrackId? (self-ref FK) + UnlockThresholdPercent
 └── ICollection<Unit>

Unit
 ├── TrackId, Title, Emoji, SortOrder
 ├── XpToUnlock  ← cumulative track XP required to unlock this unit
 └── ICollection<Lesson>

Lesson
 ├── UnitId, Title, Emoji, SortOrder
 ├── XpReward (default 10), XpPerfectBonus (default 5)
 └── ICollection<Question>

Question
 ├── LessonId, Type (enum→string in DB), Prompt
 ├── CodeSnippet?, HintText?, ExplanationText?, SortOrder
 └── ICollection<AnswerOption>

AnswerOption
 ├── QuestionId, Text, IsCorrect
 ├── PairGroup? (1=left, 2=right for MatchPairs)
 ├── PairMatchId? (ID of the correct right-side answer)
 └── SortOrder

Achievement (standalone)
 ├── Key (unique), Title, Emoji, BadgeColorHex
 ├── Trigger (enum→string in DB), TriggerThreshold
 └── TrackSlug? (for TrackComplete trigger)
```

### Enums

```csharp
// Models/Domain/Question.cs
enum QuestionType { MultipleChoice, TrueFalse, FillInTheBlank, MatchPairs }

// Models/Domain/Achievement.cs
enum AchievementTrigger
{
    FirstLessonComplete, LessonsCompleted, TrackComplete,
    StreakDays, XpEarned, PerfectLesson, AllTracksComplete
}
```

### Database Relationships & Cascade Rules

| Parent | Child | Delete Behavior |
|--------|-------|----------------|
| Track | Unit | CASCADE |
| Unit | Lesson | CASCADE |
| Lesson | Question | CASCADE |
| Question | AnswerOption | CASCADE |
| Track (prereq) | Track (dependent) | SET NULL |

---

## 6. Data Access Layer

### AppDbContext (`Data/AppDbContext.cs`)

Configured using **Fluent API** in `OnModelCreating`. Key configurations:
- `Track.Slug` — maxlen 50, required, **unique index**
- `Achievement.Key` — maxlen 100, required, **unique index**
- `Question.Type` — stored as **string** (not int) via `.HasConversion<string>()`
- `Achievement.Trigger` — stored as **string**
- **QuerySplittingBehavior.SplitQuery** set globally (avoids cartesian explosion on `.Include` chains)

### Key Query Patterns

```csharp
// Eagerly load full track hierarchy
await _db.Tracks
    .Include(t => t.Units).ThenInclude(u => u.Lessons).ThenInclude(l => l.Questions)
    .Include(t => t.PrerequisiteTrack)
    .OrderBy(t => t.SortOrder)
    .ToListAsync();

// Fetch a single question with shuffled options
var question = await _db.Questions
    .Include(q => q.AnswerOptions)
    .FirstOrDefaultAsync(q => q.Id == qId);
var options = question.AnswerOptions.OrderBy(_ => Guid.NewGuid()).ToList(); // shuffle
```

---

## 7. Service Layer

### IProgressService / ProgressService

**Responsibility:** Read/write user progress to/from session. Achievement checking.

| Method | Description |
|--------|-------------|
| `GetProgress()` | Deserialize `GuestProgress` from session (or return new) |
| `SaveProgress(progress)` | Serialize `GuestProgress` to session |
| `IsLessonCompleted(lessonId)` | Check `CompletedLessons` dictionary |
| `RecordLessonComplete(...)` | Update XP, streak, track XP, retake penalty |
| `CheckAndAwardAchievements(...)` | Evaluate all achievement triggers, save if earned |
| `ResetProgress()` | Clear session key |

**XP on Retake:** First completion = full XP. Subsequent completions = `max(1, xp/2)`.

**Streak Logic:** Increments if last activity was yesterday; resets to 1 if gap > 1 day; unchanged if same day.

### IGamificationService / GamificationService

**Responsibility:** Pure calculations (no state). Stateless helper math.

| Method | Description |
|--------|-------------|
| `CalculateXpReward(baseXp, bonusXp, mistakeCount)` | baseXp + bonusXp if perfect; -2 if mistakes > 2 |
| `GetLevelTitle(level)` | Returns one of 15 level titles |
| `GetLevelProgressPercent(totalXp)` | % progress within current level |
| `GetXpIntoCurrentLevel(totalXp)` | Raw XP above current level floor |
| `IsTrackUnlocked(...)` | % of prerequisite lessons completed ≥ threshold |

**Level Titles (1–15):**
```
1 Container Newbie  →  8 Kubernetes Rookie  →  15 DevOps Legend
```

**Registration:** Both services registered as `Scoped` in `Program.cs`.

---

## 8. Controllers & Routing

### Route Definitions (`Program.cs`)

```csharp
// Named routes (order matters — most specific first)
app.MapControllerRoute("track",   "track/{slug}",        new { controller = "Track",  action = "Index" });
app.MapControllerRoute("lesson",  "lesson/{id}/{action}", new { controller = "Lesson", action = "Start" });
app.MapControllerRoute("default", "{controller=Home}/{action=Index}/{id?}");
```

### HomeController

| Route | Method | Action |
|-------|--------|--------|
| `/` | GET | `Index` — all tracks with progress/lock status |
| `/Home/Error` | GET | `Error` — error page (no-cache) |

### TrackController

| Route | Method | Action |
|-------|--------|--------|
| `/track/{slug}` | GET | `Index` — units/lessons with sequential lock logic |

**Lesson lock logic:** A lesson is locked if its unit is locked OR if the previous lesson in the unit is not completed.

### LessonController

| Route | Method | Action |
|-------|--------|--------|
| `/lesson/{id}/start` | GET | `Start` — lesson intro |
| `/lesson/{id}/begin` | POST | `Begin` — init session, shuffle questions |
| `/lesson/{id}/play` | GET | `Play` — game page shell |
| `/lesson/{id}/question` | GET (AJAX) | `GetQuestion` — next question as JSON |
| `/lesson/{id}/answer` | POST (AJAX) | `SubmitAnswer` — validate, update session, return result JSON |
| `/lesson/{id}/complete` | GET | `Complete` — results, XP save, achievements |
| `/lesson/{id}/abandon` | POST | `Abandon` — clear session, redirect to track |

**CSRF:** All state-modifying actions use `[ValidateAntiForgeryToken]`.

### ProfileController

| Route | Method | Action |
|-------|--------|--------|
| `/Profile` | GET | `Index` — XP, level, per-track stats, achievements |
| `/Profile/Reset` | POST | `Reset` — clear all progress |

---

## 9. View Layer

### Layouts

- **`_Layout.cshtml`** — Main layout: nav bar (Home, Profile links), Bootstrap CDN, theme toggle
- **`_LayoutLesson.cshtml`** — Minimal layout for gameplay: no nav distraction, lesson-specific CSS/JS

### View → ViewModel Mapping

| View | ViewModel |
|------|-----------|
| `Home/Index` | `HomeIndexViewModel` |
| `Track/Index` | `TrackViewModel` (contains `UnitViewModel` list with `LessonStatus` list) |
| `Lesson/Start` | `LessonStartViewModel` |
| `Lesson/Play` | `LessonPlayViewModel` |
| `Lesson/Complete` | `LessonCompleteViewModel` |
| `Profile/Index` | `ProfileViewModel` (contains `TrackStatViewModel` list) |

**AJAX Response Models (not Views):**
- `QuestionViewModel` → JSON from `GetQuestion`
- `AnswerResult` → JSON from `SubmitAnswer`
- `AnswerSubmission` → JSON body received by `SubmitAnswer`

---

## 10. Session & State Management

### Session Configuration

```csharp
builder.Services.AddSession(opt =>
{
    opt.IdleTimeout = TimeSpan.FromDays(30);
    opt.Cookie.HttpOnly = true;     // No JS access
    opt.Cookie.IsEssential = true;  // GDPR
    opt.Cookie.Name = ".LearnDevOps.Session";
});
```

### Session Keys (`Models/Session/SessionKeys.cs`)

```csharp
public static class SessionKeys
{
    public const string GuestProgress  = "GuestProgress";
    public const string LessonSession  = "LessonSession";
}
```

### Generic Session Extension (`Extensions/SessionExtensions.cs`)

```csharp
session.Set<T>(key, value)   // JSON serialize → SetString
session.Get<T>(key)          // GetString → JSON deserialize
```

**JSON Options:** `camelCase` property naming policy.

### GuestProgress (Persisted for 30 days)

```csharp
class GuestProgress {
    GuestId                            // Guid
    TotalXp, CurrentStreak, LastActivityDate?
    Level                              // Computed: sqrt(XP/10) + 1
    Dictionary<int, LessonProgressRecord> CompletedLessons  // key = lessonId
    List<string> EarnedAchievements    // achievement keys
    Dictionary<int, int> TrackXp      // key = trackId
}
```

### LessonSession (Lives for duration of one lesson)

```csharp
class LessonSession {
    LessonId, TrackId
    List<int> QuestionIds              // Shuffled on Begin
    CurrentQuestionIndex
    LivesRemaining (starts at 3)
    MistakeCount, XpEarnedThisLesson
    StartedAt
}
```

---

## 11. Gamification System

### XP & Levels

```
Level = floor(sqrt(TotalXP / 10)) + 1

XP for level N starts at: (N-1)² × 10
XP required to reach level N: N² × 10

Examples:
  Level 1:  0 XP    Level 5:  160 XP
  Level 2:  10 XP   Level 10: 810 XP
  Level 3:  40 XP   Level 15: 1960 XP
```

### XP Reward Formula

```
XpReward = baseXp (10)
         + bonusXp (5)   if mistakeCount == 0   (perfect lesson)
         - 2             if mistakeCount > 2     (sloppy)
         / 2             on retake (second+ completion)
```

### Content Unlock System

| Lock Type | Mechanism |
|-----------|-----------|
| Track lock | `prerequisiteTrackId` + `unlockThresholdPercent` — % of prereq lessons completed |
| Unit lock | `XpToUnlock` — cumulative `TrackXp` for that track must exceed threshold |
| Lesson lock | Sequential — previous lesson must be completed |

### Achievement Triggers

| Trigger | Fires When |
|---------|-----------|
| `FirstLessonComplete` | `CompletedLessons.Count >= 1` |
| `LessonsCompleted` | Count ≥ `TriggerThreshold` |
| `TrackComplete` | All lessons in `TrackSlug` completed |
| `StreakDays` | `CurrentStreak >= TriggerThreshold` |
| `XpEarned` | `TotalXp >= TriggerThreshold` |
| `PerfectLesson` | Any lesson with `IsPerfect == true` |
| `AllTracksComplete` | All tracks fully completed |

**Seeded Achievements (14 total):**
`first_steps`, `on_fire` (7-day streak), `perfectionist`, `docker_master`, `k8s_pilot`, `cloud_native`, `dotnet_dev`, `micro_architect`, `systems_guru`, `xp_50`, `xp_200`, `xp_500`, `five_lessons`, `twenty_lessons`

---

## 12. Question Engine

### Question Types & Validation

| Type | Answer Input | Validation Logic |
|------|-------------|-----------------|
| `MultipleChoice` | `AnswerId` (int) | `AnswerOptions.Any(a => a.Id == answerId && a.IsCorrect)` |
| `TrueFalse` | `AnswerId` (int) | Same as MultipleChoice |
| `FillInTheBlank` | `AnswerText` (string) | Case-insensitive trim compare against all `IsCorrect` options |
| `MatchPairs` | `PairMatches[]` (LeftId→RightId) | All pairs must satisfy `left.PairMatchId == rightId` |

### MatchPairs Data Model

```
AnswerOption (PairGroup=1, left side)
  └─ PairMatchId → AnswerOption.Id (PairGroup=2, right side)
```

### Question Shuffle

Questions are shuffled on `Begin` (stored in `LessonSession.QuestionIds`). Answer options are re-shuffled every time `GetQuestion` is called. Pair left/right are separated via LINQ in the controller.

### `AnswerSubmission` JSON body (client → server)

```json
{
  "questionId": 42,
  "answerId": 7,           // MultipleChoice / TrueFalse
  "answerText": "docker",  // FillInTheBlank
  "pairMatches": [         // MatchPairs
    { "leftId": 1, "rightId": 5 },
    { "leftId": 2, "rightId": 6 }
  ]
}
```

---

## 13. Lesson Flow (Request Lifecycle)

```
User clicks "Start Lesson"
   → GET /lesson/{id}/start          → LessonStartViewModel (info screen)

User clicks "Begin"
   → POST /lesson/{id}/begin
       - Shuffle questions
       - Create LessonSession (3 lives)
       - Store in session
   → Redirect to /lesson/{id}/play

   → GET /lesson/{id}/play            → LessonPlayViewModel (game shell)
       JS: lesson-player.js initializes

   Loop (AJAX):
   → GET /lesson/{id}/question        → QuestionViewModel (JSON)
       JS: renders question UI

   → POST /lesson/{id}/answer         → AnswerResult (JSON)
       If wrong: decrement lives, show feedback, reload same question
       If correct: advance index, show celebration, load next
       If lives==0 or last question: JS redirects to /complete

   → GET /lesson/{id}/complete
       - RecordLessonComplete() → updates GuestProgress in session
       - CheckAndAwardAchievements()
       - Clear LessonSession from session
   → LessonCompleteViewModel (results screen)
```

---

## 14. Seed Data System

### Pattern

```csharp
public interface ITrackSeedData
{
    void Seed(AppDbContext db);
}
```

Each `*SeedData.cs` implements `ITrackSeedData` and adds a full `Track` object graph (with `Units → Lessons → Questions → AnswerOptions`) using EF Core collection initializers.

### Seeding Trigger

`DatabaseSeeder.SeedAsync()` is called on startup (after `app.Build()`):
1. Calls `db.Database.EnsureCreatedAsync()` — creates schema if missing
2. Checks `db.Tracks.Any()` — **skips if already seeded**
3. Runs all 6 track seeders
4. Wires up `PrerequisiteTrackId` relationships by slug lookup
5. Adds 14 achievements
6. Calls `db.SaveChangesAsync()`

### Adding a New Learning Track

1. Create `Data/SeedData/YourTrackSeedData.cs` implementing `ITrackSeedData`
2. Add to the `seeders[]` array in `DatabaseSeeder.cs`
3. If it has prerequisites, add FK wiring after the loop
4. Add a corresponding `TrackComplete` achievement if desired
5. **Delete `learndevops.db`** and restart to reseed (or write a migration)

---

## 15. Frontend Architecture

### CSS Architecture

| File | Purpose | Lines |
|------|---------|-------|
| `site.css` | Base typography, layout, utility classes | 432 |
| `gamification.css` | Track cards, progress bars, XP display, badges, achievement modals | 537 |
| `lesson.css` | Question containers, answer option states, feedback, lives display | 514 |

### JavaScript Files

| File | Purpose |
|------|---------|
| `lesson-player.js` (335L) | AJAX game loop: `loadNextQuestion()`, `submitAnswer()`, `renderQuestion()`, `updateProgress()`, `updateLives()`. Handles all 4 question types client-side. |
| `animations.js` (135L) | XP pop-ups, level-up overlays, achievement toast notifications, correct/incorrect particle effects |
| `theme.js` (53L) | Dark/light mode toggle with `localStorage` persistence |
| `site.js` (4L) | Miscellaneous utilities |

### AJAX Contract

**GET `/lesson/{id}/question`** — returns `QuestionViewModel` JSON or `{ complete: true }`

**POST `/lesson/{id}/answer`** — body: `AnswerSubmission` JSON → returns `AnswerResult` JSON

The client JS in `lesson-player.js` manages the state machine for the lesson UI entirely client-side, only communicating via these two endpoints.

---

## 16. Design Patterns

### Applied Patterns

| Pattern | Where Used |
|---------|-----------|
| **MVC** | Core architecture (Controllers/Views/Models) |
| **Service Layer** | `IProgressService`, `IGamificationService` encapsulate business logic |
| **Dependency Injection** | All services injected via constructor; registered in `Program.cs` |
| **Repository (implicit)** | `AppDbContext` as the DAL; DbSets as typed repositories |
| **Strategy** | Question validation via `switch` expression on `QuestionType` |
| **Template Method** | `ITrackSeedData.Seed(db)` — each track implements its own seeding |
| **Extension Methods** | `SessionExtensions.Get<T>/Set<T>` — generic JSON session storage |
| **ViewModel Pattern** | Separate ViewModels from domain models; never pass domain entities to views |
| **Factory (data seeding)** | `DatabaseSeeder` orchestrates creation of object graphs |

### SOLID Adherence

- **S** — `ProgressService` handles only progress; `GamificationService` handles only math
- **O** — Adding new question types extends the switch; adding new achievement triggers extends the enum
- **L** — `ITrackSeedData` implementations are interchangeable
- **I** — Focused interfaces: `IProgressService` vs `IGamificationService`
- **D** — Controllers depend on interfaces, not concrete classes

---

## 17. Coding Conventions

### C# Style

- **Nullable enabled** — `string?`, `int?` used explicitly; non-nullable properties initialized in constructors or with `= string.Empty`
- **Implicit usings** — No explicit `using` statements for common namespaces
- **Primary constructors** — Not yet used; constructor injection via traditional style
- **Collection initializers** — `[]` syntax (C# 12) used in seed data arrays
- **Pattern matching** — Switch expressions (`is`, `switch { }`) for question type validation
- **Async/await** — All DB operations are async (`ToListAsync`, `FirstOrDefaultAsync`, `SaveChangesAsync`)

### View Models

- Always created in `ViewModels/` folder
- Never pass domain entities directly to views (use ViewModels)
- Computed properties (`Level`, `LeveledUp`) defined on the ViewModel itself

### Session Access Pattern

```csharp
// Always via extension methods, never raw SetString/GetString
var progress = Session.Get<GuestProgress>(SessionKeys.GuestProgress) ?? new GuestProgress();
Session.Set(SessionKeys.GuestProgress, progress);
```

### Route Naming Convention

Routes use kebab-case slugs for tracks (e.g., `system-design`, `dotnet`). Slugs are defined in seed data and must be unique.

---

## 18. Feature Extension Guide

### Adding a New Question Type

1. Add to `QuestionType` enum in `Models/Domain/Question.cs`
2. Add validation logic to the `switch` in `LessonController.SubmitAnswer()`
3. Add `AnswerSubmission` field if needed (e.g., new answer format)
4. Add rendering in `lesson-player.js` → `renderQuestion(q)`
5. Add CSS styles in `lesson.css`

### Adding a New Achievement

1. Optionally extend `AchievementTrigger` enum
2. Add evaluation logic to `ProgressService.CheckAndAwardAchievements()`
3. Add `new Achievement { ... }` to `DatabaseSeeder.cs`
4. Delete `learndevops.db` to reseed

### Adding a New Learning Track

1. Create `Data/SeedData/YourTrackSeedData.cs` (implement `ITrackSeedData`)
2. Add to seeders array in `DatabaseSeeder.cs`
3. Wire prerequisites if needed
4. Add track-complete achievement
5. Delete DB and restart

### Adding User Authentication

The natural extension point:
1. Replace `GuestProgress` session storage with a DB-backed `UserProgress` entity
2. Add ASP.NET Core Identity or a custom auth scheme
3. `ProgressService` would switch from `ISession` to `DbContext` as backing store
4. The service interface (`IProgressService`) stays the same — controllers don't change

### Adding a New Service

1. Create `IYourService.cs` + `YourService.cs` in `Services/`
2. Register in `Program.cs`: `builder.Services.AddScoped<IYourService, YourService>();`
3. Inject via constructor in controllers that need it

---

## 19. Known Constraints & Trade-offs

| Constraint | Impact | Mitigation |
|-----------|--------|-----------|
| **Session-only progress** | Progress lost if session expires (30 days) or cleared | Future: DB-backed user accounts |
| **No authentication** | No multi-device sync, no leaderboards | By design for frictionless onboarding |
| **SQLite** | Single-writer constraint, not suitable for high concurrency | Fine for learning/development; swap to PostgreSQL for production |
| **EF Core 9 on .NET 10** | Slight version mismatch (EF 10 would align) | Works correctly; upgrade when EF 10 stable |
| **Question shuffle on GetQuestion** | Options re-shuffle on page refresh, potentially confusing | Acceptable for quiz UX |
| **No question retry within lesson** | Wrong answer just decrements lives; no "try again" | Intentional Duolingo-style: move on, learn from explanation |
| **Seed data hardcoded** | Content changes require code changes + DB reseed | Future: admin CMS for content management |
| **In-memory session** | Default ASP.NET Core in-memory session; lost on restart | Configure Redis/SQL session provider for production |

---

*Last updated: 2026-02-27 — Initial architecture document created from full codebase analysis.*
