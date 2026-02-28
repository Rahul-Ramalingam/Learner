using LearnDevOps.Models.Domain;

namespace LearnDevOps.Data.SeedData;

public class MicroservicesSeedData : ITrackSeedData
{
    public void Seed(AppDbContext db)
    {
        if (db.Tracks.Any(t => t.Slug == "microservices")) return;

        db.Tracks.Add(new Track
        {
            Name = "Microservices",
            Slug = "microservices",
            Description = "Design and build distributed microservice architectures with APIs and messaging.",
            Emoji = "🔌",
            ColorHex = "#FF6B35",
            SortOrder = 5,
            UnlockThresholdPercent = 50,
            Units =
            [
                new Unit { Title = "Concepts & APIs", Emoji = "📐", SortOrder = 1, Description = "Microservice principles and API design.", Lessons =
                [
                    new Lesson { Title = "Microservices Concepts", Emoji = "🧩", SortOrder = 1, XpReward = 10, XpPerfectBonus = 5, Questions =
                    [
                        new Question { Type = QuestionType.MultipleChoice, SortOrder = 1, Prompt = "What is a key characteristic of microservices?", ExplanationText = "Each microservice is independently deployable, owns its data, and communicates via well-defined APIs.", AnswerOptions = [ new AnswerOption { Text = "Small, independently deployable services with their own data", IsCorrect = true, SortOrder = 1 }, new AnswerOption { Text = "A single deployable unit with shared database", IsCorrect = false, SortOrder = 2 }, new AnswerOption { Text = "Services that must use the same programming language", IsCorrect = false, SortOrder = 3 }, new AnswerOption { Text = "All services running in a single process", IsCorrect = false, SortOrder = 4 }]},
                        new Question { Type = QuestionType.TrueFalse, SortOrder = 2, Prompt = "Microservices should share a single database for data consistency.", ExplanationText = "False — each microservice should own its data (database per service). Shared DBs create tight coupling.", AnswerOptions = [ new AnswerOption { Text = "True", IsCorrect = false, SortOrder = 1 }, new AnswerOption { Text = "False", IsCorrect = true, SortOrder = 2 }]},
                        new Question { Type = QuestionType.MultipleChoice, SortOrder = 3, Prompt = "What is a monolith?", ExplanationText = "A monolith is a single-unit application where all components are deployed together. Microservices break this into independent services.", AnswerOptions = [ new AnswerOption { Text = "A single deployable unit containing all application components", IsCorrect = true, SortOrder = 1 }, new AnswerOption { Text = "A large Docker container", IsCorrect = false, SortOrder = 2 }, new AnswerOption { Text = "A microservice with too many endpoints", IsCorrect = false, SortOrder = 3 }, new AnswerOption { Text = "A database with a single table", IsCorrect = false, SortOrder = 4 }]}
                    ]},
                    new Lesson { Title = "API Design & REST", Emoji = "🔗", SortOrder = 2, XpReward = 10, XpPerfectBonus = 5, Questions =
                    [
                        new Question { Type = QuestionType.MultipleChoice, SortOrder = 1, Prompt = "In REST, which HTTP method is used to create a new resource?", ExplanationText = "POST creates a new resource. PUT updates/replaces, PATCH partially updates, DELETE removes.", AnswerOptions = [ new AnswerOption { Text = "POST", IsCorrect = true, SortOrder = 1 }, new AnswerOption { Text = "GET", IsCorrect = false, SortOrder = 2 }, new AnswerOption { Text = "PUT", IsCorrect = false, SortOrder = 3 }, new AnswerOption { Text = "PATCH", IsCorrect = false, SortOrder = 4 }]},
                        new Question { Type = QuestionType.FillInTheBlank, SortOrder = 2, Prompt = "HTTP status code ___ means 'Created' (success, new resource made)", ExplanationText = "201 Created indicates a new resource was successfully created. 200 is OK, 204 is No Content.", HintText = "2xx success code", AnswerOptions = [ new AnswerOption { Text = "201", IsCorrect = true, SortOrder = 1 }]},
                        new Question { Type = QuestionType.TrueFalse, SortOrder = 3, Prompt = "REST APIs should be stateless — each request must contain all needed information.", ExplanationText = "True — statelessness is a core REST constraint. The server doesn't store client session state.", AnswerOptions = [ new AnswerOption { Text = "True", IsCorrect = true, SortOrder = 1 }, new AnswerOption { Text = "False", IsCorrect = false, SortOrder = 2 }]}
                    ]}
                ]},
                new Unit { Title = "Communication", Emoji = "📡", SortOrder = 2, Description = "Sync and async service communication patterns.", Lessons =
                [
                    new Lesson { Title = "Sync vs Async", Emoji = "🔄", SortOrder = 1, XpReward = 10, XpPerfectBonus = 5, Questions =
                    [
                        new Question { Type = QuestionType.MultipleChoice, SortOrder = 1, Prompt = "Which is an example of synchronous communication between microservices?", ExplanationText = "HTTP/REST calls are synchronous — the caller waits for a response. Message queues are asynchronous.", AnswerOptions = [ new AnswerOption { Text = "HTTP/REST API call", IsCorrect = true, SortOrder = 1 }, new AnswerOption { Text = "Publishing to a message queue", IsCorrect = false, SortOrder = 2 }, new AnswerOption { Text = "Sending an event to Kafka", IsCorrect = false, SortOrder = 3 }, new AnswerOption { Text = "Writing to a shared database", IsCorrect = false, SortOrder = 4 }]},
                        new Question { Type = QuestionType.TrueFalse, SortOrder = 2, Prompt = "Message queues (like RabbitMQ) enable asynchronous communication.", ExplanationText = "True — the sender publishes and continues. The consumer processes messages independently, decoupling services.", AnswerOptions = [ new AnswerOption { Text = "True", IsCorrect = true, SortOrder = 1 }, new AnswerOption { Text = "False", IsCorrect = false, SortOrder = 2 }]},
                        new Question { Type = QuestionType.MultipleChoice, SortOrder = 3, Prompt = "What is the main advantage of async messaging over sync HTTP?", ExplanationText = "Loose coupling and resilience — if the consumer is down, messages queue up. In sync HTTP, the call fails immediately.", AnswerOptions = [ new AnswerOption { Text = "Loose coupling and resilience — messages survive consumer downtime", IsCorrect = true, SortOrder = 1 }, new AnswerOption { Text = "Faster response times", IsCorrect = false, SortOrder = 2 }, new AnswerOption { Text = "Simpler to implement", IsCorrect = false, SortOrder = 3 }, new AnswerOption { Text = "No need for serialisation", IsCorrect = false, SortOrder = 4 }]}
                    ]},
                    new Lesson { Title = "API Gateway", Emoji = "🚪", SortOrder = 2, XpReward = 10, XpPerfectBonus = 5, Questions =
                    [
                        new Question { Type = QuestionType.MultipleChoice, SortOrder = 1, Prompt = "What is an API Gateway?", ExplanationText = "An API Gateway is a single entry point that routes requests to backend microservices, handles auth, rate limiting, and aggregation.", AnswerOptions = [ new AnswerOption { Text = "A single entry point routing requests to backend services", IsCorrect = true, SortOrder = 1 }, new AnswerOption { Text = "A database connection pool", IsCorrect = false, SortOrder = 2 }, new AnswerOption { Text = "A message broker", IsCorrect = false, SortOrder = 3 }, new AnswerOption { Text = "A service mesh proxy", IsCorrect = false, SortOrder = 4 }]},
                        new Question { Type = QuestionType.FillInTheBlank, SortOrder = 2, Prompt = "The ___ pattern allows a single API call to aggregate data from multiple services", ExplanationText = "The Backend for Frontend (BFF) or API Composition pattern aggregates multiple service responses into one.", HintText = "Combining responses", AnswerOptions = [ new AnswerOption { Text = "API Composition", IsCorrect = true, SortOrder = 1 }]},
                        new Question { Type = QuestionType.TrueFalse, SortOrder = 3, Prompt = "An API Gateway can handle cross-cutting concerns like authentication and rate limiting.", ExplanationText = "True — centralising auth, logging, rate limiting, and SSL termination at the gateway keeps services simpler.", AnswerOptions = [ new AnswerOption { Text = "True", IsCorrect = true, SortOrder = 1 }, new AnswerOption { Text = "False", IsCorrect = false, SortOrder = 2 }]}
                    ]}
                ]},
                new Unit { Title = "Resilience", Emoji = "🛡️", SortOrder = 3, Description = "Circuit breaker, retry, and fault tolerance.", Lessons =
                [
                    new Lesson { Title = "Resilience Patterns", Emoji = "🔁", SortOrder = 1, XpReward = 10, XpPerfectBonus = 5, Questions =
                    [
                        new Question { Type = QuestionType.MultipleChoice, SortOrder = 1, Prompt = "What does the Circuit Breaker pattern do?", ExplanationText = "It stops calling a failing service after repeated failures, returning fast errors instead. It 'opens' the circuit to prevent cascading failures.", AnswerOptions = [ new AnswerOption { Text = "Stops calling a failing service to prevent cascade failures", IsCorrect = true, SortOrder = 1 }, new AnswerOption { Text = "Retries failed requests indefinitely", IsCorrect = false, SortOrder = 2 }, new AnswerOption { Text = "Routes traffic to a backup service", IsCorrect = false, SortOrder = 3 }, new AnswerOption { Text = "Encrypts inter-service communication", IsCorrect = false, SortOrder = 4 }]},
                        new Question { Type = QuestionType.TrueFalse, SortOrder = 2, Prompt = "The Retry pattern should always retry immediately without delay.", ExplanationText = "False — exponential backoff (increasing delays) is recommended to avoid overwhelming the failing service.", AnswerOptions = [ new AnswerOption { Text = "True", IsCorrect = false, SortOrder = 1 }, new AnswerOption { Text = "False", IsCorrect = true, SortOrder = 2 }]},
                        new Question { Type = QuestionType.FillInTheBlank, SortOrder = 3, Prompt = "The ___ pattern returns a default value when a service call fails (fallback)", ExplanationText = "The Fallback pattern provides a default or cached response when the primary call fails.", HintText = "Alternative response", AnswerOptions = [ new AnswerOption { Text = "Fallback", IsCorrect = true, SortOrder = 1 }]}
                    ]},
                    new Lesson { Title = "Health Checks & Observability", Emoji = "🏥", SortOrder = 2, XpReward = 10, XpPerfectBonus = 5, Questions =
                    [
                        new Question { Type = QuestionType.MultipleChoice, SortOrder = 1, Prompt = "What are the three pillars of observability?", ExplanationText = "Logs (events), Metrics (numeric measurements), and Traces (request flow across services).", AnswerOptions = [ new AnswerOption { Text = "Logs, Metrics, and Traces", IsCorrect = true, SortOrder = 1 }, new AnswerOption { Text = "CPU, Memory, and Disk", IsCorrect = false, SortOrder = 2 }, new AnswerOption { Text = "Alerts, Dashboards, and Reports", IsCorrect = false, SortOrder = 3 }, new AnswerOption { Text = "Tests, Builds, and Deploys", IsCorrect = false, SortOrder = 4 }]},
                        new Question { Type = QuestionType.TrueFalse, SortOrder = 2, Prompt = "Distributed tracing tracks a request as it flows across multiple microservices.", ExplanationText = "True — tools like Jaeger and Zipkin correlate spans across services using trace IDs.", AnswerOptions = [ new AnswerOption { Text = "True", IsCorrect = true, SortOrder = 1 }, new AnswerOption { Text = "False", IsCorrect = false, SortOrder = 2 }]},
                        new Question { Type = QuestionType.FillInTheBlank, SortOrder = 3, Prompt = "A /health endpoint that returns HTTP 200 is called a ___ check", ExplanationText = "Health checks let load balancers and orchestrators know if a service is alive and ready.", HintText = "Service alive/ready", AnswerOptions = [ new AnswerOption { Text = "health", IsCorrect = true, SortOrder = 1 }]}
                    ]}
                ]}
            ]
        });
        db.SaveChanges();
    }
}
