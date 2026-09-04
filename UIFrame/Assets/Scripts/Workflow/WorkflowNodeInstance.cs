using System;

namespace Workflow
{
    internal sealed class WorkflowNodeInstance
    {
        public WorkflowNodeSpec Spec { get; }
        public WorkflowNodeRunState State { get; private set; }

        private readonly IWorkflowNodeRunner runner;

        public WorkflowNodeInstance(WorkflowNodeSpec spec, IWorkflowNodeRunner runner)
        {
            Spec = spec ?? throw new ArgumentNullException(nameof(spec));
            this.runner = runner ?? throw new ArgumentNullException(nameof(runner));
            State = WorkflowNodeRunState.NotStarted;
        }

        public WorkflowTickResult Tick(float deltaTime)
        {
            if (State == WorkflowNodeRunState.Completed)
            {
                return WorkflowTickResult.Completed(0f);
            }

            if (State == WorkflowNodeRunState.NotStarted)
            {
                runner.OnStart(Spec);
                State = WorkflowNodeRunState.Running;
            }

            WorkflowTickResult tickResult = runner.Tick(deltaTime);
            if (tickResult.State == WorkflowNodeRunState.Completed)
            {
                State = WorkflowNodeRunState.Completed;
            }

            return tickResult;
        }
    }
}