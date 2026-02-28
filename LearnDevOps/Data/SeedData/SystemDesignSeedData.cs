using LearnDevOps.Models.Domain;

namespace LearnDevOps.Data.SeedData;

public class SystemDesignSeedData : ITrackSeedData
{
    public void Seed(AppDbContext db)
    {
        if (db.Tracks.Any(t => t.Slug == "system-design")) return;

        db.Tracks.Add(new Track
        {
            Name = "System Design",
            Slug = "system-design",
            Description = "Learn scalability, caching, databases, messaging, and distributed systems.",
            Emoji = "🏗️",
            ColorHex = "#6C5CE7",
            SortOrder = 6,
            UnlockThresholdPercent = 0,
            Units =
            [
                new Unit { Title = "Scalability", Emoji = "📈", SortOrder = 1, Description = "Horizontal vs vertical scaling and fundamentals.", Lessons =
                [
                    new Lesson { Title = "Scaling Fundamentals", Emoji = "⚖️", SortOrder = 1, XpReward = 10, XpPerfectBonus = 5, Questions =
                    [
                        new Question { Type = QuestionType.MultipleChoice, SortOrder = 1, Prompt = "What is horizontal scaling?", ExplanationText = "Horizontal scaling (scale out) adds more machines. Vertical scaling (scale up) adds resources to one machine.", AnswerOptions = [ new AnswerOption { Text = "Adding more machines to distribute load", IsCorrect = true, SortOrder = 1 }, new AnswerOption { Text = "Adding more CPU/RAM to one machine", IsCorrect = false, SortOrder = 2 }, new AnswerOption { Text = "Optimizing code to run faster", IsCorrect = false, SortOrder = 3 }, new AnswerOption { Text = "Using a CDN for static assets", IsCorrect = false, SortOrder = 4 }]},
                        new Question { Type = QuestionType.TrueFalse, SortOrder = 2, Prompt = "The CAP theorem states you can only guarantee 2 out of 3: Consistency, Availability, Partition tolerance.", ExplanationText = "True — in a distributed system during a network partition, you must choose between consistency and availability.", AnswerOptions = [ new AnswerOption { Text = "True", IsCorrect = true, SortOrder = 1 }, new AnswerOption { Text = "False", IsCorrect = false, SortOrder = 2 }]},
                        new Question { Type = QuestionType.FillInTheBlank, SortOrder = 3, Prompt = "A ___ balancer distributes incoming requests across multiple servers", ExplanationText = "A load balancer sits in front of servers and distributes traffic using algorithms like round-robin or least connections.", HintText = "Distributes traffic", AnswerOptions = [ new AnswerOption { Text = "load", IsCorrect = true, SortOrder = 1 }]}
                    ]},
                    new Lesson { Title = "Load Balancing", Emoji = "⚡", SortOrder = 2, XpReward = 10, XpPerfectBonus = 5, Questions =
                    [
                        new Question { Type = QuestionType.MultipleChoice, SortOrder = 1, Prompt = "Which load balancing algorithm sends requests to each server in turn?", ExplanationText = "Round-robin sends requests sequentially to each server in order, cycling back to the first.", AnswerOptions = [ new AnswerOption { Text = "Round-robin", IsCorrect = true, SortOrder = 1 }, new AnswerOption { Text = "Random", IsCorrect = false, SortOrder = 2 }, new AnswerOption { Text = "Weighted hash", IsCorrect = false, SortOrder = 3 }, new AnswerOption { Text = "Priority queue", IsCorrect = false, SortOrder = 4 }]},
                        new Question { Type = QuestionType.TrueFalse, SortOrder = 2, Prompt = "Consistent hashing minimises redistribution when nodes are added or removed.", ExplanationText = "True — only K/n keys need remapping (K keys, n nodes) vs rehashing everything in modular hashing.", AnswerOptions = [ new AnswerOption { Text = "True", IsCorrect = true, SortOrder = 1 }, new AnswerOption { Text = "False", IsCorrect = false, SortOrder = 2 }]},
                        new Question { Type = QuestionType.MultipleChoice, SortOrder = 3, Prompt = "What is a CDN?", ExplanationText = "A Content Delivery Network caches content at edge locations worldwide to reduce latency for users.", AnswerOptions = [ new AnswerOption { Text = "A network of edge servers caching content globally", IsCorrect = true, SortOrder = 1 }, new AnswerOption { Text = "A central database cluster", IsCorrect = false, SortOrder = 2 }, new AnswerOption { Text = "A container deployment network", IsCorrect = false, SortOrder = 3 }, new AnswerOption { Text = "A cloud DNS service", IsCorrect = false, SortOrder = 4 }]}
                    ]}
                ]},
                new Unit { Title = "Data & Caching", Emoji = "🗄️", SortOrder = 2, Description = "Databases, caching strategies, and trade-offs.", Lessons =
                [
                    new Lesson { Title = "SQL vs NoSQL", Emoji = "💾", SortOrder = 1, XpReward = 10, XpPerfectBonus = 5, Questions =
                    [
                        new Question { Type = QuestionType.MultipleChoice, SortOrder = 1, Prompt = "When should you choose a NoSQL database?", ExplanationText = "NoSQL is ideal for unstructured data, horizontal scaling, flexible schemas, and high write throughput scenarios.", AnswerOptions = [ new AnswerOption { Text = "Flexible schema, high write throughput, horizontal scaling needs", IsCorrect = true, SortOrder = 1 }, new AnswerOption { Text = "Complex joins and ACID transactions", IsCorrect = false, SortOrder = 2 }, new AnswerOption { Text = "Strict data integrity requirements", IsCorrect = false, SortOrder = 3 }, new AnswerOption { Text = "Small datasets under 1GB", IsCorrect = false, SortOrder = 4 }]},
                        new Question { Type = QuestionType.FillInTheBlank, SortOrder = 2, Prompt = "Database ___ splits data across multiple servers by a partition key", ExplanationText = "Sharding distributes rows/documents across shards based on a shard key for horizontal scalability.", HintText = "Splitting data horizontally", AnswerOptions = [ new AnswerOption { Text = "sharding", IsCorrect = true, SortOrder = 1 }]},
                        new Question { Type = QuestionType.TrueFalse, SortOrder = 3, Prompt = "Database replication creates copies of data on multiple servers for redundancy.", ExplanationText = "True — replication improves read performance and fault tolerance. Primary handles writes, replicas handle reads.", AnswerOptions = [ new AnswerOption { Text = "True", IsCorrect = true, SortOrder = 1 }, new AnswerOption { Text = "False", IsCorrect = false, SortOrder = 2 }]}
                    ]},
                    new Lesson { Title = "Caching Strategies", Emoji = "⚡", SortOrder = 2, XpReward = 10, XpPerfectBonus = 5, Questions =
                    [
                        new Question { Type = QuestionType.MultipleChoice, SortOrder = 1, Prompt = "What is cache-aside (lazy loading) strategy?", ExplanationText = "Read: check cache first, on miss fetch from DB and populate cache. Write: update DB only, invalidate cache.", AnswerOptions = [ new AnswerOption { Text = "App checks cache first, fills it on miss from the database", IsCorrect = true, SortOrder = 1 }, new AnswerOption { Text = "Every write goes to cache first, then DB", IsCorrect = false, SortOrder = 2 }, new AnswerOption { Text = "Cache is pre-loaded with all data at startup", IsCorrect = false, SortOrder = 3 }, new AnswerOption { Text = "Database handles caching transparently", IsCorrect = false, SortOrder = 4 }]},
                        new Question { Type = QuestionType.TrueFalse, SortOrder = 2, Prompt = "Cache invalidation is considered one of the hardest problems in computer science.", ExplanationText = "True — Phil Karlton's famous quote: 'There are only two hard things in CS: cache invalidation and naming things.'", AnswerOptions = [ new AnswerOption { Text = "True", IsCorrect = true, SortOrder = 1 }, new AnswerOption { Text = "False", IsCorrect = false, SortOrder = 2 }]},
                        new Question { Type = QuestionType.FillInTheBlank, SortOrder = 3, Prompt = "___ is a popular open-source in-memory data store used for caching", ExplanationText = "Redis is an in-memory key-value store supporting strings, hashes, lists, sets, and sorted sets.", HintText = "Popular in-memory store", AnswerOptions = [ new AnswerOption { Text = "Redis", IsCorrect = true, SortOrder = 1 }]}
                    ]}
                ]},
                new Unit { Title = "Distributed Systems", Emoji = "🔗", SortOrder = 3, Description = "Messaging, queues, and distributed patterns.", Lessons =
                [
                    new Lesson { Title = "Message Queues", Emoji = "📬", SortOrder = 1, XpReward = 10, XpPerfectBonus = 5, Questions =
                    [
                        new Question { Type = QuestionType.MultipleChoice, SortOrder = 1, Prompt = "What is the publish-subscribe (pub/sub) pattern?", ExplanationText = "Publishers send messages to a topic. All subscribers to that topic receive a copy. Decouples senders from receivers.", AnswerOptions = [ new AnswerOption { Text = "Publishers send to topics; all subscribers get a copy", IsCorrect = true, SortOrder = 1 }, new AnswerOption { Text = "One producer sends to one consumer only", IsCorrect = false, SortOrder = 2 }, new AnswerOption { Text = "Messages are stored in a database table", IsCorrect = false, SortOrder = 3 }, new AnswerOption { Text = "Direct RPC calls between services", IsCorrect = false, SortOrder = 4 }]},
                        new Question { Type = QuestionType.FillInTheBlank, SortOrder = 2, Prompt = "Apache ___ is a distributed event streaming platform for high-throughput messaging", ExplanationText = "Kafka handles millions of events per second with durable, ordered, partitioned log storage.", HintText = "Named after a famous author", AnswerOptions = [ new AnswerOption { Text = "Kafka", IsCorrect = true, SortOrder = 1 }]},
                        new Question { Type = QuestionType.TrueFalse, SortOrder = 3, Prompt = "Message queues help achieve eventual consistency between microservices.", ExplanationText = "True — async messaging means services update independently and converge to consistent state over time.", AnswerOptions = [ new AnswerOption { Text = "True", IsCorrect = true, SortOrder = 1 }, new AnswerOption { Text = "False", IsCorrect = false, SortOrder = 2 }]}
                    ]},
                    new Lesson { Title = "Consistency & Consensus", Emoji = "🤝", SortOrder = 2, XpReward = 10, XpPerfectBonus = 5, Questions =
                    [
                        new Question { Type = QuestionType.MultipleChoice, SortOrder = 1, Prompt = "What is eventual consistency?", ExplanationText = "Given enough time without new updates, all replicas will converge to the same value. Trades immediate consistency for availability.", AnswerOptions = [ new AnswerOption { Text = "All replicas converge to the same value given enough time", IsCorrect = true, SortOrder = 1 }, new AnswerOption { Text = "All reads always return the latest write", IsCorrect = false, SortOrder = 2 }, new AnswerOption { Text = "Data is never consistent across nodes", IsCorrect = false, SortOrder = 3 }, new AnswerOption { Text = "Consistency is guaranteed by locking all nodes", IsCorrect = false, SortOrder = 4 }]},
                        new Question { Type = QuestionType.TrueFalse, SortOrder = 2, Prompt = "Strong consistency means every read returns the most recent write.", ExplanationText = "True — strong consistency ensures linearizability. More expensive in distributed systems but simpler to reason about.", AnswerOptions = [ new AnswerOption { Text = "True", IsCorrect = true, SortOrder = 1 }, new AnswerOption { Text = "False", IsCorrect = false, SortOrder = 2 }]},
                        new Question { Type = QuestionType.MultipleChoice, SortOrder = 3, Prompt = "What does the Saga pattern solve in microservices?", ExplanationText = "Sagas manage distributed transactions by chaining local transactions with compensating actions for rollback.", AnswerOptions = [ new AnswerOption { Text = "Distributed transactions across multiple services", IsCorrect = true, SortOrder = 1 }, new AnswerOption { Text = "Service discovery", IsCorrect = false, SortOrder = 2 }, new AnswerOption { Text = "Load balancing", IsCorrect = false, SortOrder = 3 }, new AnswerOption { Text = "API versioning", IsCorrect = false, SortOrder = 4 }]}
                    ]}
                ]}
            ]
        });
        db.SaveChanges();
    }
}
