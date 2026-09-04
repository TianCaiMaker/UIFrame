namespace Workflow
{
    public interface IWorkflowNodeRunner
    {
        void OnStart(WorkflowNodeSpec nodeSpec);
        WorkflowTickResult Tick(float deltaTime);
    }
}