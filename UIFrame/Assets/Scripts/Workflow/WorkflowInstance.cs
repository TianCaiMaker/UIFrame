using System;
using System.Collections.Generic;

namespace Workflow
{
    public sealed class WorkflowInstance
    {
        private readonly Dictionary<string, WorkflowNodeInstance> nodeInstances;
        private readonly List<WorkflowNodeInstance> activeNodes = new List<WorkflowNodeInstance>();

        public WorkflowSpec Spec { get; }
        public WorkflowRunState State { get; private set; }
        public bool IsCompleted => State == WorkflowRunState.Completed;

        private bool hasStarted;

        public WorkflowInstance(WorkflowSpec spec, IWorkflowNodeRunnerFactory runnerFactory)
        {
            Spec = spec ?? throw new ArgumentNullException(nameof(spec));
            if (runnerFactory == null)
            {
                throw new ArgumentNullException(nameof(runnerFactory));
            }

            nodeInstances = new Dictionary<string, WorkflowNodeInstance>(StringComparer.Ordinal);
            foreach (KeyValuePair<string, WorkflowNodeSpec> pair in spec.Nodes)
            {
                nodeInstances.Add(pair.Key, new WorkflowNodeInstance(pair.Value, runnerFactory.CreateRunner(pair.Value)));
            }

            State = WorkflowRunState.Running;
        }

        public WorkflowRunState Tick(float deltaTime)
        {
            if (deltaTime < 0f)
            {
                throw new ArgumentOutOfRangeException(nameof(deltaTime), "Workflow deltaTime cannot be negative.");
            }

            if (State == WorkflowRunState.Completed)
            {
                return State;
            }

            if (!hasStarted)
            {
                hasStarted = true;
                ActivateNode(Spec.Root);
            }

            if (activeNodes.Count == 0)
            {
                State = WorkflowRunState.Completed;
                return State;
            }

            List<WorkflowNodeInstance> tickSnapshot = new List<WorkflowNodeInstance>(activeNodes);
            for (int index = 0; index < tickSnapshot.Count; index++)
            {
                WorkflowNodeInstance nodeInstance = tickSnapshot[index];
                if (nodeInstance.State == WorkflowNodeRunState.Completed)
                {
                    continue;
                }

                TickNode(nodeInstance, deltaTime);
            }

            if (activeNodes.Count == 0)
            {
                State = WorkflowRunState.Completed;
            }

            return State;
        }

        public bool TryGetNodeState(string nodeId, out WorkflowNodeRunState state)
        {
            if (nodeInstances.TryGetValue(nodeId, out WorkflowNodeInstance nodeInstance))
            {
                state = nodeInstance.State;
                return true;
            }

            state = WorkflowNodeRunState.NotStarted;
            return false;
        }

        private void TickNode(WorkflowNodeInstance nodeInstance, float deltaTime)
        {
            if (nodeInstance.State == WorkflowNodeRunState.Completed)
            {
                return;
            }

            WorkflowTickResult tickResult = nodeInstance.Tick(deltaTime);
            if (tickResult.State != WorkflowNodeRunState.Completed)
            {
                return;
            }

            activeNodes.Remove(nodeInstance);

            float remainingDeltaTime = Math.Max(0f, deltaTime - tickResult.ConsumedDeltaTime);
            IReadOnlyList<WorkflowNodeSpec> children = nodeInstance.Spec.Children;
            for (int index = 0; index < children.Count; index++)
            {
                WorkflowNodeInstance childInstance = ActivateNode(children[index]);
                if (remainingDeltaTime > 0f)
                {
                    TickNode(childInstance, remainingDeltaTime);
                }
            }
        }

        private WorkflowNodeInstance ActivateNode(WorkflowNodeSpec nodeSpec)
        {
            WorkflowNodeInstance nodeInstance = nodeInstances[nodeSpec.NodeId];
            if (nodeInstance.State == WorkflowNodeRunState.NotStarted)
            {
                activeNodes.Add(nodeInstance);
            }

            return nodeInstance;
        }
    }
}