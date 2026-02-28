using LearnDevOps.Models.Domain;

namespace LearnDevOps.Data.SeedData;

public class AksSeedData : ITrackSeedData
{
    public void Seed(AppDbContext db)
    {
        if (db.Tracks.Any(t => t.Slug == "aks")) return;

        db.Tracks.Add(new Track
        {
            Name = "Azure Kubernetes Service",
            Slug = "aks",
            Description = "Deploy and manage Kubernetes on Azure — clusters, ACR, scaling, and monitoring.",
            Emoji = "☁️",
            ColorHex = "#0078d4",
            SortOrder = 3,
            UnlockThresholdPercent = 50,
            Units =
            [
                new Unit { Title = "AKS Overview", Emoji = "🌩️", SortOrder = 1, Description = "What is AKS and how it compares to self-managed K8s.", Lessons =
                [
                    new Lesson { Title = "What Is AKS?", Emoji = "❓", SortOrder = 1, XpReward = 10, XpPerfectBonus = 5, Questions =
                    [
                        new Question { Type = QuestionType.MultipleChoice, SortOrder = 1, Prompt = "What does Azure manage for you in AKS?", ExplanationText = "AKS manages the Kubernetes control plane (API server, etcd, scheduler). You manage worker nodes.", AnswerOptions = [ new AnswerOption { Text = "The Kubernetes control plane", IsCorrect = true, SortOrder = 1 }, new AnswerOption { Text = "Worker nodes and application code", IsCorrect = false, SortOrder = 2 }, new AnswerOption { Text = "Container images only", IsCorrect = false, SortOrder = 3 }, new AnswerOption { Text = "Nothing — AKS is fully self-managed", IsCorrect = false, SortOrder = 4 }]},
                        new Question { Type = QuestionType.TrueFalse, SortOrder = 2, Prompt = "AKS charges for the control plane nodes.", ExplanationText = "False — Azure provides the control plane for free. You pay only for worker node VMs and other Azure resources.", AnswerOptions = [ new AnswerOption { Text = "True", IsCorrect = false, SortOrder = 1 }, new AnswerOption { Text = "False", IsCorrect = true, SortOrder = 2 }]},
                        new Question { Type = QuestionType.FillInTheBlank, SortOrder = 3, Prompt = "Complete the Azure CLI command: az ___ create (to create an AKS cluster)", ExplanationText = "'az aks create' provisions a new AKS cluster with specified node count and size.", HintText = "Azure Kubernetes Service", AnswerOptions = [ new AnswerOption { Text = "aks", IsCorrect = true, SortOrder = 1 }]}
                    ]},
                    new Lesson { Title = "Creating AKS Clusters", Emoji = "🏗️", SortOrder = 2, XpReward = 10, XpPerfectBonus = 5, Questions =
                    [
                        new Question { Type = QuestionType.MultipleChoice, SortOrder = 1, Prompt = "Which CLI command gets AKS cluster credentials for kubectl?", ExplanationText = "'az aks get-credentials' merges the AKS cluster config into your ~/.kube/config.", AnswerOptions = [ new AnswerOption { Text = "az aks get-credentials", IsCorrect = true, SortOrder = 1 }, new AnswerOption { Text = "az aks login", IsCorrect = false, SortOrder = 2 }, new AnswerOption { Text = "kubectl aks connect", IsCorrect = false, SortOrder = 3 }, new AnswerOption { Text = "az aks kubeconfig", IsCorrect = false, SortOrder = 4 }]},
                        new Question { Type = QuestionType.TrueFalse, SortOrder = 2, Prompt = "AKS node pools allow mixing different VM sizes in the same cluster.", ExplanationText = "True — you can add multiple node pools with different VM SKUs for varied workload needs.", AnswerOptions = [ new AnswerOption { Text = "True", IsCorrect = true, SortOrder = 1 }, new AnswerOption { Text = "False", IsCorrect = false, SortOrder = 2 }]},
                        new Question { Type = QuestionType.MultipleChoice, SortOrder = 3, Prompt = "What is the system node pool in AKS?", ExplanationText = "The system pool runs critical pods like CoreDNS and metrics-server. User workloads should use separate user pools.", AnswerOptions = [ new AnswerOption { Text = "Runs critical system pods like CoreDNS", IsCorrect = true, SortOrder = 1 }, new AnswerOption { Text = "A pool dedicated to user applications", IsCorrect = false, SortOrder = 2 }, new AnswerOption { Text = "Azure's control plane VMs", IsCorrect = false, SortOrder = 3 }, new AnswerOption { Text = "A pool for CI/CD agents only", IsCorrect = false, SortOrder = 4 }]}
                    ]}
                ]},
                new Unit { Title = "ACR & Images", Emoji = "📦", SortOrder = 2, Description = "Azure Container Registry integration.", Lessons =
                [
                    new Lesson { Title = "Azure Container Registry", Emoji = "🏪", SortOrder = 1, XpReward = 10, XpPerfectBonus = 5, Questions =
                    [
                        new Question { Type = QuestionType.MultipleChoice, SortOrder = 1, Prompt = "What is Azure Container Registry (ACR)?", ExplanationText = "ACR is a managed Docker registry in Azure for storing and managing container images privately.", AnswerOptions = [ new AnswerOption { Text = "A managed private Docker registry on Azure", IsCorrect = true, SortOrder = 1 }, new AnswerOption { Text = "A Kubernetes cluster deployment tool", IsCorrect = false, SortOrder = 2 }, new AnswerOption { Text = "Azure's CDN for container images", IsCorrect = false, SortOrder = 3 }, new AnswerOption { Text = "A container orchestration alternative to AKS", IsCorrect = false, SortOrder = 4 }]},
                        new Question { Type = QuestionType.FillInTheBlank, SortOrder = 2, Prompt = "Complete: az aks update --attach-___ myacr (to connect ACR to AKS)", ExplanationText = "'--attach-acr' grants AKS's managed identity pull access to the specified ACR.", HintText = "Azure Container Registry abbreviation", AnswerOptions = [ new AnswerOption { Text = "acr", IsCorrect = true, SortOrder = 1 }]},
                        new Question { Type = QuestionType.TrueFalse, SortOrder = 3, Prompt = "ACR Tasks can automatically build images in the cloud without a local Docker daemon.", ExplanationText = "True — 'az acr build' builds images remotely in Azure, no local Docker required.", AnswerOptions = [ new AnswerOption { Text = "True", IsCorrect = true, SortOrder = 1 }, new AnswerOption { Text = "False", IsCorrect = false, SortOrder = 2 }]}
                    ]},
                    new Lesson { Title = "Image Deployment", Emoji = "🚀", SortOrder = 2, XpReward = 10, XpPerfectBonus = 5, Questions =
                    [
                        new Question { Type = QuestionType.MultipleChoice, SortOrder = 1, Prompt = "How do you reference an ACR image in a Kubernetes deployment?", ExplanationText = "Use the ACR login server: 'myacr.azurecr.io/myapp:v1' as the container image.", AnswerOptions = [ new AnswerOption { Text = "myacr.azurecr.io/myapp:v1", IsCorrect = true, SortOrder = 1 }, new AnswerOption { Text = "docker.io/myacr/myapp:v1", IsCorrect = false, SortOrder = 2 }, new AnswerOption { Text = "azure://myacr/myapp:v1", IsCorrect = false, SortOrder = 3 }, new AnswerOption { Text = "acr:myacr/myapp:v1", IsCorrect = false, SortOrder = 4 }]},
                        new Question { Type = QuestionType.TrueFalse, SortOrder = 2, Prompt = "AKS can pull images from ACR without explicit imagePullSecrets when attached.", ExplanationText = "True — attaching ACR to AKS sets up managed identity authentication automatically.", AnswerOptions = [ new AnswerOption { Text = "True", IsCorrect = true, SortOrder = 1 }, new AnswerOption { Text = "False", IsCorrect = false, SortOrder = 2 }]},
                        new Question { Type = QuestionType.FillInTheBlank, SortOrder = 3, Prompt = "Complete: az acr ___ -n myacr (to list images in the registry)", ExplanationText = "'az acr repository list' shows all repositories stored in the registry.", HintText = "List stored items", AnswerOptions = [ new AnswerOption { Text = "repository list", IsCorrect = true, SortOrder = 1 }]}
                    ]}
                ]},
                new Unit { Title = "Scaling & Monitoring", Emoji = "📈", SortOrder = 3, Description = "Auto-scaling and observability in AKS.", Lessons =
                [
                    new Lesson { Title = "Auto-Scaling", Emoji = "⚖️", SortOrder = 1, XpReward = 10, XpPerfectBonus = 5, Questions =
                    [
                        new Question { Type = QuestionType.MultipleChoice, SortOrder = 1, Prompt = "HPA in Kubernetes stands for:", ExplanationText = "Horizontal Pod Autoscaler scales the number of pod replicas based on CPU/memory or custom metrics.", AnswerOptions = [ new AnswerOption { Text = "Horizontal Pod Autoscaler", IsCorrect = true, SortOrder = 1 }, new AnswerOption { Text = "Host Performance Analyzer", IsCorrect = false, SortOrder = 2 }, new AnswerOption { Text = "High Priority Assignment", IsCorrect = false, SortOrder = 3 }, new AnswerOption { Text = "Hybrid Platform Adapter", IsCorrect = false, SortOrder = 4 }]},
                        new Question { Type = QuestionType.TrueFalse, SortOrder = 2, Prompt = "AKS Cluster Autoscaler adds/removes nodes based on pending pods.", ExplanationText = "True — if pods can't be scheduled due to insufficient resources, the cluster autoscaler adds nodes.", AnswerOptions = [ new AnswerOption { Text = "True", IsCorrect = true, SortOrder = 1 }, new AnswerOption { Text = "False", IsCorrect = false, SortOrder = 2 }]},
                        new Question { Type = QuestionType.MultipleChoice, SortOrder = 3, Prompt = "What does VPA (Vertical Pod Autoscaler) adjust?", ExplanationText = "VPA adjusts CPU/memory requests and limits per container, scaling vertically instead of adding replicas.", AnswerOptions = [ new AnswerOption { Text = "CPU and memory requests/limits per container", IsCorrect = true, SortOrder = 1 }, new AnswerOption { Text = "Number of pod replicas", IsCorrect = false, SortOrder = 2 }, new AnswerOption { Text = "Number of cluster nodes", IsCorrect = false, SortOrder = 3 }, new AnswerOption { Text = "Disk IOPS per volume", IsCorrect = false, SortOrder = 4 }]}
                    ]},
                    new Lesson { Title = "Monitoring with Azure", Emoji = "📊", SortOrder = 2, XpReward = 10, XpPerfectBonus = 5, Questions =
                    [
                        new Question { Type = QuestionType.MultipleChoice, SortOrder = 1, Prompt = "Which Azure service provides container-level monitoring for AKS?", ExplanationText = "Azure Monitor Container Insights collects metrics and logs from AKS clusters, nodes, and containers.", AnswerOptions = [ new AnswerOption { Text = "Azure Monitor Container Insights", IsCorrect = true, SortOrder = 1 }, new AnswerOption { Text = "Azure Advisor", IsCorrect = false, SortOrder = 2 }, new AnswerOption { Text = "Azure Security Center only", IsCorrect = false, SortOrder = 3 }, new AnswerOption { Text = "Azure Traffic Manager", IsCorrect = false, SortOrder = 4 }]},
                        new Question { Type = QuestionType.FillInTheBlank, SortOrder = 2, Prompt = "Complete: kubectl ___ node (to show CPU and memory usage of nodes)", ExplanationText = "'kubectl top node' shows resource consumption. Requires metrics-server to be installed.", HintText = "Resource usage command", AnswerOptions = [ new AnswerOption { Text = "top", IsCorrect = true, SortOrder = 1 }]},
                        new Question { Type = QuestionType.TrueFalse, SortOrder = 3, Prompt = "Azure Monitor can set alerts when AKS node CPU exceeds a threshold.", ExplanationText = "True — you can configure metric alerts in Azure Monitor for node and pod resource usage.", AnswerOptions = [ new AnswerOption { Text = "True", IsCorrect = true, SortOrder = 1 }, new AnswerOption { Text = "False", IsCorrect = false, SortOrder = 2 }]}
                    ]}
                ]}
            ]
        });
        db.SaveChanges();
    }
}
