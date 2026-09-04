using System;

namespace Workflow
{
    public sealed class WorkflowBuiltInRunnerFactory : IWorkflowNodeRunnerFactory
    {
        public IWorkflowNodeRunner CreateRunner(WorkflowNodeSpec nodeSpec)
        {
            if (nodeSpec == null)
            {
                throw new ArgumentNullException(nameof(nodeSpec));
            }

            if (string.IsNullOrWhiteSpace(nodeSpec.RunnerKey) || nodeSpec.RunnerKey == WorkflowRunnerKeys.Delay)
            {
                return new DelayWorkflowNodeRunner(nodeSpec.Duration);
            }

            throw new InvalidOperationException($"Unknown workflow runner '{nodeSpec.RunnerKey}' on node '{nodeSpec.NodeId}'.");
        }
    }
}