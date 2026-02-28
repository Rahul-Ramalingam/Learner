using LearnDevOps.Models.Domain;

namespace LearnDevOps.Data.SeedData;

public class DotNetSeedData : ITrackSeedData
{
    public void Seed(AppDbContext db)
    {
        if (db.Tracks.Any(t => t.Slug == "dotnet")) return;

        db.Tracks.Add(new Track
        {
            Name = ".NET Essentials",
            Slug = "dotnet",
            Description = "Master C#, ASP.NET Core MVC, Entity Framework, DI, and testing.",
            Emoji = "🔷",
            ColorHex = "#512bd4",
            SortOrder = 4,
            UnlockThresholdPercent = 0,
            Units =
            [
                new Unit { Title = "C# & CLR", Emoji = "⚙️", SortOrder = 1, Description = "CLR runtime, C# fundamentals, and modern features.", Lessons =
                [
                    new Lesson { Title = "CLR & Runtime", Emoji = "🏭", SortOrder = 1, XpReward = 10, XpPerfectBonus = 5, Questions =
                    [
                        new Question { Type = QuestionType.MultipleChoice, SortOrder = 1, Prompt = "What does CLR stand for?", ExplanationText = "Common Language Runtime — the execution engine that handles memory management, JIT compilation, and garbage collection.", AnswerOptions = [ new AnswerOption { Text = "Common Language Runtime", IsCorrect = true, SortOrder = 1 }, new AnswerOption { Text = "Core Library Runtime", IsCorrect = false, SortOrder = 2 }, new AnswerOption { Text = "Compiled Language Runner", IsCorrect = false, SortOrder = 3 }, new AnswerOption { Text = "C-Like Runtime", IsCorrect = false, SortOrder = 4 }]},
                        new Question { Type = QuestionType.TrueFalse, SortOrder = 2, Prompt = "The .NET garbage collector automatically manages memory deallocation.", ExplanationText = "True — the GC tracks object references and frees unreachable objects automatically.", AnswerOptions = [ new AnswerOption { Text = "True", IsCorrect = true, SortOrder = 1 }, new AnswerOption { Text = "False", IsCorrect = false, SortOrder = 2 }]},
                        new Question { Type = QuestionType.FillInTheBlank, SortOrder = 3, Prompt = "C# code is first compiled to ___ (Intermediate Language) before JIT compilation", ExplanationText = "IL (Intermediate Language or MSIL) is the CPU-independent bytecode that the JIT compiler turns into native code.", HintText = "Bytecode abbreviation", AnswerOptions = [ new AnswerOption { Text = "IL", IsCorrect = true, SortOrder = 1 }]}
                    ]},
                    new Lesson { Title = "C# Essentials", Emoji = "💎", SortOrder = 2, XpReward = 10, XpPerfectBonus = 5, Questions =
                    [
                        new Question { Type = QuestionType.MultipleChoice, SortOrder = 1, Prompt = "What does the 'async' keyword indicate in C#?", ExplanationText = "async marks a method as asynchronous, allowing the use of 'await' for non-blocking I/O operations.", AnswerOptions = [ new AnswerOption { Text = "The method contains asynchronous operations using await", IsCorrect = true, SortOrder = 1 }, new AnswerOption { Text = "The method runs on a separate thread", IsCorrect = false, SortOrder = 2 }, new AnswerOption { Text = "The method returns void", IsCorrect = false, SortOrder = 3 }, new AnswerOption { Text = "The method cannot throw exceptions", IsCorrect = false, SortOrder = 4 }]},
                        new Question { Type = QuestionType.FillInTheBlank, SortOrder = 2, Prompt = "In C#, the ___ operator checks if a value is null: myVar ?? defaultValue", ExplanationText = "The null-coalescing operator ?? returns the left operand if non-null, otherwise the right.", HintText = "Two question marks", AnswerOptions = [ new AnswerOption { Text = "??", IsCorrect = true, SortOrder = 1 }]},
                        new Question { Type = QuestionType.TrueFalse, SortOrder = 3, Prompt = "LINQ allows querying collections using SQL-like syntax in C#.", ExplanationText = "True — Language Integrated Query provides methods like Where, Select, OrderBy for querying any IEnumerable.", AnswerOptions = [ new AnswerOption { Text = "True", IsCorrect = true, SortOrder = 1 }, new AnswerOption { Text = "False", IsCorrect = false, SortOrder = 2 }]}
                    ]}
                ]},
                new Unit { Title = "ASP.NET Core", Emoji = "🌐", SortOrder = 2, Description = "MVC, Entity Framework, and middleware.", Lessons =
                [
                    new Lesson { Title = "MVC Pattern", Emoji = "🏛️", SortOrder = 1, XpReward = 10, XpPerfectBonus = 5, Questions =
                    [
                        new Question { Type = QuestionType.MultipleChoice, SortOrder = 1, Prompt = "In MVC, what does the Controller do?", ExplanationText = "The Controller handles HTTP requests, processes input, works with the Model, and returns a View.", AnswerOptions = [ new AnswerOption { Text = "Handles requests and coordinates Model and View", IsCorrect = true, SortOrder = 1 }, new AnswerOption { Text = "Defines the database schema", IsCorrect = false, SortOrder = 2 }, new AnswerOption { Text = "Renders HTML templates", IsCorrect = false, SortOrder = 3 }, new AnswerOption { Text = "Manages client-side JavaScript", IsCorrect = false, SortOrder = 4 }]},
                        new Question { Type = QuestionType.FillInTheBlank, SortOrder = 2, Prompt = "ASP.NET Core uses ___ binding to automatically map form/query data to action parameters", ExplanationText = "Model binding maps HTTP data (form fields, query strings, route data) to C# objects and primitives.", HintText = "Maps data to object properties", AnswerOptions = [ new AnswerOption { Text = "model", IsCorrect = true, SortOrder = 1 }]},
                        new Question { Type = QuestionType.TrueFalse, SortOrder = 3, Prompt = "Razor views use the @ symbol to transition from HTML to C# code.", ExplanationText = "True — @ is the Razor transition character. @Model.Name outputs a model property, @{ } defines code blocks.", AnswerOptions = [ new AnswerOption { Text = "True", IsCorrect = true, SortOrder = 1 }, new AnswerOption { Text = "False", IsCorrect = false, SortOrder = 2 }]}
                    ]},
                    new Lesson { Title = "Entity Framework Core", Emoji = "🗄️", SortOrder = 2, XpReward = 10, XpPerfectBonus = 5, Questions =
                    [
                        new Question { Type = QuestionType.MultipleChoice, SortOrder = 1, Prompt = "What is a DbContext in Entity Framework Core?", ExplanationText = "DbContext is the primary class for database interaction — it manages entity tracking, queries, and SaveChanges.", AnswerOptions = [ new AnswerOption { Text = "The main class for database interaction and change tracking", IsCorrect = true, SortOrder = 1 }, new AnswerOption { Text = "A connection string wrapper", IsCorrect = false, SortOrder = 2 }, new AnswerOption { Text = "A migration script runner", IsCorrect = false, SortOrder = 3 }, new AnswerOption { Text = "A query optimizer", IsCorrect = false, SortOrder = 4 }]},
                        new Question { Type = QuestionType.FillInTheBlank, SortOrder = 2, Prompt = "Complete: dotnet ef ___ add InitialCreate (to create a new EF migration)", ExplanationText = "'dotnet ef migrations add' scaffolds a new migration based on model changes.", HintText = "Database schema versions", AnswerOptions = [ new AnswerOption { Text = "migrations", IsCorrect = true, SortOrder = 1 }]},
                        new Question { Type = QuestionType.TrueFalse, SortOrder = 3, Prompt = "EF Core supports both code-first and database-first approaches.", ExplanationText = "True — code-first creates the DB from C# models; database-first scaffolds models from an existing DB.", AnswerOptions = [ new AnswerOption { Text = "True", IsCorrect = true, SortOrder = 1 }, new AnswerOption { Text = "False", IsCorrect = false, SortOrder = 2 }]}
                    ]}
                ]},
                new Unit { Title = "DI & Testing", Emoji = "💉", SortOrder = 3, Description = "Dependency Injection and unit testing with xUnit.", Lessons =
                [
                    new Lesson { Title = "Dependency Injection", Emoji = "🔌", SortOrder = 1, XpReward = 10, XpPerfectBonus = 5, Questions =
                    [
                        new Question { Type = QuestionType.MultipleChoice, SortOrder = 1, Prompt = "What are the three DI service lifetimes in ASP.NET Core?", ExplanationText = "Transient (new instance per request), Scoped (one per HTTP request), Singleton (one for app lifetime).", AnswerOptions = [ new AnswerOption { Text = "Transient, Scoped, Singleton", IsCorrect = true, SortOrder = 1 }, new AnswerOption { Text = "Request, Session, Application", IsCorrect = false, SortOrder = 2 }, new AnswerOption { Text = "Prototype, Factory, Static", IsCorrect = false, SortOrder = 3 }, new AnswerOption { Text = "New, Cached, Permanent", IsCorrect = false, SortOrder = 4 }]},
                        new Question { Type = QuestionType.TrueFalse, SortOrder = 2, Prompt = "Constructor injection is the recommended DI pattern in ASP.NET Core.", ExplanationText = "True — dependencies are passed via the constructor, making them explicit and testable.", AnswerOptions = [ new AnswerOption { Text = "True", IsCorrect = true, SortOrder = 1 }, new AnswerOption { Text = "False", IsCorrect = false, SortOrder = 2 }]},
                        new Question { Type = QuestionType.FillInTheBlank, SortOrder = 3, Prompt = "Complete: builder.Services.Add___(IMyService, MyService) — for one instance per HTTP request", ExplanationText = "AddScoped creates one instance per HTTP request scope.", HintText = "Per-request lifetime", AnswerOptions = [ new AnswerOption { Text = "Scoped<", IsCorrect = true, SortOrder = 1 }]}
                    ]},
                    new Lesson { Title = "Testing with xUnit", Emoji = "✅", SortOrder = 2, XpReward = 10, XpPerfectBonus = 5, Questions =
                    [
                        new Question { Type = QuestionType.MultipleChoice, SortOrder = 1, Prompt = "Which attribute marks a test method in xUnit?", ExplanationText = "[Fact] marks a single test. [Theory] marks a parameterised test with [InlineData].", AnswerOptions = [ new AnswerOption { Text = "[Fact]", IsCorrect = true, SortOrder = 1 }, new AnswerOption { Text = "[Test]", IsCorrect = false, SortOrder = 2 }, new AnswerOption { Text = "[TestMethod]", IsCorrect = false, SortOrder = 3 }, new AnswerOption { Text = "[UnitTest]", IsCorrect = false, SortOrder = 4 }]},
                        new Question { Type = QuestionType.FillInTheBlank, SortOrder = 2, Prompt = "Complete: Assert.___(expected, actual) — to check two values are the same", ExplanationText = "Assert.Equal compares expected and actual values.", HintText = "Same/identical", AnswerOptions = [ new AnswerOption { Text = "Equal", IsCorrect = true, SortOrder = 1 }]},
                        new Question { Type = QuestionType.TrueFalse, SortOrder = 3, Prompt = "[Theory] with [InlineData] allows running the same test with different inputs.", ExplanationText = "True — [Theory] parameterises tests. Each [InlineData] set runs the test with those values.", AnswerOptions = [ new AnswerOption { Text = "True", IsCorrect = true, SortOrder = 1 }, new AnswerOption { Text = "False", IsCorrect = false, SortOrder = 2 }]}
                    ]}
                ]}
            ]
        });
        db.SaveChanges();
    }
}
