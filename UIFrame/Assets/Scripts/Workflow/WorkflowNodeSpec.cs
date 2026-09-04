using System.Collections.Generic;

namespace Workflow
{
    public sealed class WorkflowNodeSpec
    {
        public WorkflowAsset SourceAsset { get; }
        public string NodeId { get; }
        public string RunnerKey { get; }
        public float Duration { get; }
        public IReadOnlyList<WorkflowNodeSpec> Children { get; }

        public WorkflowNodeSpec(WorkflowAsset sourceAsset, string nodeId, string runnerKey, float duration, IReadOnlyList<WorkflowNodeSpec> children)
        {
            SourceAsset = sourceAsset;
            NodeId = nodeId;
            RunnerKey = runnerKey;
            Duration = duration;
            Children = children;
        }
    }
}