using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
namespace UIFrame
{
    [RequireComponent(typeof(RectTransform))]
    public class WindowOpenClose : MonoBehaviour
    {
        public UnityEvent onOpenWindow;
        public UnityEvent onCloseWindow;
        public bool IsOpen { get; protected set; } = false;
        [HideInInspector]
        public bool closeImmediately = true;
        public virtual void Open()
        {
            if (IsOpen)
            {
                return;
            }
            IsOpen = true;
            gameObject.SetActive(true);
            onOpenWindow?.Invoke();
        }
        public virtual void Close()
        {
            onCloseWindow?.Invoke();
            if (closeImmediately)
            {
                ForceClose();
            }
        }

        public virtual void ForceClose()
        {
            IsOpen = false;
            gameObject.SetActive(false);
        }
    }
}