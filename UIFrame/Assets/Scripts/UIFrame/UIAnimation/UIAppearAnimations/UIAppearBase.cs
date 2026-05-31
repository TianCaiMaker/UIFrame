using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
namespace UIFrame
{
    [RequireComponent(typeof(UIAppearAnimation))]
    public abstract class UIAppearBase : MonoBehaviour, IUIAppear
    {
        protected enum AppearAnimationState
        {
            None,
            Appearing,
            Disappearing
        }

        [SerializeField]
        private float appearDuration = 0.2f;
        [SerializeField]
        private float disappearDuration = 0.2f;

        protected AppearAnimationState animationState;

        public float AppearDuration
        {
            get => appearDuration;
            set
            {
                if (value < 0)
                {
                    appearDuration = 0;
                }
                else
                {
                    appearDuration = value;
                }
            }
        }
        public float DisappearDuration
        {
            get => disappearDuration;
            set
            {
                if (value < 0)
                {
                    disappearDuration = 0;
                }
                else
                {
                    disappearDuration = value;
                }
            }
        }

        public abstract void Appear();

        public abstract void Disappear();
    }
}
