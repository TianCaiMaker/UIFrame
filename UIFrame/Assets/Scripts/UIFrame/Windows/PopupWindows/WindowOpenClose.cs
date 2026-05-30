using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
namespace UIFrame
{
    public class WindowOpenClose : MonoBehaviour
    {
        public UnityEvent onOpenPanel;
        public UnityEvent onClosePanel;
        public bool IsOpen{ get; protected set; } = false;
        public bool closeImmediately = true;
        public virtual void Open()
        {
            if (IsOpen)
            {
                return;
            }
            IsOpen = true;
            gameObject.SetActive(true);
            onOpenPanel?.Invoke();
        }
        public virtual void Close()
        {
            if (!IsOpen)
            {
                return;
            }
            IsOpen = false;
            if (closeImmediately)
            {
                ForceClose();
            }
        }

        public virtual void ForceClose()
        {
            IsOpen = false;
            gameObject.SetActive(false);
            onClosePanel?.Invoke();
        }
    }
}