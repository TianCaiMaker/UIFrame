namespace Workflow
{
    public interface IWorkflowNodeRunnerFactory
    {
        IWorkflowNodeRunner CreateRunner(WorkflowNodeSpec nodeSpec);
    }
}