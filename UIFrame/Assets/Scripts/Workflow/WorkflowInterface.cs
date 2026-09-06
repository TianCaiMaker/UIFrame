using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Workflow
{
    public interface IWorkflowAsset
    {
        public List<WorkflowAsset> Children { get; }
        public WorkflowSpec CreateSpec();
    }
}