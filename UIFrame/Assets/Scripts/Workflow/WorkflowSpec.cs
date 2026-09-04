using System;
using System.Collections.Generic;

namespace Workflow
{
    public sealed class WorkflowSpec
    {
        private readonly Dictionary<string, WorkflowNodeSpec> nodes;

        public string RootNodeId { get; }
        public WorkflowNodeSpec Root { get; }
        public IReadOnlyDictionary<string, WorkflowNodeSpec> Nodes => nodes;

        private WorkflowSpec(string rootNodeId, WorkflowNodeSpec root, Dictionary<string, WorkflowNodeSpec> nodes)
        {
            RootNodeId = rootNodeId;
            Root = root;
            this.nodes = nodes;
        }

        public static WorkflowSpec Create(WorkflowAsset workflowAsset)
        {
            if (workflowAsset == null)
            {
                throw new ArgumentNullException(nameof(workflowAsset));
            }

            Dictionary<string, WorkflowNodeSpec> specMap = new Dictionary<string, WorkflowNodeSpec>(StringComparer.Ordinal);
            Dictionary<string, WorkflowAsset> nodeIdMap = new Dictionary<string, WorkflowAsset>(StringComparer.Ordinal);
            HashSet<WorkflowAsset> visitStack = new HashSet<WorkflowAsset>();
            HashSet<WorkflowAsset> builtNodes = new HashSet<WorkflowAsset>();

            WorkflowNodeSpec BuildNode(WorkflowAsset nodeAsset)
            {
                if (nodeAsset == null)
                {
                    throw new InvalidOperationException("Workflow node reference cannot be null.");
                }

                if (builtNodes.Contains(nodeAsset))
                {
                    throw new InvalidOperationException($"Workflow node '{nodeAsset.name}' is referenced by multiple parents. Workflow must be a tree.");
                }

                if (!visitStack.Add(nodeAsset))
                {
                    throw new InvalidOperationException($"Workflow cycle detected at node '{nodeAsset.name}'.");
                }

                if (string.IsNullOrWhiteSpace(nodeAsset.NodeId))
                {
                    throw new InvalidOperationException($"Workflow node '{nodeAsset.name}' is missing a node id.");
                }

                if (nodeIdMap.TryGetValue(nodeAsset.NodeId, out WorkflowAsset existingNode)
                    && existingNode != nodeAsset)
                {
                    throw new InvalidOperationException($"Duplicate workflow node id '{nodeAsset.NodeId}'.");
                }

                nodeIdMap[nodeAsset.NodeId] = nodeAsset;

                List<WorkflowNodeSpec> childSpecs = new List<WorkflowNodeSpec>(nodeAsset.Children.Count);
                for (int index = 0; index < nodeAsset.Children.Count; index++)
                {
                    WorkflowAsset childNode = nodeAsset.Children[index];
                    childSpecs.Add(BuildNode(childNode));
                }

                visitStack.Remove(nodeAsset);
                builtNodes.Add(nodeAsset);

                WorkflowNodeSpec nodeSpec = new WorkflowNodeSpec(
                    nodeAsset,
                    nodeAsset.NodeId,
                    nodeAsset.RunnerKey,
                    nodeAsset.Duration,
                    childSpecs);

                specMap.Add(nodeAsset.NodeId, nodeSpec);
                return nodeSpec;
            }

            WorkflowNodeSpec root = BuildNode(workflowAsset);
            return new WorkflowSpec(root.NodeId, root, specMap);
        }

        public WorkflowInstance CreateInstance(IWorkflowNodeRunnerFactory runnerFactory = null)
        {
            return new WorkflowInstance(this, runnerFactory ?? new WorkflowBuiltInRunnerFactory());
        }
    }
}