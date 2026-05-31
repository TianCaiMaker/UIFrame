using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace UIFrame
{
    public class UIMoveAppear : UIAppearValueAnimation<RectTransform, Vector2>
    {
        [SerializeField]
        private Vector2 moveOffset;
        [SerializeField]
        private bool disappearUsesNegativeOffset;
        [SerializeField]
        private AnimationCurve moveEaseCurve = AnimationCurve.EaseInOut(0f, 0f, 1f, 1f);

        protected override Vector2 GetCurrentValue(RectTransform targetComponent)
        {
            return targetComponent.anchoredPosition;
        }

        protected override void SetCurrentValue(RectTransform targetComponent, Vector2 value)
        {
            targetComponent.anchoredPosition = value;
        }

        protected override Vector2 GetAppearStartValue(Vector2 visibleValue)
        {
            return visibleValue + moveOffset;
        }

        protected override Vector2 GetDisappearTargetValue(Vector2 visibleValue)
        {
            Vector2 disappearOffset = disappearUsesNegativeOffset ? -moveOffset : moveOffset;
            return visibleValue + disappearOffset;
        }

        protected override Vector2 LerpValue(Vector2 fromValue, Vector2 toValue, float progress)
        {
            float easedProgress = moveEaseCurve == null ? progress : moveEaseCurve.Evaluate(progress);
            return Vector2.LerpUnclamped(fromValue, toValue, easedProgress);
        }
    }
}