using System;

namespace Workflow
{
    public abstract class WorkflowNodeRunner : IWorkflowNodeRunner
    {
        protected WorkflowNodeSpec Spec { get; private set; }
        protected WorkflowAsset Asset => Spec.SourceAsset;

        void IWorkflowNodeRunner.OnStart(WorkflowNodeSpec nodeSpec)
        {
            Spec = nodeSpec ?? throw new ArgumentNullException(nameof(nodeSpec));
            OnStart();
        }

        WorkflowTickResult IWorkflowNodeRunner.Tick(float deltaTime)
        {
            return OnTick(deltaTime);
        }

        protected virtual void OnStart()
        {
        }

        protected abstract WorkflowTickResult OnTick(float deltaTime);
    }

    public abstract class WorkflowNodeRunner<TAsset> : WorkflowNodeRunner where TAsset : WorkflowAsset
    {
        protected TAsset TypedAsset => (TAsset)Asset;
    }
}