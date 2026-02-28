using LearnDevOps.Models.Domain;

namespace LearnDevOps.Data.SeedData;

public class KubernetesSeedData : ITrackSeedData
{
    public void Seed(AppDbContext db)
    {
        if (db.Tracks.Any(t => t.Slug == "kubernetes")) return;

        db.Tracks.Add(new Track
        {
            Name = "Kubernetes",
            Slug = "kubernetes",
            Description = "Master container orchestration — pods, deployments, services, and storage.",
            Emoji = "☸️",
            ColorHex = "#326ce5",
            SortOrder = 2,
            UnlockThresholdPercent = 50,
            Units =
            [
                new Unit { Title = "K8s Concepts", Emoji = "🧠", SortOrder = 1, Description = "Clusters, nodes, pods, and the control plane.", Lessons =
                [
                    new Lesson { Title = "What Is Kubernetes?", Emoji = "❓", SortOrder = 1, XpReward = 10, XpPerfectBonus = 5, Questions =
                    [
                        new Question { Type = QuestionType.MultipleChoice, SortOrder = 1, Prompt = "What problem does Kubernetes primarily solve?", ExplanationText = "Kubernetes automates deployment, scaling, and management of containerised applications across clusters.", AnswerOptions = [ new AnswerOption { Text = "Orchestrating containers at scale", IsCorrect = true, SortOrder = 1 }, new AnswerOption { Text = "Building Docker images faster", IsCorrect = false, SortOrder = 2 }, new AnswerOption { Text = "Replacing Docker entirely", IsCorrect = false, SortOrder = 3 }, new AnswerOption { Text = "Managing cloud billing", IsCorrect = false, SortOrder = 4 }]},
                        new Question { Type = QuestionType.TrueFalse, SortOrder = 2, Prompt = "A Pod is the smallest deployable unit in Kubernetes.", ExplanationText = "True — a Pod contains one or more containers sharing network and storage.", AnswerOptions = [ new AnswerOption { Text = "True", IsCorrect = true, SortOrder = 1 }, new AnswerOption { Text = "False", IsCorrect = false, SortOrder = 2 }]},
                        new Question { Type = QuestionType.FillInTheBlank, SortOrder = 3, Prompt = "Kubernetes is often abbreviated as ___ (K + 8 letters + s)", ExplanationText = "K8s — K, 8 middle letters, s.", HintText = "K + number + s", AnswerOptions = [ new AnswerOption { Text = "K8s", IsCorrect = true, SortOrder = 1 }]}
                    ]},
                    new Lesson { Title = "Cluster Architecture", Emoji = "🏛️", SortOrder = 2, XpReward = 10, XpPerfectBonus = 5, Questions =
                    [
                        new Question { Type = QuestionType.MultipleChoice, SortOrder = 1, Prompt = "Which component stores all Kubernetes cluster data?", ExplanationText = "etcd is the distributed key-value store holding the entire cluster state.", AnswerOptions = [ new AnswerOption { Text = "etcd", IsCorrect = true, SortOrder = 1 }, new AnswerOption { Text = "kube-proxy", IsCorrect = false, SortOrder = 2 }, new AnswerOption { Text = "kubelet", IsCorrect = false, SortOrder = 3 }, new AnswerOption { Text = "CoreDNS", IsCorrect = false, SortOrder = 4 }]},
                        new Question { Type = QuestionType.MultipleChoice, SortOrder = 2, Prompt = "Which control plane component assigns pods to nodes?", ExplanationText = "kube-scheduler watches for unscheduled pods and assigns them to nodes.", AnswerOptions = [ new AnswerOption { Text = "kube-scheduler", IsCorrect = true, SortOrder = 1 }, new AnswerOption { Text = "kube-apiserver", IsCorrect = false, SortOrder = 2 }, new AnswerOption { Text = "kubelet", IsCorrect = false, SortOrder = 3 }, new AnswerOption { Text = "kube-controller-manager", IsCorrect = false, SortOrder = 4 }]},
                        new Question { Type = QuestionType.FillInTheBlank, SortOrder = 3, Prompt = "The agent on every worker node that ensures containers are running is the ___", ExplanationText = "The kubelet communicates with the API server and manages containers on its node.", HintText = "Node-level agent", AnswerOptions = [ new AnswerOption { Text = "kubelet", IsCorrect = true, SortOrder = 1 }]}
                    ]}
                ]},
                new Unit { Title = "kubectl & Workloads", Emoji = "⌨️", SortOrder = 2, Description = "Essential commands and workload management.", Lessons =
                [
                    new Lesson { Title = "Essential Commands", Emoji = "🔑", SortOrder = 1, XpReward = 10, XpPerfectBonus = 5, Questions =
                    [
                        new Question { Type = QuestionType.FillInTheBlank, SortOrder = 1, Prompt = "Complete: kubectl ___ pods (to list all pods)", ExplanationText = "'kubectl get pods' lists pods. Add -o wide for detail or -A for all namespaces.", HintText = "Retrieve/fetch", AnswerOptions = [ new AnswerOption { Text = "get", IsCorrect = true, SortOrder = 1 }]},
                        new Question { Type = QuestionType.MultipleChoice, SortOrder = 2, Prompt = "Which command applies a YAML manifest declaratively?", ExplanationText = "'kubectl apply -f' creates or updates resources from a manifest file.", AnswerOptions = [ new AnswerOption { Text = "kubectl apply -f manifest.yaml", IsCorrect = true, SortOrder = 1 }, new AnswerOption { Text = "kubectl run -f manifest.yaml", IsCorrect = false, SortOrder = 2 }, new AnswerOption { Text = "kubectl create manifest.yaml", IsCorrect = false, SortOrder = 3 }, new AnswerOption { Text = "kubectl deploy manifest.yaml", IsCorrect = false, SortOrder = 4 }]},
                        new Question { Type = QuestionType.TrueFalse, SortOrder = 3, Prompt = "'kubectl exec -it mypod -- /bin/sh' opens a shell inside a running pod.", ExplanationText = "True — exec runs commands in a running pod. -it makes it interactive.", AnswerOptions = [ new AnswerOption { Text = "True", IsCorrect = true, SortOrder = 1 }, new AnswerOption { Text = "False", IsCorrect = false, SortOrder = 2 }]}
                    ]},
                    new Lesson { Title = "Deployments", Emoji = "🚀", SortOrder = 2, XpReward = 10, XpPerfectBonus = 5, Questions =
                    [
                        new Question { Type = QuestionType.MultipleChoice, SortOrder = 1, Prompt = "What does a Kubernetes Deployment manage?", ExplanationText = "A Deployment manages a ReplicaSet of pods with rolling update and rollback support.", AnswerOptions = [ new AnswerOption { Text = "A ReplicaSet of pods with rolling update support", IsCorrect = true, SortOrder = 1 }, new AnswerOption { Text = "A single non-restartable pod", IsCorrect = false, SortOrder = 2 }, new AnswerOption { Text = "Network policies between pods", IsCorrect = false, SortOrder = 3 }, new AnswerOption { Text = "Persistent storage volumes", IsCorrect = false, SortOrder = 4 }]},
                        new Question { Type = QuestionType.FillInTheBlank, SortOrder = 2, Prompt = "Complete: kubectl rollout ___ deployment/myapp (to revert to the previous version)", ExplanationText = "'kubectl rollout undo' reverts to the previous ReplicaSet revision.", HintText = "Revert/reverse", AnswerOptions = [ new AnswerOption { Text = "undo", IsCorrect = true, SortOrder = 1 }]},
                        new Question { Type = QuestionType.TrueFalse, SortOrder = 3, Prompt = "A rolling update replaces all pods at once.", ExplanationText = "False — rolling updates replace pods incrementally for zero-downtime deploys.", AnswerOptions = [ new AnswerOption { Text = "True", IsCorrect = false, SortOrder = 1 }, new AnswerOption { Text = "False", IsCorrect = true, SortOrder = 2 }]}
                    ]}
                ]},
                new Unit { Title = "Services & Storage", Emoji = "🌐", SortOrder = 3, Description = "Exposing apps and persisting data.", Lessons =
                [
                    new Lesson { Title = "Service Types", Emoji = "🔌", SortOrder = 1, XpReward = 10, XpPerfectBonus = 5, Questions =
                    [
                        new Question { Type = QuestionType.MultipleChoice, SortOrder = 1, Prompt = "Which Service type is internal-only (default)?", ExplanationText = "ClusterIP creates a virtual IP reachable only within the cluster.", AnswerOptions = [ new AnswerOption { Text = "ClusterIP", IsCorrect = true, SortOrder = 1 }, new AnswerOption { Text = "NodePort", IsCorrect = false, SortOrder = 2 }, new AnswerOption { Text = "LoadBalancer", IsCorrect = false, SortOrder = 3 }, new AnswerOption { Text = "ExternalName", IsCorrect = false, SortOrder = 4 }]},
                        new Question { Type = QuestionType.TrueFalse, SortOrder = 2, Prompt = "A LoadBalancer Service provisions an external LB in cloud environments.", ExplanationText = "True — in AWS/Azure/GCP, it creates a cloud load balancer automatically.", AnswerOptions = [ new AnswerOption { Text = "True", IsCorrect = true, SortOrder = 1 }, new AnswerOption { Text = "False", IsCorrect = false, SortOrder = 2 }]},
                        new Question { Type = QuestionType.FillInTheBlank, SortOrder = 3, Prompt = "A Service uses a ___ to select which pods receive traffic", ExplanationText = "A selector matches labels on pods to route traffic.", HintText = "Matches pod labels", AnswerOptions = [ new AnswerOption { Text = "selector", IsCorrect = true, SortOrder = 1 }]}
                    ]},
                    new Lesson { Title = "Persistent Storage", Emoji = "💾", SortOrder = 2, XpReward = 10, XpPerfectBonus = 5, Questions =
                    [
                        new Question { Type = QuestionType.MultipleChoice, SortOrder = 1, Prompt = "What is a PersistentVolumeClaim (PVC)?", ExplanationText = "A PVC is how pods request storage. Kubernetes binds it to a matching PersistentVolume.", AnswerOptions = [ new AnswerOption { Text = "A pod's request for persistent storage", IsCorrect = true, SortOrder = 1 }, new AnswerOption { Text = "A cloud disk backup", IsCorrect = false, SortOrder = 2 }, new AnswerOption { Text = "A temporary in-memory volume", IsCorrect = false, SortOrder = 3 }, new AnswerOption { Text = "A ConfigMap variant for files", IsCorrect = false, SortOrder = 4 }]},
                        new Question { Type = QuestionType.TrueFalse, SortOrder = 2, Prompt = "StorageClasses enable dynamic PV provisioning.", ExplanationText = "True — a StorageClass defines a provisioner to automatically create PVs when PVCs are submitted.", AnswerOptions = [ new AnswerOption { Text = "True", IsCorrect = true, SortOrder = 1 }, new AnswerOption { Text = "False", IsCorrect = false, SortOrder = 2 }]},
                        new Question { Type = QuestionType.MultipleChoice, SortOrder = 3, Prompt = "Which access mode allows a volume to be mounted read-write by many nodes?", ExplanationText = "ReadWriteMany (RWX) allows simultaneous read-write from multiple nodes.", AnswerOptions = [ new AnswerOption { Text = "ReadWriteMany", IsCorrect = true, SortOrder = 1 }, new AnswerOption { Text = "ReadWriteOnce", IsCorrect = false, SortOrder = 2 }, new AnswerOption { Text = "ReadOnlyMany", IsCorrect = false, SortOrder = 3 }, new AnswerOption { Text = "WriteMany", IsCorrect = false, SortOrder = 4 }]}
                    ]}
                ]}
            ]
        });
        db.SaveChanges();
    }
}
