# Implementation Plan — Roadmap-style Learning Tree

## Overview
Add a full roadmap.sh-style interactive learning tree to all 4 tracks (Docker, Kubernetes, ASP.NET Core, System Design). Each node is clickable and opens a rich detail panel with YouTube videos, interview questions, tips, and reference links.

---

## Architecture

### Data Layer — JSON files (no EF migration needed)
Content is static reference material, stored as JSON in `wwwroot/data/roadmaps/`.

```
wwwroot/data/roadmaps/
  docker.json
  kubernetes.json
  aspnet-core.json
  system-design.json
```

Each JSON file:
```json
{
  "slug": "docker",
  "title": "Docker Roadmap",
  "description": "...",
  "sections": [
    {
      "id": "introduction",
      "title": "Introduction",
      "order": 1,
      "topics": [
        {
          "id": "what-are-containers",
          "title": "What are Containers?",
          "description": "Markdown paragraph explaining the concept...",
          "isOptional": false,
          "youtubeVideos": [
            { "title": "...", "url": "https://youtube.com/...", "channel": "TechWorld with Nana", "duration": "12:34" }
          ],
          "resources": [
            { "title": "Official Docker Docs", "url": "https://docs.docker.com/...", "type": "official" },
            { "title": "Blog Post Title", "url": "https://...", "type": "article" }
          ],
          "interviewQuestions": [
            { "q": "What is a container?", "a": "A lightweight, isolated process that shares the host OS kernel..." }
          ],
          "tips": [
            "Always start with the official Docker Getting Started guide before diving into advanced topics."
          ],
          "subtopics": [ ...nested same structure... ]
        }
      ]
    }
  ]
}
```

### Service Layer
- **`IRoadmapService`** + **`RoadmapService`** — loads/caches JSON, finds topics by ID, returns section tree.

### Controller
- **`RoadmapController`**
  - `Index(slug)` → full roadmap page (GET `/roadmap/{slug}`)
  - `Topic(slug, topicId)` → topic detail JSON (GET `/roadmap/{slug}/topic/{topicId}` AJAX)

### Views
- `Views/Roadmap/Index.cshtml` — full-page roadmap tree with sidebar panel
- Reuses `_Layout.cshtml`

### CSS
- `wwwroot/css/roadmap.css` — roadmap tree layout, nodes, edges, panel, responsive

### JavaScript
- `wwwroot/js/roadmap.js` — click handlers, AJAX panel load, topic status tracking, smooth scroll

---

## Visual Design (roadmap.sh style)

```
    ┌─────────────────────────────┐
    │  🐳 DOCKER ROADMAP          │  ← hero
    │  Step-by-step guide...      │
    └──────────────┬──────────────┘
                   │                 ← central spine line
    ╔══════════════╧══════════════╗
    ║       INTRODUCTION          ║  ← section header (colored)
    ╚══════════════╤══════════════╝
        ┌──────────┼──────────┐
        │          │          │
   ┌────▼───┐ ┌───▼────┐ ┌───▼────┐
   │ What   │ │  Why   │ │ Bare   │  ← topic nodes (clickable)
   │ are    │ │ do we  │ │ Metal  │
   │Contrs? │ │ need?  │ │ vs VMs │
   └────────┘ └────────┘ └────────┘
                   │
    ╔══════════════╧══════════════╗
    ║    UNDERLYING TECHNOLOGIES  ║
    ╚══════════════╤══════════════╝
                  ...
```

**Side panel** slides in from the right when a topic is clicked:
```
┌────────────────────────────────────────┐
│  ✕                        ⬜ Not Started│
│                                        │
│  # What are Containers?                │
│  ─────────────────────                 │
│  A container is a lightweight...       │
│                                        │
│  📹 Learning Videos                    │
│  ├─ ▶ Docker in 100 Seconds (Fireship)│
│  ├─ ▶ Containers Explained (TechWorld) │
│  └─ ▶ Docker Crash Course (Traversy)   │
│                                        │
│  🎯 Interview Questions               │
│  ├─ Q: What is a container?           │
│  │  A: A lightweight isolated...       │
│  ├─ Q: Containers vs VMs?             │
│  │  A: Containers share the host...    │
│                                        │
│  💡 Pro Tips                           │
│  ├─ Start with official docs first     │
│  └─ Practice on play-with-docker.com   │
│                                        │
│  🔗 Resources                          │
│  ├─ 📖 Docker Docs (official)          │
│  ├─ 📰 DigitalOcean Tutorial (article) │
│  └─ 🎓 Docker Course (course)          │
│                                        │
│  [🎮 Practice Quiz →]  (links to      │
│                         existing lesson)│
└────────────────────────────────────────┘
```

