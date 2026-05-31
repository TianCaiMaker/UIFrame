using System.Collections;
using UnityEngine;
namespace UIFrame
{
    public abstract class UIAppearValueAnimation<TComponent, TValue> : UIAppearBase where TComponent : Component
    {
        [SerializeField]
        private bool restartAppearWhenTriggeredDuringAppear = true;

        private Coroutine animationCoroutine;
        private TComponent targetComponent;
        private TValue cachedVisibleValue;
        private bool hasCachedVisibleValue;

        public override void Appear()
        {
            TComponent currentTargetComponent = GetTargetComponent();
            if (currentTargetComponent == null)
            {
                return;
            }

            CacheVisibleValueIfNeeded(currentTargetComponent);

            if (animationState == AppearAnimationState.Appearing && !restartAppearWhenTriggeredDuringAppear)
            {
                return;
            }

            TValue fromValue = animationState == AppearAnimationState.Disappearing
                ? GetCurrentValue(currentTargetComponent)
                : GetAppearStartValue(cachedVisibleValue);

            PlayAnimation(fromValue, cachedVisibleValue, AppearDuration, AppearAnimationState.Appearing);
        }

        public override void Disappear()
        {
            TComponent currentTargetComponent = GetTargetComponent();
            if (currentTargetComponent == null)
            {
                return;
            }

            CacheVisibleValueIfNeeded(currentTargetComponent);
            PlayAnimation(GetCurrentValue(currentTargetComponent), GetDisappearTargetValue(cachedVisibleValue), DisappearDuration, AppearAnimationState.Disappearing);
        }

        protected TComponent GetTargetComponent()
        {
            if (targetComponent == null)
            {
                targetComponent = GetComponent<TComponent>();
            }

            return targetComponent;
        }

        protected abstract TValue GetCurrentValue(TComponent targetComponent);

        protected abstract void SetCurrentValue(TComponent targetComponent, TValue value);

        protected abstract TValue GetAppearStartValue(TValue visibleValue);

        protected abstract TValue GetDisappearTargetValue(TValue visibleValue);

        protected abstract TValue LerpValue(TValue fromValue, TValue toValue, float progress);

        private void CacheVisibleValueIfNeeded(TComponent currentTargetComponent)
        {
            if (hasCachedVisibleValue)
            {
                return;
            }

            cachedVisibleValue = GetCurrentValue(currentTargetComponent);
            hasCachedVisibleValue = true;
        }

        private void PlayAnimation(TValue fromValue, TValue toValue, float duration, AppearAnimationState targetState)
        {
            TComponent currentTargetComponent = GetTargetComponent();
            if (currentTargetComponent == null)
            {
                return;
            }

            animationState = targetState;

            if (animationCoroutine != null)
            {
                StopCoroutine(animationCoroutine);
            }

            if (duration <= 0f)
            {
                SetCurrentValue(currentTargetComponent, toValue);
                animationCoroutine = null;
                animationState = AppearAnimationState.None;
                return;
            }

            SetCurrentValue(currentTargetComponent, fromValue);
            animationCoroutine = StartCoroutine(AnimationCoroutine(fromValue, toValue, duration));
        }

        private IEnumerator AnimationCoroutine(TValue fromValue, TValue toValue, float duration)
        {
            TComponent currentTargetComponent = GetTargetComponent();
            if (currentTargetComponent == null)
            {
                animationState = AppearAnimationState.None;
                animationCoroutine = null;
                yield break;
            }

            float elapsed = 0f;

            while (elapsed < duration)
            {
                elapsed += Time.deltaTime;
                float progress = Mathf.Clamp01(elapsed / duration);
                SetCurrentValue(currentTargetComponent, LerpValue(fromValue, toValue, progress));
                yield return null;
            }

            SetCurrentValue(currentTargetComponent, toValue);
            animationState = AppearAnimationState.None;
            animationCoroutine = null;
        }
    }
}