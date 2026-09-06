using System.Collections.Generic;
using General;
namespace Workflow
{
    public class WorkflowRunner
    {
        private readonly WorkflowSpec rootSpec;
        private readonly List<WorkflowSpec> activeSpecs = new List<WorkflowSpec>();
        private readonly List<WorkflowSpec> tickBuffer = new List<WorkflowSpec>();
        private readonly Dictionary<WorkflowSpec, WorkflowNodeRunState> states = new Dictionary<WorkflowSpec, WorkflowNodeRunState>();

        public GeneralContext Context { get; }
        public WorkflowTickResult State { get; private set; } = WorkflowTickResult.Running;
        public bool HasStarted { get; private set; }
        public bool IsCompleted => State == WorkflowTickResult.Completed;

        public WorkflowRunner(IWorkflowAsset workflowAsset, GeneralContext workflowContext = null)
            : this(workflowAsset?.CreateSpec(), workflowContext)
        {
        }

        public WorkflowRunner(WorkflowSpec workflowSpec, GeneralContext workflowContext = null)
        {
            rootSpec = workflowSpec ?? throw new System.ArgumentNullException(nameof(workflowSpec));
            Context = workflowContext ?? new GeneralContext();
            rootSpec.SetContextRecursive(Context);
        }
        public WorkflowTickResult Tick()
        {
            if (State == WorkflowTickResult.Completed)
            {
                return State;
            }

            if (!HasStarted)
            {
                HasStarted = true;
                Activate(rootSpec);
            }

            if (activeSpecs.Count == 0)
            {
                State = WorkflowTickResult.Completed;
                return State;
            }

            tickBuffer.Clear();
            tickBuffer.AddRange(activeSpecs);

            for (int tickIndex = 0; tickIndex < tickBuffer.Count; tickIndex++)
            {
                WorkflowSpec workflowSpec = tickBuffer[tickIndex];
                if (GetNodeState(workflowSpec) == WorkflowNodeRunState.Completed)
                {
                    continue;
                }

                WorkflowTickResult tickResult = workflowSpec.Tick();
                if (tickResult == WorkflowTickResult.Running)
                {
                    continue;
                }

                Complete(workflowSpec);
            }

            if (activeSpecs.Count == 0)
            {
                State = WorkflowTickResult.Completed;
            }

            return State;
        }

        public WorkflowNodeRunState GetNodeState(WorkflowSpec workflowSpec)
        {
            if (workflowSpec == null)
            {
                throw new System.ArgumentNullException(nameof(workflowSpec));
            }

            if (states.TryGetValue(workflowSpec, out WorkflowNodeRunState state))
            {
                return state;
            }

            return WorkflowNodeRunState.NotStarted;
        }

        private void Complete(WorkflowSpec workflowSpec)
        {
            states[workflowSpec] = WorkflowNodeRunState.Completed;
            activeSpecs.Remove(workflowSpec);

            foreach (WorkflowSpec child in workflowSpec.Children)
            {
                if (Activate(child))
                {
                    tickBuffer.Add(child);
                }
            }
        }

        private bool Activate(WorkflowSpec workflowSpec)
        {
            WorkflowNodeRunState state = GetNodeState(workflowSpec);
            if (state != WorkflowNodeRunState.NotStarted)
            {
                return false;
            }

            states[workflowSpec] = WorkflowNodeRunState.Running;
            activeSpecs.Add(workflowSpec);
            return true;
        }
        
        public void Reset()
        {
            activeSpecs.Clear();
            tickBuffer.Clear();
            states.Clear();
            State = WorkflowTickResult.Running;
            HasStarted = false;
            // Reapply the context to the root spec so all specs have current Context
            rootSpec.SetContextRecursive(Context);
        }
    }
}