---

## Content Scope (per roadmap.sh structure)

### Docker (~35 topics across 14 sections)
Introduction, Linux Fundamentals, Underlying Technologies, Installation, Basics, Data Persistence, 3rd Party Images, Building Images, Container Registries, Running Containers, Container Security, Docker CLI, Networking, Developer Experience, Deploying Containers

### Kubernetes (~45 topics across 14 sections)
Introduction, Containers, Setting Up, Running Applications (Pods/ReplicaSets/Deployments/StatefulSets/Jobs), Services & Networking, Config Management, Resource Management, Security, Monitoring & Logging, Autoscaling, Scheduling, Storage, Deployment Patterns, Advanced Topics, Cluster Operations

### ASP.NET Core (~55 topics across 16 sections)
C# Basics, General Dev Skills, Database Fundamentals, ASP.NET Core Basics, ORM (EF Core/Dapper), DI, Caching, Databases, Logging, API Clients, Real-Time, Object Mapping, Task Scheduling, Testing, Microservices, CI/CD

### System Design (~65 topics across 16 sections)
Fundamentals, CAP Theorem, Consistency/Availability Patterns, DNS, CDN, Load Balancers, Application Layer, Databases, Caching, Asynchronism, Communication, Performance Antipatterns, Monitoring, Cloud Design Patterns, Reliability Patterns

**Total: ~200 topics, each with 3-5 videos, 3-5 interview Q&As, 2-3 tips, 3-5 links**

---

## Files to Create / Modify

### New Files (create)
1. `Services/IRoadmapService.cs` — interface
2. `Services/RoadmapService.cs` — JSON loader + in-memory cache
3. `Controllers/RoadmapController.cs` — Index + Topic AJAX
4. `Views/Roadmap/Index.cshtml` — full roadmap page
5. `wwwroot/css/roadmap.css` — all roadmap + panel styling
6. `wwwroot/js/roadmap.js` — interactivity, AJAX panel, status tracking
7. `wwwroot/data/roadmaps/docker.json` — full Docker content
8. `wwwroot/data/roadmaps/kubernetes.json` — full Kubernetes content
9. `wwwroot/data/roadmaps/aspnet-core.json` — full ASP.NET Core content
10. `wwwroot/data/roadmaps/system-design.json` — full System Design content

### Modified Files
11. `Program.cs` — register RoadmapService, add `/roadmap/{slug}` route
12. `Views/Shared/_Layout.cshtml` — add "Roadmap" nav link
13. `Views/Home/Index.cshtml` — update track card buttons to link to roadmap

---

## Implementation Order

1. Create service layer (IRoadmapService + RoadmapService)
2. Create RoadmapController with route
3. Create roadmap.css (full visual design)
4. Create roadmap.js (interactivity)
5. Create Views/Roadmap/Index.cshtml
6. Create docker.json with FULL content (search web for real URLs)
7. Create kubernetes.json with FULL content
8. Create aspnet-core.json with FULL content
9. Create system-design.json with FULL content
10. Update Program.cs, _Layout, Home/Index for navigation
11. Build + test all 4 roadmaps in browser

---

## Topic Status Tracking
Reuse existing session-based GuestProgress. Add a new dictionary:
```csharp
// In GuestProgress model:
public Dictionary<string, string> TopicStatus { get; set; } = new();
// Key: "docker:what-are-containers", Value: "done" | "in_progress" | "skip"
```
This allows roadmap progress to persist in the session alongside quiz progress.
