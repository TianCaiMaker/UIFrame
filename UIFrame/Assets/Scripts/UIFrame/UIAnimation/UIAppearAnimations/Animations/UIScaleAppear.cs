using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace UIFrame
{
    public class UIScaleAppear : UIAppearValueAnimation<Transform, Vector3>
    {
        protected override Vector3 GetCurrentValue(Transform targetComponent)
        {
            return targetComponent.localScale;
        }

        protected override void SetCurrentValue(Transform targetComponent, Vector3 value)
        {
            targetComponent.localScale = value;
        }

        protected override Vector3 GetAppearStartValue(Vector3 visibleValue)
        {
            return Vector3.zero;
        }

        protected override Vector3 GetDisappearTargetValue(Vector3 visibleValue)
        {
            return Vector3.zero;
        }

        protected override Vector3 LerpValue(Vector3 fromValue, Vector3 toValue, float progress)
        {
            return Vector3.LerpUnclamped(fromValue, toValue, progress);
        }
    }
}