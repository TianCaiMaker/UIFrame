namespace Workflow
{
    public readonly struct WorkflowTickResult
    {
        public WorkflowNodeRunState State { get; }
        public float ConsumedDeltaTime { get; }

        public WorkflowTickResult(WorkflowNodeRunState state, float consumedDeltaTime)
        {
            State = state;
            ConsumedDeltaTime = consumedDeltaTime;
        }

        public static WorkflowTickResult Running(float consumedDeltaTime)
        {
            return new WorkflowTickResult(WorkflowNodeRunState.Running, consumedDeltaTime);
        }

        public static WorkflowTickResult Completed(float consumedDeltaTime)
        {
            return new WorkflowTickResult(WorkflowNodeRunState.Completed, consumedDeltaTime);
        }
    }
}