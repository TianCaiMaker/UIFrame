using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace UIFrame
{
    [RequireComponent(typeof(WindowOpenClose))]
    public class UIAppearAnimation : MonoBehaviour
    {
        IUIAppear appearAnimation;
        WindowOpenClose windowOpenClose;
        private void Awake()
        {
            Init();
        }
        private void Init()
        {
            appearAnimation = GetComponent<IUIAppear>();
            windowOpenClose = GetComponent<WindowOpenClose>();
            windowOpenClose.onOpenWindow.AddListener(Appear);
            windowOpenClose.onCloseWindow.AddListener(Disappear);
        }
        public void Appear()
        {
            if (appearAnimation == null || windowOpenClose == null)
            {
                Init();
            }
            if (appearAnimation == null || windowOpenClose == null)
            {
                Debug.LogError("UIAppearAnimation requires a component that implements IUIAppear and WindowOpenClose on the same GameObject.");
                return;
            }
            windowOpenClose.closeImmediately = false;
            appearAnimation?.Appear();
        }
        public void Disappear()
        {
            if (appearAnimation == null || windowOpenClose == null)
            {
                Init();
            }
            if (appearAnimation == null || windowOpenClose == null)
            {
                Debug.LogError("UIAppearAnimation requires a component that implements IUIAppear and WindowOpenClose on the same GameObject.");
                return;
            }
            appearAnimation?.Disappear();
            StartCoroutine(CloseCouroutine());
        }

        IEnumerator CloseCouroutine()
        {
            yield return new WaitForSeconds(appearAnimation.DisappearDuration + 0.01f);
            WindowOpenClose windowOpenClose = GetComponent<WindowOpenClose>();
            if (windowOpenClose != null)
            {
                windowOpenClose.ForceClose();
            }
        }
    }
}