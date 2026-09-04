using System;

namespace Workflow
{
    internal sealed class DelayWorkflowNodeRunner : WorkflowNodeRunner
    {
        private readonly float duration;
        private float elapsedTime;

        public DelayWorkflowNodeRunner(float duration)
        {
            this.duration = Math.Max(0f, duration);
        }

        protected override void OnStart()
        {
            elapsedTime = 0f;
        }

        protected override WorkflowTickResult OnTick(float deltaTime)
        {
            if (duration <= 0f)
            {
                return WorkflowTickResult.Completed(0f);
            }

            float remainingTime = duration - elapsedTime;
            float consumedDeltaTime = Math.Min(deltaTime, remainingTime);
            elapsedTime += consumedDeltaTime;

            if (elapsedTime >= duration)
            {
                return WorkflowTickResult.Completed(consumedDeltaTime);
            }

            return WorkflowTickResult.Running(consumedDeltaTime);
        }
    }
}