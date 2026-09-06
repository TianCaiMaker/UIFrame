using System.Collections.Generic;
using General;
namespace Workflow
{
    public abstract class WorkflowSpec
    {
        protected List<WorkflowSpec> children = new List<WorkflowSpec>();
        protected GeneralContext workflowContext;
        public IReadOnlyList<WorkflowSpec> Children => children;

        public WorkflowSpec(IWorkflowAsset workflowAsset)
        {
            foreach (var child in workflowAsset.Children)
            {
                children.Add(child.CreateSpec());
            }
        }
        public void SetContext(GeneralContext workflowContext)
        {
            this.workflowContext = workflowContext;
        }
        
        public void SetContextRecursive(GeneralContext workflowContext)
        {
            this.workflowContext = workflowContext;
            foreach (var child in children)
            {
                child.SetContextRecursive(workflowContext);
            }
        }
        
        public abstract WorkflowTickResult Tick();
    }
    
}