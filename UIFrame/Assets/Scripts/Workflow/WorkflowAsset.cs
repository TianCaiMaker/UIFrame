using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Workflow
{
    public abstract class WorkflowAsset : ScriptableObject,IWorkflowAsset
    {
        [SerializeField]
        List<WorkflowAsset> children = new List<WorkflowAsset>();
        public List<WorkflowAsset> Children => children;
        public abstract WorkflowSpec CreateSpec();
    }
}
