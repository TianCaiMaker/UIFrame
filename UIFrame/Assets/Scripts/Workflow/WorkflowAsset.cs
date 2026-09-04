using System.Collections.Generic;
using UnityEngine;

namespace Workflow
{
    [CreateAssetMenu(menuName = "Workflow/Workflow Asset", fileName = "WorkflowAsset")]
    public class WorkflowAsset : ScriptableObject
    {
        [SerializeField] private string nodeId;
        [SerializeField] private string runnerKey = WorkflowRunnerKeys.Delay;
        [Min(0f)]
        [SerializeField] private float duration;
        [SerializeField] private List<WorkflowAsset> children = new List<WorkflowAsset>();

        public string NodeId => nodeId;
        public string RunnerKey => runnerKey;
        public float Duration => duration;
        public IReadOnlyList<WorkflowAsset> Children => children;
    }
}
