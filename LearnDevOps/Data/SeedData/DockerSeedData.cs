using LearnDevOps.Models.Domain;

namespace LearnDevOps.Data.SeedData;

public class DockerSeedData : ITrackSeedData
{
    public void Seed(AppDbContext db)
    {
        if (db.Tracks.Any(t => t.Slug == "docker")) return;

        db.Tracks.Add(new Track
        {
            Name = "Docker",
            Slug = "docker",
            Description = "Master containerisation from images and containers to networking and volumes.",
            Emoji = "🐳",
            ColorHex = "#0db7ed",
            SortOrder = 1,
            PrerequisiteTrackId = null,
            UnlockThresholdPercent = 0,
            Units =
            [
                new Unit { Title = "Container Basics", Emoji = "📦", SortOrder = 1, Description = "Understand what containers are and how they differ from VMs.", Lessons =
                [
                    new Lesson { Title = "What Is a Container?", Emoji = "🤔", SortOrder = 1, XpReward = 10, XpPerfectBonus = 5, Description = "Concepts: containers vs VMs, isolation, portability.", Questions =
                    [
                        new Question { Type = QuestionType.MultipleChoice, SortOrder = 1, Prompt = "What best describes a Docker container?", ExplanationText = "A container is a lightweight, isolated process that shares the host OS kernel — unlike VMs which include a full OS.", AnswerOptions =
                        [
                            new AnswerOption { Text = "A lightweight isolated process sharing the host OS kernel", IsCorrect = true, SortOrder = 1 },
                            new AnswerOption { Text = "A full virtual machine with its own OS kernel", IsCorrect = false, SortOrder = 2 },
                            new AnswerOption { Text = "A zip archive of application source code", IsCorrect = false, SortOrder = 3 },
                            new AnswerOption { Text = "A cloud-hosted server instance", IsCorrect = false, SortOrder = 4 }
                        ]},
                        new Question { Type = QuestionType.TrueFalse, SortOrder = 2, Prompt = "Containers include a full copy of the operating system kernel.", ExplanationText = "False — containers share the host OS kernel. This is what makes them lighter than VMs.", AnswerOptions =
                        [
                            new AnswerOption { Text = "True", IsCorrect = false, SortOrder = 1 },
                            new AnswerOption { Text = "False", IsCorrect = true, SortOrder = 2 }
                        ]},
                        new Question { Type = QuestionType.MultipleChoice, SortOrder = 3, Prompt = "Which of these is a key benefit of containers over VMs?", ExplanationText = "Containers start in milliseconds and are much smaller than VMs because they don't bundle a full OS.", AnswerOptions =
                        [
                            new AnswerOption { Text = "Faster startup and smaller footprint", IsCorrect = true, SortOrder = 1 },
                            new AnswerOption { Text = "Better hardware isolation", IsCorrect = false, SortOrder = 2 },
                            new AnswerOption { Text = "Stronger security boundaries", IsCorrect = false, SortOrder = 3 },
                            new AnswerOption { Text = "No need for networking", IsCorrect = false, SortOrder = 4 }
                        ]},
                        new Question { Type = QuestionType.TrueFalse, SortOrder = 4, Prompt = "Docker is the only container runtime available.", ExplanationText = "False — other runtimes include Podman, containerd, and CRI-O. Docker is simply the most widely known.", AnswerOptions =
                        [
                            new AnswerOption { Text = "True", IsCorrect = false, SortOrder = 1 },
                            new AnswerOption { Text = "False", IsCorrect = true, SortOrder = 2 }
                        ]},
                        new Question { Type = QuestionType.MultipleChoice, SortOrder = 5, Prompt = "What is a Docker image?", ExplanationText = "An image is a read-only template used to create containers. Think of it as a class, while a container is an instance.", AnswerOptions =
                        [
                            new AnswerOption { Text = "A read-only template from which containers are created", IsCorrect = true, SortOrder = 1 },
                            new AnswerOption { Text = "A running process on the host", IsCorrect = false, SortOrder = 2 },
                            new AnswerOption { Text = "A network configuration file", IsCorrect = false, SortOrder = 3 },
                            new AnswerOption { Text = "A snapshot of a virtual machine disk", IsCorrect = false, SortOrder = 4 }
                        ]}
                    ]},
                    new Lesson { Title = "Images vs Containers", Emoji = "🖼️", SortOrder = 2, XpReward = 10, XpPerfectBonus = 5, Description = "The relationship between images and running containers.", Questions =
                    [
                        new Question { Type = QuestionType.MultipleChoice, SortOrder = 1, Prompt = "How many containers can be created from a single image?", ExplanationText = "One image can spawn many containers simultaneously — like a class instantiated multiple times.", AnswerOptions =
                        [
                            new AnswerOption { Text = "Many", IsCorrect = true, SortOrder = 1 },
                            new AnswerOption { Text = "Only one", IsCorrect = false, SortOrder = 2 },
                            new AnswerOption { Text = "Two — one dev, one prod", IsCorrect = false, SortOrder = 3 },
                            new AnswerOption { Text = "Unlimited, but only on the same host", IsCorrect = false, SortOrder = 4 }
                        ]},
                        new Question { Type = QuestionType.TrueFalse, SortOrder = 2, Prompt = "Docker images are mutable — you can edit them while they are running.", ExplanationText = "False — images are read-only. Changes are written to a container's writable layer. To persist changes, you create a new image.", AnswerOptions =
                        [
                            new AnswerOption { Text = "True", IsCorrect = false, SortOrder = 1 },
                            new AnswerOption { Text = "False", IsCorrect = true, SortOrder = 2 }
                        ]},
                        new Question { Type = QuestionType.FillInTheBlank, SortOrder = 3, Prompt = "Complete the command to list all running containers: docker ___", ExplanationText = "'docker ps' lists running containers. Add -a to include stopped ones.", HintText = "Think: process status", AnswerOptions =
                        [
                            new AnswerOption { Text = "ps", IsCorrect = true, SortOrder = 1 }
                        ]},
                        new Question { Type = QuestionType.MultipleChoice, SortOrder = 4, Prompt = "What happens to a container's filesystem changes when the container is removed?", ExplanationText = "By default, container filesystem changes (writable layer) are lost on removal. Use volumes to persist data.", AnswerOptions =
                        [
                            new AnswerOption { Text = "They are lost unless a volume was used", IsCorrect = true, SortOrder = 1 },
                            new AnswerOption { Text = "They are saved back to the image", IsCorrect = false, SortOrder = 2 },
                            new AnswerOption { Text = "They are backed up automatically", IsCorrect = false, SortOrder = 3 },
                            new AnswerOption { Text = "They remain on the host filesystem", IsCorrect = false, SortOrder = 4 }
                        ]},
                        new Question { Type = QuestionType.TrueFalse, SortOrder = 5, Prompt = "docker images and docker image ls show the same result.", ExplanationText = "True — 'docker images' is an alias for 'docker image ls'. Both list locally available images.", AnswerOptions =
                        [
                            new AnswerOption { Text = "True", IsCorrect = true, SortOrder = 1 },
                            new AnswerOption { Text = "False", IsCorrect = false, SortOrder = 2 }
                        ]}
                    ]},
                    new Lesson { Title = "Docker Hub & Registries", Emoji = "🏪", SortOrder = 3, XpReward = 10, XpPerfectBonus = 5, Description = "Where images live — Docker Hub and private registries.", Questions =
                    [
                        new Question { Type = QuestionType.MultipleChoice, SortOrder = 1, Prompt = "What is Docker Hub?", ExplanationText = "Docker Hub is the default public registry for Docker images. You can pull official images and push your own.", AnswerOptions =
                        [
                            new AnswerOption { Text = "The default public registry for Docker images", IsCorrect = true, SortOrder = 1 },
                            new AnswerOption { Text = "A Docker installation dashboard", IsCorrect = false, SortOrder = 2 },
                            new AnswerOption { Text = "Docker's cloud compute service", IsCorrect = false, SortOrder = 3 },
                            new AnswerOption { Text = "A container monitoring tool", IsCorrect = false, SortOrder = 4 }
                        ]},
                        new Question { Type = QuestionType.FillInTheBlank, SortOrder = 2, Prompt = "Complete the command to pull the official nginx image: docker ___ nginx", ExplanationText = "'docker pull nginx' downloads the nginx image from Docker Hub.", HintText = "You're fetching the image", AnswerOptions =
                        [
                            new AnswerOption { Text = "pull", IsCorrect = true, SortOrder = 1 }
                        ]},
                        new Question { Type = QuestionType.TrueFalse, SortOrder = 3, Prompt = "A Docker tag is used to version images (e.g., nginx:1.25).", ExplanationText = "True — tags label specific versions. 'latest' is the default tag if none is specified.", AnswerOptions =
                        [
                            new AnswerOption { Text = "True", IsCorrect = true, SortOrder = 1 },
                            new AnswerOption { Text = "False", IsCorrect = false, SortOrder = 2 }
                        ]},
                        new Question { Type = QuestionType.MultipleChoice, SortOrder = 4, Prompt = "Which command pushes an image to a registry?", ExplanationText = "'docker push' uploads a tagged image to the specified registry. You must be logged in first with 'docker login'.", AnswerOptions =
                        [
                            new AnswerOption { Text = "docker push", IsCorrect = true, SortOrder = 1 },
                            new AnswerOption { Text = "docker upload", IsCorrect = false, SortOrder = 2 },
                            new AnswerOption { Text = "docker publish", IsCorrect = false, SortOrder = 3 },
                            new AnswerOption { Text = "docker send", IsCorrect = false, SortOrder = 4 }
                        ]},
                        new Question { Type = QuestionType.MultipleChoice, SortOrder = 5, Prompt = "What does 'docker pull ubuntu:22.04' do?", ExplanationText = "It downloads the ubuntu image with the specific tag '22.04' from Docker Hub to your local machine.", AnswerOptions =
                        [
                            new AnswerOption { Text = "Downloads ubuntu version 22.04 from Docker Hub", IsCorrect = true, SortOrder = 1 },
                            new AnswerOption { Text = "Runs ubuntu 22.04 as a container", IsCorrect = false, SortOrder = 2 },
                            new AnswerOption { Text = "Builds an ubuntu 22.04 image", IsCorrect = false, SortOrder = 3 },
                            new AnswerOption { Text = "Updates a running ubuntu container", IsCorrect = false, SortOrder = 4 }
                        ]}
                    ]},
                    new Lesson { Title = "Container Lifecycle", Emoji = "🔄", SortOrder = 4, XpReward = 10, XpPerfectBonus = 5, Description = "Creating, starting, stopping and removing containers.", Questions =
                    [
                        new Question { Type = QuestionType.MultipleChoice, SortOrder = 1, Prompt = "What does 'docker run -d nginx' do?", ExplanationText = "The -d flag runs the container in detached (background) mode so your terminal remains free.", AnswerOptions =
                        [
                            new AnswerOption { Text = "Runs nginx container in the background (detached mode)", IsCorrect = true, SortOrder = 1 },
                            new AnswerOption { Text = "Deletes the nginx container", IsCorrect = false, SortOrder = 2 },
                            new AnswerOption { Text = "Runs nginx in debug mode", IsCorrect = false, SortOrder = 3 },
                            new AnswerOption { Text = "Downloads nginx and exits immediately", IsCorrect = false, SortOrder = 4 }
                        ]},
                        new Question { Type = QuestionType.FillInTheBlank, SortOrder = 2, Prompt = "Complete: docker ___ <container_id>  (to stop a running container)", ExplanationText = "'docker stop' sends SIGTERM then SIGKILL to gracefully stop a container.", HintText = "Graceful shutdown", AnswerOptions =
                        [
                            new AnswerOption { Text = "stop", IsCorrect = true, SortOrder = 1 }
                        ]},
                        new Question { Type = QuestionType.TrueFalse, SortOrder = 3, Prompt = "A stopped container is permanently deleted from the system.", ExplanationText = "False — stopped containers still exist and can be restarted with 'docker start'. Use 'docker rm' to delete.", AnswerOptions =
                        [
                            new AnswerOption { Text = "True", IsCorrect = false, SortOrder = 1 },
                            new AnswerOption { Text = "False", IsCorrect = true, SortOrder = 2 }
                        ]},
                        new Question { Type = QuestionType.MultipleChoice, SortOrder = 4, Prompt = "Which flag in 'docker run' automatically removes the container when it exits?", ExplanationText = "'--rm' tells Docker to remove the container's filesystem when it stops. Useful for one-off tasks.", AnswerOptions =
                        [
                            new AnswerOption { Text = "--rm", IsCorrect = true, SortOrder = 1 },
                            new AnswerOption { Text = "--delete", IsCorrect = false, SortOrder = 2 },
                            new AnswerOption { Text = "--clean", IsCorrect = false, SortOrder = 3 },
                            new AnswerOption { Text = "--purge", IsCorrect = false, SortOrder = 4 }
                        ]},
                        new Question { Type = QuestionType.MultipleChoice, SortOrder = 5, Prompt = "How do you open an interactive shell in a new container?", ExplanationText = "'docker run -it ubuntu bash' runs ubuntu interactively (-i keeps stdin open, -t allocates a pseudo-TTY).", AnswerOptions =
                        [
                            new AnswerOption { Text = "docker run -it ubuntu bash", IsCorrect = true, SortOrder = 1 },
                            new AnswerOption { Text = "docker run ubuntu --shell", IsCorrect = false, SortOrder = 2 },
                            new AnswerOption { Text = "docker exec ubuntu bash", IsCorrect = false, SortOrder = 3 },
                            new AnswerOption { Text = "docker attach ubuntu", IsCorrect = false, SortOrder = 4 }
                        ]}
                    ]}
                ]},
                new Unit { Title = "Docker Commands", Emoji = "⌨️", SortOrder = 2, Description = "Master essential Docker CLI commands.", Lessons =
                [
                    new Lesson { Title = "Build & Run", Emoji = "🏗️", SortOrder = 1, XpReward = 10, XpPerfectBonus = 5, Description = "Building images and running containers.", Questions =
                    [
                        new Question { Type = QuestionType.FillInTheBlank, SortOrder = 1, Prompt = "Complete: docker ___ -t myapp:v1 . (to build an image from current directory)", ExplanationText = "'docker build' builds an image from a Dockerfile. -t names and optionally tags the image.", HintText = "You're constructing the image", AnswerOptions =
                        [
                            new AnswerOption { Text = "build", IsCorrect = true, SortOrder = 1 }
                        ]},
                        new Question { Type = QuestionType.MultipleChoice, SortOrder = 2, Prompt = "What does the -p 8080:80 flag do in 'docker run -p 8080:80 nginx'?", ExplanationText = "Port mapping: host port 8080 maps to container port 80. Traffic to localhost:8080 reaches the container's port 80.", AnswerOptions =
                        [
                            new AnswerOption { Text = "Maps host port 8080 to container port 80", IsCorrect = true, SortOrder = 1 },
                            new AnswerOption { Text = "Maps container port 8080 to host port 80", IsCorrect = false, SortOrder = 2 },
                            new AnswerOption { Text = "Exposes both ports 8080 and 80 on the host", IsCorrect = false, SortOrder = 3 },
                            new AnswerOption { Text = "Runs nginx on port 8080 inside the container", IsCorrect = false, SortOrder = 4 }
                        ]},
                        new Question { Type = QuestionType.MultipleChoice, SortOrder = 3, Prompt = "Which command shows logs of a running container?", ExplanationText = "'docker logs <id>' prints STDOUT/STDERR. Add -f to follow in real-time.", AnswerOptions =
                        [
                            new AnswerOption { Text = "docker logs", IsCorrect = true, SortOrder = 1 },
                            new AnswerOption { Text = "docker output", IsCorrect = false, SortOrder = 2 },
                            new AnswerOption { Text = "docker print", IsCorrect = false, SortOrder = 3 },
                            new AnswerOption { Text = "docker inspect --logs", IsCorrect = false, SortOrder = 4 }
                        ]},
                        new Question { Type = QuestionType.TrueFalse, SortOrder = 4, Prompt = "'docker exec' can run commands inside a running container.", ExplanationText = "True — 'docker exec -it <id> bash' opens a shell in an already-running container.", AnswerOptions =
                        [
                            new AnswerOption { Text = "True", IsCorrect = true, SortOrder = 1 },
                            new AnswerOption { Text = "False", IsCorrect = false, SortOrder = 2 }
                        ]},
                        new Question { Type = QuestionType.MultipleChoice, SortOrder = 5, Prompt = "Which command removes all stopped containers?", ExplanationText = "'docker container prune' removes all stopped containers at once. 'docker rm <id>' removes a specific one.", AnswerOptions =
                        [
                            new AnswerOption { Text = "docker container prune", IsCorrect = true, SortOrder = 1 },
                            new AnswerOption { Text = "docker rm --all", IsCorrect = false, SortOrder = 2 },
                            new AnswerOption { Text = "docker delete stopped", IsCorrect = false, SortOrder = 3 },
                            new AnswerOption { Text = "docker clean", IsCorrect = false, SortOrder = 4 }
                        ]}
                    ]},
                    new Lesson { Title = "Inspect & Debug", Emoji = "🔍", SortOrder = 2, XpReward = 10, XpPerfectBonus = 5, Description = "Inspecting containers and debugging issues.", Questions =
                    [
                        new Question { Type = QuestionType.MultipleChoice, SortOrder = 1, Prompt = "What does 'docker inspect <container>' return?", ExplanationText = "'docker inspect' returns detailed JSON metadata about a container including IP, mounts, env vars, and config.", AnswerOptions =
                        [
                            new AnswerOption { Text = "Detailed JSON metadata about the container", IsCorrect = true, SortOrder = 1 },
                            new AnswerOption { Text = "A list of running processes inside the container", IsCorrect = false, SortOrder = 2 },
                            new AnswerOption { Text = "The container's live resource usage", IsCorrect = false, SortOrder = 3 },
                            new AnswerOption { Text = "The container's filesystem changes", IsCorrect = false, SortOrder = 4 }
                        ]},
                        new Question { Type = QuestionType.FillInTheBlank, SortOrder = 2, Prompt = "Complete: docker ___ <container>  (to see CPU and memory usage in real-time)", ExplanationText = "'docker stats' shows live CPU, memory, network I/O stats for running containers.", HintText = "Like Linux 'top' for containers", AnswerOptions =
                        [
                            new AnswerOption { Text = "stats", IsCorrect = true, SortOrder = 1 }
                        ]},
                        new Question { Type = QuestionType.MultipleChoice, SortOrder = 3, Prompt = "Which command shows running processes inside a container?", ExplanationText = "'docker top <container>' lists processes running inside the container, similar to the 'ps' command.", AnswerOptions =
                        [
                            new AnswerOption { Text = "docker top", IsCorrect = true, SortOrder = 1 },
                            new AnswerOption { Text = "docker ps --inside", IsCorrect = false, SortOrder = 2 },
                            new AnswerOption { Text = "docker processes", IsCorrect = false, SortOrder = 3 },
                            new AnswerOption { Text = "docker exec ps", IsCorrect = false, SortOrder = 4 }
                        ]},
                        new Question { Type = QuestionType.TrueFalse, SortOrder = 4, Prompt = "'docker diff <container>' shows filesystem changes made to a container.", ExplanationText = "True — 'docker diff' lists files added (A), changed (C), or deleted (D) in the container's writable layer.", AnswerOptions =
                        [
                            new AnswerOption { Text = "True", IsCorrect = true, SortOrder = 1 },
                            new AnswerOption { Text = "False", IsCorrect = false, SortOrder = 2 }
                        ]},
                        new Question { Type = QuestionType.MultipleChoice, SortOrder = 5, Prompt = "How do you copy a file FROM a container to the host?", ExplanationText = "'docker cp <container>:/path/file ./dest' copies files between host and container in either direction.", AnswerOptions =
                        [
                            new AnswerOption { Text = "docker cp <container>:/path/file ./dest", IsCorrect = true, SortOrder = 1 },
                            new AnswerOption { Text = "docker export <container> ./dest/file", IsCorrect = false, SortOrder = 2 },
                            new AnswerOption { Text = "docker pull <container> ./file", IsCorrect = false, SortOrder = 3 },
                            new AnswerOption { Text = "docker get <container>:/path/file", IsCorrect = false, SortOrder = 4 }
                        ]}
                    ]},
                    new Lesson { Title = "Image Management", Emoji = "🗃️", SortOrder = 3, XpReward = 10, XpPerfectBonus = 5, Description = "Managing local images: tag, remove, save.", Questions =
                    [
                        new Question { Type = QuestionType.FillInTheBlank, SortOrder = 1, Prompt = "Complete: docker ___ myapp:v1 myapp:latest (to create a new tag for an existing image)", ExplanationText = "'docker tag' creates a new tag pointing to an existing image. No data is copied.", HintText = "You're labelling the image", AnswerOptions =
                        [
                            new AnswerOption { Text = "tag", IsCorrect = true, SortOrder = 1 }
                        ]},
                        new Question { Type = QuestionType.MultipleChoice, SortOrder = 2, Prompt = "Which command removes a local image?", ExplanationText = "'docker rmi <image>' or 'docker image rm <image>' removes local images. You cannot remove an image with running containers.", AnswerOptions =
                        [
                            new AnswerOption { Text = "docker rmi", IsCorrect = true, SortOrder = 1 },
                            new AnswerOption { Text = "docker rm --image", IsCorrect = false, SortOrder = 2 },
                            new AnswerOption { Text = "docker delete", IsCorrect = false, SortOrder = 3 },
                            new AnswerOption { Text = "docker remove image", IsCorrect = false, SortOrder = 4 }
                        ]},
                        new Question { Type = QuestionType.TrueFalse, SortOrder = 3, Prompt = "Docker images are stored in layers, and layers are shared between images.", ExplanationText = "True — Docker uses a union filesystem. Shared layers are reused, saving disk space and speeding up pulls.", AnswerOptions =
                        [
                            new AnswerOption { Text = "True", IsCorrect = true, SortOrder = 1 },
                            new AnswerOption { Text = "False", IsCorrect = false, SortOrder = 2 }
                        ]},
                        new Question { Type = QuestionType.MultipleChoice, SortOrder = 4, Prompt = "What does 'docker image prune' do?", ExplanationText = "'docker image prune' removes dangling images (untagged layers not referenced by any container). Add -a to remove all unused images.", AnswerOptions =
                        [
                            new AnswerOption { Text = "Removes dangling (untagged, unreferenced) images", IsCorrect = true, SortOrder = 1 },
                            new AnswerOption { Text = "Removes all images from Docker Hub", IsCorrect = false, SortOrder = 2 },
                            new AnswerOption { Text = "Removes running containers", IsCorrect = false, SortOrder = 3 },
                            new AnswerOption { Text = "Compresses all local images", IsCorrect = false, SortOrder = 4 }
                        ]},
                        new Question { Type = QuestionType.FillInTheBlank, SortOrder = 5, Prompt = "Complete: docker system ___ (to remove all unused data: containers, networks, images)", ExplanationText = "'docker system prune' is the nuclear option — removes all stopped containers, unused networks, dangling images, and optionally volumes.", HintText = "Clean everything up", AnswerOptions =
                        [
                            new AnswerOption { Text = "prune", IsCorrect = true, SortOrder = 1 }
                        ]}
                    ]},
                    new Lesson { Title = "Environment Variables", Emoji = "🌱", SortOrder = 4, XpReward = 10, XpPerfectBonus = 5, Description = "Passing configuration into containers via env vars.", Questions =
                    [
                        new Question { Type = QuestionType.MultipleChoice, SortOrder = 1, Prompt = "How do you pass an environment variable to a container?", ExplanationText = "The -e flag sets environment variables. You can also use --env-file to load from a file.", AnswerOptions =
                        [
                            new AnswerOption { Text = "docker run -e MY_VAR=value myimage", IsCorrect = true, SortOrder = 1 },
                            new AnswerOption { Text = "docker run --var MY_VAR=value myimage", IsCorrect = false, SortOrder = 2 },
                            new AnswerOption { Text = "docker run --config MY_VAR=value myimage", IsCorrect = false, SortOrder = 3 },
                            new AnswerOption { Text = "docker run --set MY_VAR=value myimage", IsCorrect = false, SortOrder = 4 }
                        ]},
                        new Question { Type = QuestionType.TrueFalse, SortOrder = 2, Prompt = "Environment variables set with -e are visible to all processes inside the container.", ExplanationText = "True — all processes spawned inside the container inherit environment variables passed via -e.", AnswerOptions =
                        [
                            new AnswerOption { Text = "True", IsCorrect = true, SortOrder = 1 },
                            new AnswerOption { Text = "False", IsCorrect = false, SortOrder = 2 }
                        ]},
                        new Question { Type = QuestionType.FillInTheBlank, SortOrder = 3, Prompt = "Complete: docker run --env-___ .env myimage (to load env vars from a file)", ExplanationText = "'--env-file .env' reads key=value pairs from a file and passes them as environment variables.", HintText = "Pointing to a file", AnswerOptions =
                        [
                            new AnswerOption { Text = "file", IsCorrect = true, SortOrder = 1 }
                        ]},
                        new Question { Type = QuestionType.MultipleChoice, SortOrder = 4, Prompt = "Where should you store sensitive values like passwords in production?", ExplanationText = "Docker Secrets (in Swarm) or Kubernetes Secrets are better for sensitive data. Avoid hardcoding in images or Dockerfiles.", AnswerOptions =
                        [
                            new AnswerOption { Text = "Docker Secrets or a dedicated secrets manager", IsCorrect = true, SortOrder = 1 },
                            new AnswerOption { Text = "Directly in the Dockerfile as ENV instructions", IsCorrect = false, SortOrder = 2 },
                            new AnswerOption { Text = "In the image tag name", IsCorrect = false, SortOrder = 3 },
                            new AnswerOption { Text = "In plain .env files committed to Git", IsCorrect = false, SortOrder = 4 }
                        ]},
                        new Question { Type = QuestionType.TrueFalse, SortOrder = 5, Prompt = "'docker run -e DB_HOST' (without =value) inherits the variable from the host shell.", ExplanationText = "True — if you omit the value, Docker uses the current value of that variable from your shell environment.", AnswerOptions =
                        [
                            new AnswerOption { Text = "True", IsCorrect = true, SortOrder = 1 },
                            new AnswerOption { Text = "False", IsCorrect = false, SortOrder = 2 }
                        ]}
                    ]}
                ]},
                new Unit { Title = "Dockerfile", Emoji = "📄", SortOrder = 3, Description = "Write Dockerfiles to build your own images.", Lessons =
                [
                    new Lesson { Title = "Dockerfile Basics", Emoji = "📝", SortOrder = 1, XpReward = 10, XpPerfectBonus = 5, Description = "FROM, RUN, COPY, CMD — the core instructions.", Questions =
                    [
                        new Question { Type = QuestionType.MultipleChoice, SortOrder = 1, Prompt = "What does the FROM instruction do in a Dockerfile?", ExplanationText = "FROM sets the base image for subsequent instructions. Every Dockerfile must start with FROM (except for scratch images).", AnswerOptions =
                        [
                            new AnswerOption { Text = "Sets the base image for the build", IsCorrect = true, SortOrder = 1 },
                            new AnswerOption { Text = "Specifies the author of the image", IsCorrect = false, SortOrder = 2 },
                            new AnswerOption { Text = "Copies files from the host into the image", IsCorrect = false, SortOrder = 3 },
                            new AnswerOption { Text = "Defines the default command to run", IsCorrect = false, SortOrder = 4 }
                        ]},
                        new Question { Type = QuestionType.TrueFalse, SortOrder = 2, Prompt = "Each RUN instruction in a Dockerfile creates a new image layer.", ExplanationText = "True — every RUN, COPY, and ADD creates a new layer. This is why combining RUN commands with && saves space.", AnswerOptions =
                        [
                            new AnswerOption { Text = "True", IsCorrect = true, SortOrder = 1 },
                            new AnswerOption { Text = "False", IsCorrect = false, SortOrder = 2 }
                        ]},
                        new Question { Type = QuestionType.FillInTheBlank, SortOrder = 3, Prompt = "Complete this Dockerfile instruction to set the working directory: ___ /app", ExplanationText = "WORKDIR sets the working directory for subsequent RUN, CMD, ENTRYPOINT, COPY and ADD instructions.", HintText = "Sets the current directory", AnswerOptions =
                        [
                            new AnswerOption { Text = "WORKDIR", IsCorrect = true, SortOrder = 1 }
                        ]},
                        new Question { Type = QuestionType.MultipleChoice, SortOrder = 4, Prompt = "What is the difference between CMD and ENTRYPOINT?", ExplanationText = "ENTRYPOINT defines the executable; CMD provides default arguments. CMD is easily overridden at runtime; ENTRYPOINT is not.", AnswerOptions =
                        [
                            new AnswerOption { Text = "ENTRYPOINT defines the executable; CMD provides default arguments", IsCorrect = true, SortOrder = 1 },
                            new AnswerOption { Text = "CMD runs at build time; ENTRYPOINT runs at runtime", IsCorrect = false, SortOrder = 2 },
                            new AnswerOption { Text = "They are identical — both define the startup command", IsCorrect = false, SortOrder = 3 },
                            new AnswerOption { Text = "ENTRYPOINT copies files; CMD runs commands", IsCorrect = false, SortOrder = 4 }
                        ]},
                        new Question { Type = QuestionType.MultipleChoice, SortOrder = 5, Prompt = "What does EXPOSE 8080 do in a Dockerfile?", ExplanationText = "EXPOSE documents that the container listens on port 8080, but does NOT publish the port to the host. You still need -p to publish.", AnswerOptions =
                        [
                            new AnswerOption { Text = "Documents that the container listens on port 8080 (doesn't publish to host)", IsCorrect = true, SortOrder = 1 },
                            new AnswerOption { Text = "Publishes port 8080 to the same port on the host", IsCorrect = false, SortOrder = 2 },
                            new AnswerOption { Text = "Opens port 8080 through the firewall", IsCorrect = false, SortOrder = 3 },
                            new AnswerOption { Text = "Sets the default port for all docker run commands", IsCorrect = false, SortOrder = 4 }
                        ]}
                    ]},
                    new Lesson { Title = "Multi-Stage Builds", Emoji = "🏭", SortOrder = 2, XpReward = 10, XpPerfectBonus = 5, Description = "Build smaller production images with multi-stage builds.", Questions =
                    [
                        new Question { Type = QuestionType.MultipleChoice, SortOrder = 1, Prompt = "What is the main benefit of multi-stage builds?", ExplanationText = "Multi-stage builds let you compile code in one stage and copy only the binary to a smaller final stage, drastically reducing image size.", AnswerOptions =
                        [
                            new AnswerOption { Text = "Smaller final image by excluding build tools", IsCorrect = true, SortOrder = 1 },
                            new AnswerOption { Text = "Faster build times", IsCorrect = false, SortOrder = 2 },
                            new AnswerOption { Text = "Parallel layer execution", IsCorrect = false, SortOrder = 3 },
                            new AnswerOption { Text = "Automatic caching of all layers", IsCorrect = false, SortOrder = 4 }
                        ]},
                        new Question { Type = QuestionType.TrueFalse, SortOrder = 2, Prompt = "In a multi-stage Dockerfile, each FROM instruction starts a new build stage.", ExplanationText = "True — each FROM begins a new stage. You name stages with 'FROM image AS stagename' and copy from them with 'COPY --from=stagename'.", AnswerOptions =
                        [
                            new AnswerOption { Text = "True", IsCorrect = true, SortOrder = 1 },
                            new AnswerOption { Text = "False", IsCorrect = false, SortOrder = 2 }
                        ]},
                        new Question { Type = QuestionType.MultipleChoice, SortOrder = 3, Prompt = "Which instruction copies files from a previous build stage?", ExplanationText = "'COPY --from=builder /app/bin ./bin' copies only the needed artifacts from the 'builder' stage into the final stage.", AnswerOptions =
                        [
                            new AnswerOption { Text = "COPY --from=<stage>", IsCorrect = true, SortOrder = 1 },
                            new AnswerOption { Text = "IMPORT --stage=<stage>", IsCorrect = false, SortOrder = 2 },
                            new AnswerOption { Text = "ADD --from=<stage>", IsCorrect = false, SortOrder = 3 },
                            new AnswerOption { Text = "GET --stage=<stage>", IsCorrect = false, SortOrder = 4 }
                        ]},
                        new Question { Type = QuestionType.FillInTheBlank, SortOrder = 4, Prompt = "Complete: FROM mcr.microsoft.com/dotnet/sdk:8.0 ___ builder (to name a build stage)", ExplanationText = "'AS builder' gives the stage a name so you can reference it in later COPY --from= instructions.", HintText = "Naming the stage with a keyword", AnswerOptions =
                        [
                            new AnswerOption { Text = "AS", IsCorrect = true, SortOrder = 1 }
                        ]},
                        new Question { Type = QuestionType.TrueFalse, SortOrder = 5, Prompt = "Only the last stage of a multi-stage build is included in the final image.", ExplanationText = "True — intermediate stages are used during build but discarded. Only the final stage becomes the image.", AnswerOptions =
                        [
                            new AnswerOption { Text = "True", IsCorrect = true, SortOrder = 1 },
                            new AnswerOption { Text = "False", IsCorrect = false, SortOrder = 2 }
                        ]}
                    ]},
                    new Lesson { Title = ".dockerignore", Emoji = "🚫", SortOrder = 3, XpReward = 10, XpPerfectBonus = 5, Description = "Exclude files from the build context.", Questions =
                    [
                        new Question { Type = QuestionType.MultipleChoice, SortOrder = 1, Prompt = "What is the purpose of .dockerignore?", ExplanationText = ".dockerignore excludes files from the build context sent to the Docker daemon, speeding up builds and reducing image bloat.", AnswerOptions =
                        [
                            new AnswerOption { Text = "Exclude files from the build context to speed up builds", IsCorrect = true, SortOrder = 1 },
                            new AnswerOption { Text = "Block certain users from building images", IsCorrect = false, SortOrder = 2 },
                            new AnswerOption { Text = "Prevent docker pull from downloading certain images", IsCorrect = false, SortOrder = 3 },
                            new AnswerOption { Text = "List ports that should not be exposed", IsCorrect = false, SortOrder = 4 }
                        ]},
                        new Question { Type = QuestionType.TrueFalse, SortOrder = 2, Prompt = ".dockerignore uses the same syntax as .gitignore.", ExplanationText = "True — both use glob patterns like *.log, node_modules/, and **/*.tmp to match files and directories.", AnswerOptions =
                        [
                            new AnswerOption { Text = "True", IsCorrect = true, SortOrder = 1 },
                            new AnswerOption { Text = "False", IsCorrect = false, SortOrder = 2 }
                        ]},
                        new Question { Type = QuestionType.MultipleChoice, SortOrder = 3, Prompt = "Which entry in .dockerignore excludes the node_modules folder?", ExplanationText = "Adding 'node_modules' to .dockerignore prevents those heavy dependencies from being sent to the daemon — they'll be installed inside the image via RUN npm install.", AnswerOptions =
                        [
                            new AnswerOption { Text = "node_modules", IsCorrect = true, SortOrder = 1 },
                            new AnswerOption { Text = "!node_modules", IsCorrect = false, SortOrder = 2 },
                            new AnswerOption { Text = "/node_modules/**", IsCorrect = false, SortOrder = 3 },
                            new AnswerOption { Text = "exclude node_modules", IsCorrect = false, SortOrder = 4 }
                        ]},
                        new Question { Type = QuestionType.TrueFalse, SortOrder = 4, Prompt = "Not having a .dockerignore file can significantly slow down docker build.", ExplanationText = "True — without it, the entire directory (including node_modules, .git, etc.) is sent to the daemon as the build context.", AnswerOptions =
                        [
                            new AnswerOption { Text = "True", IsCorrect = true, SortOrder = 1 },
                            new AnswerOption { Text = "False", IsCorrect = false, SortOrder = 2 }
                        ]},
                        new Question { Type = QuestionType.MultipleChoice, SortOrder = 5, Prompt = "What does '**/*.log' in .dockerignore match?", ExplanationText = "'**/*.log' matches any .log file in any subdirectory at any depth — a recursive glob pattern.", AnswerOptions =
                        [
                            new AnswerOption { Text = "All .log files in all subdirectories", IsCorrect = true, SortOrder = 1 },
                            new AnswerOption { Text = "Only .log files in the root directory", IsCorrect = false, SortOrder = 2 },
                            new AnswerOption { Text = "A folder named **.log", IsCorrect = false, SortOrder = 3 },
                            new AnswerOption { Text = "Log files starting with **", IsCorrect = false, SortOrder = 4 }
                        ]}
                    ]},
                    new Lesson { Title = "Best Practices", Emoji = "🌟", SortOrder = 4, XpReward = 10, XpPerfectBonus = 5, Description = "Writing efficient, secure Dockerfiles.", Questions =
                    [
                        new Question { Type = QuestionType.MultipleChoice, SortOrder = 1, Prompt = "Why should you avoid running containers as root?", ExplanationText = "Running as root in a container is a security risk — if the container is compromised, the attacker may gain elevated host access.", AnswerOptions =
                        [
                            new AnswerOption { Text = "Security — a compromised container has less host access as non-root", IsCorrect = true, SortOrder = 1 },
                            new AnswerOption { Text = "Performance — non-root processes run faster", IsCorrect = false, SortOrder = 2 },
                            new AnswerOption { Text = "Compatibility — some images only support non-root", IsCorrect = false, SortOrder = 3 },
                            new AnswerOption { Text = "Networking — root containers can't use port mapping", IsCorrect = false, SortOrder = 4 }
                        ]},
                        new Question { Type = QuestionType.TrueFalse, SortOrder = 2, Prompt = "Combining multiple RUN commands with && reduces the number of image layers.", ExplanationText = "True — 'RUN apt-get update && apt-get install -y curl' creates one layer instead of two, reducing image size.", AnswerOptions =
                        [
                            new AnswerOption { Text = "True", IsCorrect = true, SortOrder = 1 },
                            new AnswerOption { Text = "False", IsCorrect = false, SortOrder = 2 }
                        ]},
                        new Question { Type = QuestionType.MultipleChoice, SortOrder = 3, Prompt = "What instruction creates a non-root user in a Dockerfile?", ExplanationText = "RUN useradd -r appuser creates a system user. Then USER appuser switches to that user for subsequent instructions.", AnswerOptions =
                        [
                            new AnswerOption { Text = "USER", IsCorrect = true, SortOrder = 1 },
                            new AnswerOption { Text = "OWNER", IsCorrect = false, SortOrder = 2 },
                            new AnswerOption { Text = "SUDO", IsCorrect = false, SortOrder = 3 },
                            new AnswerOption { Text = "RUNUSER", IsCorrect = false, SortOrder = 4 }
                        ]},
                        new Question { Type = QuestionType.MultipleChoice, SortOrder = 4, Prompt = "When should you COPY package.json before the rest of your source code?", ExplanationText = "Copying package.json first and running npm install creates a cached layer. Source code changes won't invalidate the dependency install layer.", AnswerOptions =
                        [
                            new AnswerOption { Text = "To leverage layer caching — dependencies change less often than source code", IsCorrect = true, SortOrder = 1 },
                            new AnswerOption { Text = "To ensure npm install runs before the app starts", IsCorrect = false, SortOrder = 2 },
                            new AnswerOption { Text = "It doesn't matter — Docker ignores COPY order", IsCorrect = false, SortOrder = 3 },
                            new AnswerOption { Text = "To reduce total build time by running installs in parallel", IsCorrect = false, SortOrder = 4 }
                        ]},
                        new Question { Type = QuestionType.FillInTheBlank, SortOrder = 5, Prompt = "Complete: use a ___ base image to reduce attack surface (e.g., alpine or distroless)", ExplanationText = "Minimal base images like Alpine Linux or distroless have fewer packages, reducing vulnerabilities and image size.", HintText = "Smaller is better", AnswerOptions =
                        [
                            new AnswerOption { Text = "minimal", IsCorrect = true, SortOrder = 1 }
                        ]}
                    ]}
                ]},
                new Unit { Title = "Docker Compose", Emoji = "🎼", SortOrder = 4, Description = "Define and run multi-container apps with Compose.", Lessons =
                [
                    new Lesson { Title = "Compose Basics", Emoji = "📋", SortOrder = 1, XpReward = 10, XpPerfectBonus = 5, Description = "docker-compose.yml structure and key commands.", Questions =
                    [
                        new Question { Type = QuestionType.MultipleChoice, SortOrder = 1, Prompt = "What is Docker Compose used for?", ExplanationText = "Docker Compose lets you define and run multi-container applications using a single YAML file.", AnswerOptions =
                        [
                            new AnswerOption { Text = "Define and run multi-container applications with a YAML file", IsCorrect = true, SortOrder = 1 },
                            new AnswerOption { Text = "Build images faster using parallel execution", IsCorrect = false, SortOrder = 2 },
                            new AnswerOption { Text = "Manage Docker clusters in production", IsCorrect = false, SortOrder = 3 },
                            new AnswerOption { Text = "Monitor Docker container resource usage", IsCorrect = false, SortOrder = 4 }
                        ]},
                        new Question { Type = QuestionType.FillInTheBlank, SortOrder = 2, Prompt = "Complete: docker compose ___ (to start all services in detached mode, add -d)", ExplanationText = "'docker compose up -d' creates and starts all containers defined in docker-compose.yml in detached mode.", HintText = "The opposite of 'down'", AnswerOptions =
                        [
                            new AnswerOption { Text = "up", IsCorrect = true, SortOrder = 1 }
                        ]},
                        new Question { Type = QuestionType.TrueFalse, SortOrder = 3, Prompt = "Docker Compose automatically creates a shared network for all services.", ExplanationText = "True — Compose creates a default network and connects all services to it, so they can reach each other by service name.", AnswerOptions =
                        [
                            new AnswerOption { Text = "True", IsCorrect = true, SortOrder = 1 },
                            new AnswerOption { Text = "False", IsCorrect = false, SortOrder = 2 }
                        ]},
                        new Question { Type = QuestionType.MultipleChoice, SortOrder = 4, Prompt = "How do services communicate in a Compose file?", ExplanationText = "In Compose, services can use each other's service name as the hostname (e.g., 'db' instead of an IP address).", AnswerOptions =
                        [
                            new AnswerOption { Text = "By service name as the hostname", IsCorrect = true, SortOrder = 1 },
                            new AnswerOption { Text = "By IP address only", IsCorrect = false, SortOrder = 2 },
                            new AnswerOption { Text = "Via a shared volume", IsCorrect = false, SortOrder = 3 },
                            new AnswerOption { Text = "Through environment variables only", IsCorrect = false, SortOrder = 4 }
                        ]},
                        new Question { Type = QuestionType.MultipleChoice, SortOrder = 5, Prompt = "What does 'docker compose down' do?", ExplanationText = "'docker compose down' stops and removes containers, networks, and default volumes. Add --volumes to also remove named volumes.", AnswerOptions =
                        [
                            new AnswerOption { Text = "Stops and removes containers and networks", IsCorrect = true, SortOrder = 1 },
                            new AnswerOption { Text = "Pauses all running services", IsCorrect = false, SortOrder = 2 },
                            new AnswerOption { Text = "Rebuilds all service images", IsCorrect = false, SortOrder = 3 },
                            new AnswerOption { Text = "Scales all services down to zero replicas", IsCorrect = false, SortOrder = 4 }
                        ]}
                    ]},
                    new Lesson { Title = "Services & Dependencies", Emoji = "🔗", SortOrder = 2, XpReward = 10, XpPerfectBonus = 5, Description = "depends_on, healthchecks, and service ordering.", Questions =
                    [
                        new Question { Type = QuestionType.MultipleChoice, SortOrder = 1, Prompt = "What does 'depends_on' control in Docker Compose?", ExplanationText = "'depends_on' controls container start order — it ensures the listed services start before the dependent one. It does NOT wait for readiness.", AnswerOptions =
                        [
                            new AnswerOption { Text = "Container start order (but not readiness)", IsCorrect = true, SortOrder = 1 },
                            new AnswerOption { Text = "Network connection between services", IsCorrect = false, SortOrder = 2 },
                            new AnswerOption { Text = "Volume sharing between services", IsCorrect = false, SortOrder = 3 },
                            new AnswerOption { Text = "Which services are built first", IsCorrect = false, SortOrder = 4 }
                        ]},
                        new Question { Type = QuestionType.TrueFalse, SortOrder = 2, Prompt = "'depends_on' guarantees the dependency service is fully ready before the dependent starts.", ExplanationText = "False — depends_on only waits for the container to START, not for the app inside to be ready. Use healthchecks for true readiness.", AnswerOptions =
                        [
                            new AnswerOption { Text = "True", IsCorrect = false, SortOrder = 1 },
                            new AnswerOption { Text = "False", IsCorrect = true, SortOrder = 2 }
                        ]},
                        new Question { Type = QuestionType.FillInTheBlank, SortOrder = 3, Prompt = "In a Compose healthcheck, what property sets how often Docker checks the container's health? ___ (the interval key)", ExplanationText = "The 'interval' key in healthcheck sets how frequently Docker runs the health test command.", HintText = "Time between checks", AnswerOptions =
                        [
                            new AnswerOption { Text = "interval", IsCorrect = true, SortOrder = 1 }
                        ]},
                        new Question { Type = QuestionType.MultipleChoice, SortOrder = 4, Prompt = "Which Compose key defines environment variables for a service?", ExplanationText = "The 'environment' key sets env vars inline. You can also use 'env_file' to load from a file.", AnswerOptions =
                        [
                            new AnswerOption { Text = "environment", IsCorrect = true, SortOrder = 1 },
                            new AnswerOption { Text = "env_vars", IsCorrect = false, SortOrder = 2 },
                            new AnswerOption { Text = "config", IsCorrect = false, SortOrder = 3 },
                            new AnswerOption { Text = "variables", IsCorrect = false, SortOrder = 4 }
                        ]},
                        new Question { Type = QuestionType.MultipleChoice, SortOrder = 5, Prompt = "How do you rebuild images when running docker compose up?", ExplanationText = "'docker compose up --build' forces Compose to rebuild the images before starting containers.", AnswerOptions =
                        [
                            new AnswerOption { Text = "docker compose up --build", IsCorrect = true, SortOrder = 1 },
                            new AnswerOption { Text = "docker compose rebuild && docker compose up", IsCorrect = false, SortOrder = 2 },
                            new AnswerOption { Text = "docker compose up --refresh", IsCorrect = false, SortOrder = 3 },
                            new AnswerOption { Text = "docker compose recreate", IsCorrect = false, SortOrder = 4 }
                        ]}
                    ]},
                    new Lesson { Title = "Volumes in Compose", Emoji = "💾", SortOrder = 3, XpReward = 10, XpPerfectBonus = 5, Description = "Persisting data with named volumes in Compose.", Questions =
                    [
                        new Question { Type = QuestionType.TrueFalse, SortOrder = 1, Prompt = "Named volumes defined in Compose persist even after 'docker compose down'.", ExplanationText = "True — named volumes survive 'down'. Use 'docker compose down --volumes' to also remove them.", AnswerOptions =
                        [
                            new AnswerOption { Text = "True", IsCorrect = true, SortOrder = 1 },
                            new AnswerOption { Text = "False", IsCorrect = false, SortOrder = 2 }
                        ]},
                        new Question { Type = QuestionType.MultipleChoice, SortOrder = 2, Prompt = "In a Compose file, where do you declare a named volume?", ExplanationText = "Named volumes must be declared under the top-level 'volumes:' key. Services then reference them under their 'volumes:' section.", AnswerOptions =
                        [
                            new AnswerOption { Text = "Under the top-level 'volumes:' key", IsCorrect = true, SortOrder = 1 },
                            new AnswerOption { Text = "Only under each service's configuration", IsCorrect = false, SortOrder = 2 },
                            new AnswerOption { Text = "Under the 'networks:' section", IsCorrect = false, SortOrder = 3 },
                            new AnswerOption { Text = "In a separate volumes.yml file", IsCorrect = false, SortOrder = 4 }
                        ]},
                        new Question { Type = QuestionType.FillInTheBlank, SortOrder = 3, Prompt = "Complete the volume mount syntax in a service: - ___:/var/lib/postgresql/data (using named volume 'pgdata')", ExplanationText = "'pgdata:/var/lib/postgresql/data' mounts the 'pgdata' named volume to the PostgreSQL data directory.", HintText = "The name of the named volume", AnswerOptions =
                        [
                            new AnswerOption { Text = "pgdata", IsCorrect = true, SortOrder = 1 }
                        ]},
                        new Question { Type = QuestionType.MultipleChoice, SortOrder = 4, Prompt = "What is a bind mount in Docker Compose?", ExplanationText = "A bind mount maps a host directory directly into a container (e.g., ./src:/app/src). Changes on the host are immediately reflected in the container.", AnswerOptions =
                        [
                            new AnswerOption { Text = "A host directory mapped directly into a container path", IsCorrect = true, SortOrder = 1 },
                            new AnswerOption { Text = "A Docker-managed named volume", IsCorrect = false, SortOrder = 2 },
                            new AnswerOption { Text = "A volume shared between two containers", IsCorrect = false, SortOrder = 3 },
                            new AnswerOption { Text = "A read-only filesystem layer", IsCorrect = false, SortOrder = 4 }
                        ]},
                        new Question { Type = QuestionType.TrueFalse, SortOrder = 5, Prompt = "Multiple services in Compose can share the same named volume.", ExplanationText = "True — multiple services can mount the same named volume, which is useful for sharing data between containers.", AnswerOptions =
                        [
                            new AnswerOption { Text = "True", IsCorrect = true, SortOrder = 1 },
                            new AnswerOption { Text = "False", IsCorrect = false, SortOrder = 2 }
                        ]}
                    ]},
                    new Lesson { Title = "Scaling & Profiles", Emoji = "📊", SortOrder = 4, XpReward = 10, XpPerfectBonus = 5, Description = "Scaling services and using Compose profiles.", Questions =
                    [
                        new Question { Type = QuestionType.MultipleChoice, SortOrder = 1, Prompt = "How do you scale a service to 3 replicas with Docker Compose?", ExplanationText = "'docker compose up --scale web=3' starts 3 instances of the 'web' service. Note: port conflicts arise if you publish host ports.", AnswerOptions =
                        [
                            new AnswerOption { Text = "docker compose up --scale web=3", IsCorrect = true, SortOrder = 1 },
                            new AnswerOption { Text = "docker compose scale web 3", IsCorrect = false, SortOrder = 2 },
                            new AnswerOption { Text = "docker compose replicate web=3", IsCorrect = false, SortOrder = 3 },
                            new AnswerOption { Text = "docker compose up --replicas=3 web", IsCorrect = false, SortOrder = 4 }
                        ]},
                        new Question { Type = QuestionType.TrueFalse, SortOrder = 2, Prompt = "Compose profiles allow you to selectively start subsets of services.", ExplanationText = "True — profiles let you tag services and activate groups with 'docker compose --profile <name> up'.", AnswerOptions =
                        [
                            new AnswerOption { Text = "True", IsCorrect = true, SortOrder = 1 },
                            new AnswerOption { Text = "False", IsCorrect = false, SortOrder = 2 }
                        ]},
                        new Question { Type = QuestionType.FillInTheBlank, SortOrder = 3, Prompt = "Complete: docker compose --___ debug up (to start only services tagged with 'debug' profile)", ExplanationText = "'--profile debug' activates only services that have 'debug' in their profiles list.", HintText = "The key word for selecting a group", AnswerOptions =
                        [
                            new AnswerOption { Text = "profile", IsCorrect = true, SortOrder = 1 }
                        ]},
                        new Question { Type = QuestionType.MultipleChoice, SortOrder = 4, Prompt = "What is the 'deploy.replicas' key used for in Compose?", ExplanationText = "'deploy.replicas' sets the number of container replicas when used with Docker Swarm. It has no effect with plain 'docker compose up'.", AnswerOptions =
                        [
                            new AnswerOption { Text = "Sets replica count when deployed to Docker Swarm", IsCorrect = true, SortOrder = 1 },
                            new AnswerOption { Text = "Sets replicas for local development too", IsCorrect = false, SortOrder = 2 },
                            new AnswerOption { Text = "Limits CPU usage for a service", IsCorrect = false, SortOrder = 3 },
                            new AnswerOption { Text = "Configures database replication", IsCorrect = false, SortOrder = 4 }
                        ]},
                        new Question { Type = QuestionType.TrueFalse, SortOrder = 5, Prompt = "You can override values in docker-compose.yml using a docker-compose.override.yml file.", ExplanationText = "True — Compose automatically merges docker-compose.override.yml. This is a common pattern for local dev vs CI differences.", AnswerOptions =
                        [
                            new AnswerOption { Text = "True", IsCorrect = true, SortOrder = 1 },
                            new AnswerOption { Text = "False", IsCorrect = false, SortOrder = 2 }
                        ]}
                    ]}
                ]},
                new Unit { Title = "Networking", Emoji = "🌐", SortOrder = 5, Description = "Docker networking modes and container communication.", Lessons =
                [
                    new Lesson { Title = "Network Modes", Emoji = "🔌", SortOrder = 1, XpReward = 10, XpPerfectBonus = 5, Description = "bridge, host, none, and overlay networks.", Questions =
                    [
                        new Question { Type = QuestionType.MultipleChoice, SortOrder = 1, Prompt = "Which is the default network mode for Docker containers?", ExplanationText = "Bridge is the default — containers get their own IP on a virtual bridge network and can communicate via NAT.", AnswerOptions =
                        [
                            new AnswerOption { Text = "bridge", IsCorrect = true, SortOrder = 1 },
                            new AnswerOption { Text = "host", IsCorrect = false, SortOrder = 2 },
                            new AnswerOption { Text = "overlay", IsCorrect = false, SortOrder = 3 },
                            new AnswerOption { Text = "macvlan", IsCorrect = false, SortOrder = 4 }
                        ]},
                        new Question { Type = QuestionType.TrueFalse, SortOrder = 2, Prompt = "In 'host' network mode, the container shares the host's network stack.", ExplanationText = "True — with --network host, there's no network isolation. The container uses the host's IP and ports directly.", AnswerOptions =
                        [
                            new AnswerOption { Text = "True", IsCorrect = true, SortOrder = 1 },
                            new AnswerOption { Text = "False", IsCorrect = false, SortOrder = 2 }
                        ]},
                        new Question { Type = QuestionType.MultipleChoice, SortOrder = 3, Prompt = "Which network type is used for multi-host Docker Swarm communication?", ExplanationText = "Overlay networks span multiple Docker hosts, enabling containers on different machines to communicate securely in a Swarm.", AnswerOptions =
                        [
                            new AnswerOption { Text = "overlay", IsCorrect = true, SortOrder = 1 },
                            new AnswerOption { Text = "bridge", IsCorrect = false, SortOrder = 2 },
                            new AnswerOption { Text = "macvlan", IsCorrect = false, SortOrder = 3 },
                            new AnswerOption { Text = "host", IsCorrect = false, SortOrder = 4 }
                        ]},
                        new Question { Type = QuestionType.FillInTheBlank, SortOrder = 4, Prompt = "Complete: docker network ___ mynet (to create a custom bridge network named 'mynet')", ExplanationText = "'docker network create mynet' creates a custom bridge network. Containers on it can communicate by name.", HintText = "Building a new network", AnswerOptions =
                        [
                            new AnswerOption { Text = "create", IsCorrect = true, SortOrder = 1 }
                        ]},
                        new Question { Type = QuestionType.TrueFalse, SortOrder = 5, Prompt = "Containers on the default bridge network can resolve each other by container name.", ExplanationText = "False — name resolution only works on user-defined bridge networks. The default bridge uses IPs, not names.", AnswerOptions =
                        [
                            new AnswerOption { Text = "True", IsCorrect = false, SortOrder = 1 },
                            new AnswerOption { Text = "False", IsCorrect = true, SortOrder = 2 }
                        ]}
                    ]},
                    new Lesson { Title = "Port Publishing", Emoji = "🚪", SortOrder = 2, XpReward = 10, XpPerfectBonus = 5, Description = "Binding container ports to the host.", Questions =
                    [
                        new Question { Type = QuestionType.MultipleChoice, SortOrder = 1, Prompt = "What does 'docker run -p 127.0.0.1:8080:80 nginx' do differently from '-p 8080:80'?", ExplanationText = "Binding to 127.0.0.1 restricts port exposure to localhost only. Without it, the port is bound to all host interfaces (0.0.0.0).", AnswerOptions =
                        [
                            new AnswerOption { Text = "Restricts access to localhost only (not external network)", IsCorrect = true, SortOrder = 1 },
                            new AnswerOption { Text = "They behave identically", IsCorrect = false, SortOrder = 2 },
                            new AnswerOption { Text = "Runs on port 127 instead of 8080", IsCorrect = false, SortOrder = 3 },
                            new AnswerOption { Text = "Disables port publishing altogether", IsCorrect = false, SortOrder = 4 }
                        ]},
                        new Question { Type = QuestionType.TrueFalse, SortOrder = 2, Prompt = "EXPOSE in a Dockerfile automatically publishes the port to the host.", ExplanationText = "False — EXPOSE is documentation only. You must use -p (or -P) at runtime to actually publish to the host.", AnswerOptions =
                        [
                            new AnswerOption { Text = "True", IsCorrect = false, SortOrder = 1 },
                            new AnswerOption { Text = "False", IsCorrect = true, SortOrder = 2 }
                        ]},
                        new Question { Type = QuestionType.FillInTheBlank, SortOrder = 3, Prompt = "Complete: docker run ___ nginx (to publish all EXPOSE'd ports to random host ports)", ExplanationText = "'-P' (uppercase) publishes all exposed ports to random ephemeral host ports. '-p' (lowercase) requires explicit mapping.", HintText = "Capital P for publish-all", AnswerOptions =
                        [
                            new AnswerOption { Text = "-P", IsCorrect = true, SortOrder = 1 }
                        ]},
                        new Question { Type = QuestionType.MultipleChoice, SortOrder = 4, Prompt = "How do you publish a UDP port?", ExplanationText = "Append '/udp' to the port mapping: '-p 5353:5353/udp'. The default is TCP if no protocol is specified.", AnswerOptions =
                        [
                            new AnswerOption { Text = "-p 5353:5353/udp", IsCorrect = true, SortOrder = 1 },
                            new AnswerOption { Text = "-p 5353:5353 --udp", IsCorrect = false, SortOrder = 2 },
                            new AnswerOption { Text = "-p udp:5353:5353", IsCorrect = false, SortOrder = 3 },
                            new AnswerOption { Text = "-p 5353:5353 -proto udp", IsCorrect = false, SortOrder = 4 }
                        ]},
                        new Question { Type = QuestionType.TrueFalse, SortOrder = 5, Prompt = "Containers on the same custom bridge network can communicate without port publishing.", ExplanationText = "True — containers on the same user-defined network can reach each other on any port internally. Port publishing is only needed for host access.", AnswerOptions =
                        [
                            new AnswerOption { Text = "True", IsCorrect = true, SortOrder = 1 },
                            new AnswerOption { Text = "False", IsCorrect = false, SortOrder = 2 }
                        ]}
                    ]},
                    new Lesson { Title = "DNS & Service Discovery", Emoji = "🔎", SortOrder = 3, XpReward = 10, XpPerfectBonus = 5, Description = "How containers find each other by name.", Questions =
                    [
                        new Question { Type = QuestionType.MultipleChoice, SortOrder = 1, Prompt = "On a user-defined Docker network, how does container 'app' reach container 'db'?", ExplanationText = "Docker's embedded DNS resolves container names on user-defined networks. 'app' can use 'db' as the hostname.", AnswerOptions =
                        [
                            new AnswerOption { Text = "By using 'db' as the hostname — Docker DNS resolves it", IsCorrect = true, SortOrder = 1 },
                            new AnswerOption { Text = "Only via explicit IP address", IsCorrect = false, SortOrder = 2 },
                            new AnswerOption { Text = "Through a shared /etc/hosts entry only", IsCorrect = false, SortOrder = 3 },
                            new AnswerOption { Text = "Via a load balancer only", IsCorrect = false, SortOrder = 4 }
                        ]},
                        new Question { Type = QuestionType.TrueFalse, SortOrder = 2, Prompt = "Docker provides a built-in DNS server for container name resolution on custom networks.", ExplanationText = "True — Docker runs an embedded DNS server at 127.0.0.11 inside containers on custom networks.", AnswerOptions =
                        [
                            new AnswerOption { Text = "True", IsCorrect = true, SortOrder = 1 },
                            new AnswerOption { Text = "False", IsCorrect = false, SortOrder = 2 }
                        ]},
                        new Question { Type = QuestionType.FillInTheBlank, SortOrder = 3, Prompt = "Complete: docker run --network mynet --___ db nginx (to give the container the alias 'db' on the network)", ExplanationText = "'--network-alias db' gives the container an additional DNS name on that specific network.", HintText = "Extra DNS name", AnswerOptions =
                        [
                            new AnswerOption { Text = "network-alias", IsCorrect = true, SortOrder = 1 }
                        ]},
                        new Question { Type = QuestionType.MultipleChoice, SortOrder = 4, Prompt = "Which file inside a container holds DNS resolver configuration?", ExplanationText = "/etc/resolv.conf contains the DNS server addresses. Docker sets this to point to its embedded DNS.", AnswerOptions =
                        [
                            new AnswerOption { Text = "/etc/resolv.conf", IsCorrect = true, SortOrder = 1 },
                            new AnswerOption { Text = "/etc/dns.conf", IsCorrect = false, SortOrder = 2 },
                            new AnswerOption { Text = "/var/dns/config", IsCorrect = false, SortOrder = 3 },
                            new AnswerOption { Text = "/etc/network/resolvers", IsCorrect = false, SortOrder = 4 }
                        ]},
                        new Question { Type = QuestionType.TrueFalse, SortOrder = 5, Prompt = "Docker Compose uses service names as DNS hostnames between services.", ExplanationText = "True — in a Compose file, service names act as hostnames on the default Compose network.", AnswerOptions =
                        [
                            new AnswerOption { Text = "True", IsCorrect = true, SortOrder = 1 },
                            new AnswerOption { Text = "False", IsCorrect = false, SortOrder = 2 }
                        ]}
                    ]},
                    new Lesson { Title = "Network Commands", Emoji = "🛠️", SortOrder = 4, XpReward = 10, XpPerfectBonus = 5, Description = "Managing Docker networks with CLI commands.", Questions =
                    [
                        new Question { Type = QuestionType.FillInTheBlank, SortOrder = 1, Prompt = "Complete: docker network ___ (to list all networks)", ExplanationText = "'docker network ls' shows all networks including bridge, host, and none plus any custom networks.", HintText = "Listing command", AnswerOptions =
                        [
                            new AnswerOption { Text = "ls", IsCorrect = true, SortOrder = 1 }
                        ]},
                        new Question { Type = QuestionType.MultipleChoice, SortOrder = 2, Prompt = "How do you connect a running container to an existing network?", ExplanationText = "'docker network connect <network> <container>' attaches a running container to a network without restarting it.", AnswerOptions =
                        [
                            new AnswerOption { Text = "docker network connect <network> <container>", IsCorrect = true, SortOrder = 1 },
                            new AnswerOption { Text = "docker run --add-network <network> <container>", IsCorrect = false, SortOrder = 2 },
                            new AnswerOption { Text = "docker network attach <network> <container>", IsCorrect = false, SortOrder = 3 },
                            new AnswerOption { Text = "docker connect <container> <network>", IsCorrect = false, SortOrder = 4 }
                        ]},
                        new Question { Type = QuestionType.TrueFalse, SortOrder = 3, Prompt = "A container can be connected to multiple Docker networks simultaneously.", ExplanationText = "True — a container can have network interfaces on multiple networks at the same time, enabling cross-network communication.", AnswerOptions =
                        [
                            new AnswerOption { Text = "True", IsCorrect = true, SortOrder = 1 },
                            new AnswerOption { Text = "False", IsCorrect = false, SortOrder = 2 }
                        ]},
                        new Question { Type = QuestionType.MultipleChoice, SortOrder = 4, Prompt = "Which command shows detailed info about a specific Docker network?", ExplanationText = "'docker network inspect <name>' shows the network's subnet, gateway, connected containers, and configuration.", AnswerOptions =
                        [
                            new AnswerOption { Text = "docker network inspect", IsCorrect = true, SortOrder = 1 },
                            new AnswerOption { Text = "docker network info", IsCorrect = false, SortOrder = 2 },
                            new AnswerOption { Text = "docker network describe", IsCorrect = false, SortOrder = 3 },
                            new AnswerOption { Text = "docker network show", IsCorrect = false, SortOrder = 4 }
                        ]},
                        new Question { Type = QuestionType.FillInTheBlank, SortOrder = 5, Prompt = "Complete: docker network ___ mynet (to delete a network)", ExplanationText = "'docker network rm mynet' removes a network. All containers must be disconnected first.", HintText = "Remove/delete", AnswerOptions =
                        [
                            new AnswerOption { Text = "rm", IsCorrect = true, SortOrder = 1 }
                        ]}
                    ]}
                ]},
                new Unit { Title = "Volumes", Emoji = "💾", SortOrder = 6, Description = "Persist data with Docker volumes and bind mounts.", Lessons =
                [
                    new Lesson { Title = "Volume Types", Emoji = "📀", SortOrder = 1, XpReward = 10, XpPerfectBonus = 5, Description = "Named volumes, bind mounts, and tmpfs.", Questions =
                    [
                        new Question { Type = QuestionType.MultipleChoice, SortOrder = 1, Prompt = "Which volume type is fully managed by Docker and stored in Docker's storage area?", ExplanationText = "Named volumes are managed by Docker in /var/lib/docker/volumes. They're the recommended way to persist data.", AnswerOptions =
                        [
                            new AnswerOption { Text = "Named volumes", IsCorrect = true, SortOrder = 1 },
                            new AnswerOption { Text = "Bind mounts", IsCorrect = false, SortOrder = 2 },
                            new AnswerOption { Text = "tmpfs mounts", IsCorrect = false, SortOrder = 3 },
                            new AnswerOption { Text = "Anonymous volumes", IsCorrect = false, SortOrder = 4 }
                        ]},
                        new Question { Type = QuestionType.TrueFalse, SortOrder = 2, Prompt = "tmpfs mounts store data in memory only and are lost when the container stops.", ExplanationText = "True — tmpfs mounts are in-memory only. They're useful for sensitive data that shouldn't be written to disk.", AnswerOptions =
                        [
                            new AnswerOption { Text = "True", IsCorrect = true, SortOrder = 1 },
                            new AnswerOption { Text = "False", IsCorrect = false, SortOrder = 2 }
                        ]},
                        new Question { Type = QuestionType.FillInTheBlank, SortOrder = 3, Prompt = "Complete: docker run -v ___:/app/data myimage (to mount a named volume called 'mydata')", ExplanationText = "'mydata:/app/data' mounts the named volume 'mydata' to /app/data inside the container.", HintText = "The volume name", AnswerOptions =
                        [
                            new AnswerOption { Text = "mydata", IsCorrect = true, SortOrder = 1 }
                        ]},
                        new Question { Type = QuestionType.MultipleChoice, SortOrder = 4, Prompt = "What is a key advantage of named volumes over bind mounts?", ExplanationText = "Named volumes work the same on Windows, Mac, and Linux. Bind mounts depend on the host path which varies by OS.", AnswerOptions =
                        [
                            new AnswerOption { Text = "Portability — they work the same across all operating systems", IsCorrect = true, SortOrder = 1 },
                            new AnswerOption { Text = "Faster I/O performance", IsCorrect = false, SortOrder = 2 },
                            new AnswerOption { Text = "Can be accessed by the host without mapping", IsCorrect = false, SortOrder = 3 },
                            new AnswerOption { Text = "Automatically backed up by Docker", IsCorrect = false, SortOrder = 4 }
                        ]},
                        new Question { Type = QuestionType.TrueFalse, SortOrder = 5, Prompt = "Bind mounts can be used to inject source code into a container for live development.", ExplanationText = "True — bind mounts map host directories into containers. Mounting your ./src folder means code changes reflect instantly without rebuilding.", AnswerOptions =
                        [
                            new AnswerOption { Text = "True", IsCorrect = true, SortOrder = 1 },
                            new AnswerOption { Text = "False", IsCorrect = false, SortOrder = 2 }
                        ]}
                    ]},
                    new Lesson { Title = "Volume Commands", Emoji = "🔧", SortOrder = 2, XpReward = 10, XpPerfectBonus = 5, Description = "Managing volumes with docker volume CLI.", Questions =
                    [
                        new Question { Type = QuestionType.FillInTheBlank, SortOrder = 1, Prompt = "Complete: docker volume ___ (to list all volumes)", ExplanationText = "'docker volume ls' lists all named volumes managed by Docker.", HintText = "The listing subcommand", AnswerOptions =
                        [
                            new AnswerOption { Text = "ls", IsCorrect = true, SortOrder = 1 }
                        ]},
                        new Question { Type = QuestionType.MultipleChoice, SortOrder = 2, Prompt = "How do you create a named volume before running a container?", ExplanationText = "'docker volume create pgdata' creates an empty named volume. Docker also auto-creates it on first use in 'docker run -v pgdata:/path'.", AnswerOptions =
                        [
                            new AnswerOption { Text = "docker volume create pgdata", IsCorrect = true, SortOrder = 1 },
                            new AnswerOption { Text = "docker volume new pgdata", IsCorrect = false, SortOrder = 2 },
                            new AnswerOption { Text = "docker volume init pgdata", IsCorrect = false, SortOrder = 3 },
                            new AnswerOption { Text = "docker volume add pgdata", IsCorrect = false, SortOrder = 4 }
                        ]},
                        new Question { Type = QuestionType.TrueFalse, SortOrder = 3, Prompt = "You can inspect a volume to find where Docker stores its data on the host.", ExplanationText = "True — 'docker volume inspect pgdata' shows the 'Mountpoint' which is the host path (usually /var/lib/docker/volumes/pgdata/_data).", AnswerOptions =
                        [
                            new AnswerOption { Text = "True", IsCorrect = true, SortOrder = 1 },
                            new AnswerOption { Text = "False", IsCorrect = false, SortOrder = 2 }
                        ]},
                        new Question { Type = QuestionType.MultipleChoice, SortOrder = 4, Prompt = "Which command removes all unused Docker volumes?", ExplanationText = "'docker volume prune' removes all volumes not mounted by at least one container. Use with caution!", AnswerOptions =
                        [
                            new AnswerOption { Text = "docker volume prune", IsCorrect = true, SortOrder = 1 },
                            new AnswerOption { Text = "docker volume rm --all", IsCorrect = false, SortOrder = 2 },
                            new AnswerOption { Text = "docker volume clean", IsCorrect = false, SortOrder = 3 },
                            new AnswerOption { Text = "docker volume delete unused", IsCorrect = false, SortOrder = 4 }
                        ]},
                        new Question { Type = QuestionType.FillInTheBlank, SortOrder = 5, Prompt = "Complete: docker volume ___ pgdata (to delete a specific named volume)", ExplanationText = "'docker volume rm pgdata' deletes the named volume and its data permanently.", HintText = "Remove/delete", AnswerOptions =
                        [
                            new AnswerOption { Text = "rm", IsCorrect = true, SortOrder = 1 }
                        ]}
                    ]},
                    new Lesson { Title = "Volume Drivers & Backups", Emoji = "☁️", SortOrder = 3, XpReward = 10, XpPerfectBonus = 5, Description = "Volume drivers and backing up container data.", Questions =
                    [
                        new Question { Type = QuestionType.MultipleChoice, SortOrder = 1, Prompt = "What is a volume driver in Docker?", ExplanationText = "Volume drivers let Docker use external storage systems (NFS, AWS EBS, Azure Disks) instead of local disk.", AnswerOptions =
                        [
                            new AnswerOption { Text = "A plugin that stores volumes on external storage systems", IsCorrect = true, SortOrder = 1 },
                            new AnswerOption { Text = "A tool for compressing volumes", IsCorrect = false, SortOrder = 2 },
                            new AnswerOption { Text = "A process that mounts volumes automatically", IsCorrect = false, SortOrder = 3 },
                            new AnswerOption { Text = "An OS service that manages disk I/O", IsCorrect = false, SortOrder = 4 }
                        ]},
                        new Question { Type = QuestionType.TrueFalse, SortOrder = 2, Prompt = "The default volume driver is 'local' which stores data on the Docker host.", ExplanationText = "True — 'local' driver is the default, keeping data on the host machine's filesystem.", AnswerOptions =
                        [
                            new AnswerOption { Text = "True", IsCorrect = true, SortOrder = 1 },
                            new AnswerOption { Text = "False", IsCorrect = false, SortOrder = 2 }
                        ]},
                        new Question { Type = QuestionType.MultipleChoice, SortOrder = 3, Prompt = "How would you back up a named Docker volume?", ExplanationText = "Run a temporary container that mounts the volume and tar's its contents to a bind-mounted host directory.", AnswerOptions =
                        [
                            new AnswerOption { Text = "Mount the volume in a temp container and tar the contents to the host", IsCorrect = true, SortOrder = 1 },
                            new AnswerOption { Text = "Run 'docker volume backup'", IsCorrect = false, SortOrder = 2 },
                            new AnswerOption { Text = "Copy /var/lib/docker directly", IsCorrect = false, SortOrder = 3 },
                            new AnswerOption { Text = "Use 'docker save' on the volume", IsCorrect = false, SortOrder = 4 }
                        ]},
                        new Question { Type = QuestionType.FillInTheBlank, SortOrder = 4, Prompt = "Complete: docker run --mount type=___,src=mydata,dst=/data alpine (volume mount type)", ExplanationText = "type=volume specifies a named volume. Other types are 'bind' and 'tmpfs'.", HintText = "The named volume type", AnswerOptions =
                        [
                            new AnswerOption { Text = "volume", IsCorrect = true, SortOrder = 1 }
                        ]},
                        new Question { Type = QuestionType.TrueFalse, SortOrder = 5, Prompt = "Volumes are a better option than bind mounts for databases in production.", ExplanationText = "True — named volumes are Docker-managed, portable, and have better performance characteristics than bind mounts for databases.", AnswerOptions =
                        [
                            new AnswerOption { Text = "True", IsCorrect = true, SortOrder = 1 },
                            new AnswerOption { Text = "False", IsCorrect = false, SortOrder = 2 }
                        ]}
                    ]},
                    new Lesson { Title = "Read-Only & Sharing", Emoji = "🔒", SortOrder = 4, XpReward = 10, XpPerfectBonus = 5, Description = "Read-only mounts and sharing volumes between containers.", Questions =
                    [
                        new Question { Type = QuestionType.MultipleChoice, SortOrder = 1, Prompt = "How do you mount a volume as read-only?", ExplanationText = "Append ':ro' to the volume mount: '-v mydata:/app/data:ro'. The container can read but not write to the volume.", AnswerOptions =
                        [
                            new AnswerOption { Text = "-v mydata:/app/data:ro", IsCorrect = true, SortOrder = 1 },
                            new AnswerOption { Text = "-v mydata:/app/data --readonly", IsCorrect = false, SortOrder = 2 },
                            new AnswerOption { Text = "-v mydata:/app/data:lock", IsCorrect = false, SortOrder = 3 },
                            new AnswerOption { Text = "--volume-readonly mydata:/app/data", IsCorrect = false, SortOrder = 4 }
                        ]},
                        new Question { Type = QuestionType.TrueFalse, SortOrder = 2, Prompt = "Multiple containers can mount the same Docker volume simultaneously.", ExplanationText = "True — volumes can be shared, but concurrent writes may cause data corruption without application-level locking.", AnswerOptions =
                        [
                            new AnswerOption { Text = "True", IsCorrect = true, SortOrder = 1 },
                            new AnswerOption { Text = "False", IsCorrect = false, SortOrder = 2 }
                        ]},
                        new Question { Type = QuestionType.MultipleChoice, SortOrder = 3, Prompt = "What is a 'volumes-from' pattern in Docker?", ExplanationText = "'--volumes-from <container>' mounts all volumes from another container. Often used with 'data container' patterns (now largely replaced by named volumes).", AnswerOptions =
                        [
                            new AnswerOption { Text = "Mount all volumes from another container", IsCorrect = true, SortOrder = 1 },
                            new AnswerOption { Text = "Copy volumes to another host", IsCorrect = false, SortOrder = 2 },
                            new AnswerOption { Text = "Create volumes from a compose file", IsCorrect = false, SortOrder = 3 },
                            new AnswerOption { Text = "Import volumes from Docker Hub", IsCorrect = false, SortOrder = 4 }
                        ]},
                        new Question { Type = QuestionType.FillInTheBlank, SortOrder = 4, Prompt = "Complete the :___ suffix for a read-write mount (the default, but explicit) e.g. -v data:/app:___", ExplanationText = "':rw' explicitly sets read-write mode. It's the default, but writing it makes your intent clear.", HintText = "Read + write = ?", AnswerOptions =
                        [
                            new AnswerOption { Text = "rw", IsCorrect = true, SortOrder = 1 }
                        ]},
                        new Question { Type = QuestionType.TrueFalse, SortOrder = 5, Prompt = "Deleting a container also deletes its named volumes by default.", ExplanationText = "False — 'docker rm' only removes the container. Named volumes persist. Use 'docker rm -v' to also remove anonymous volumes (not named ones).", AnswerOptions =
                        [
                            new AnswerOption { Text = "True", IsCorrect = false, SortOrder = 1 },
                            new AnswerOption { Text = "False", IsCorrect = true, SortOrder = 2 }
                        ]}
                    ]}
                ]}
            ]
        });

        db.SaveChanges();
    }
}
