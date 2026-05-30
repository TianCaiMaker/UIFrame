using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
namespace UIFrame
{
    public class PopupWindow : WindowOpenClose
    {
        public string layerName = "";
        public event Action<PopupWindow> OnPopupOpened;
        public event Action<PopupWindow> OnPopupClosed;
        public override void Open()
        {
            base.Open();
            OnPopupOpened?.Invoke(this);
        }
        public override void ForceClose()
        {
            base.ForceClose();
            OnPopupClosed?.Invoke(this);
        }
    }
}