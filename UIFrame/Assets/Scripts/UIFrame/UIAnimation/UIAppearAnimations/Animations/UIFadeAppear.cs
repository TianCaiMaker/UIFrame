using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace UIFrame
{
    [RequireComponent(typeof(CanvasGroup))]
    public class UIFadeAppear : UIAppearValueAnimation<CanvasGroup, float>
    {
        protected override float GetCurrentValue(CanvasGroup targetComponent)
        {
            return targetComponent.alpha;
        }

        protected override void SetCurrentValue(CanvasGroup targetComponent, float value)
        {
            targetComponent.alpha = value;
        }

        protected override float GetAppearStartValue(float visibleValue)
        {
            return 0f;
        }

        protected override float GetDisappearTargetValue(float visibleValue)
        {
            return 0f;
        }

        protected override float LerpValue(float fromValue, float toValue, float progress)
        {
            return Mathf.LerpUnclamped(fromValue, toValue, progress);
        }
    }
}