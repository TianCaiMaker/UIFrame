using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
namespace UIFrame
{
    public class PopupWindowManager : MonoBehaviour
    {
        List<PopupWindow> popupWindowList = new List<PopupWindow>();
        public Image windowMask;
        public Dictionary<string, List<PopupWindow>> layerDict = new();
        void Awake()
        {
            popupWindowList.Clear();
            popupWindowList.AddRange(GetComponentsInChildren<PopupWindow>(true));
            foreach (var popupWindow in popupWindowList)
            {
                popupWindow.OnPopupOpened += OnPopupWindowOpen;
                popupWindow.OnPopupClosed += OnPopupWindowClose;
                if (!string.IsNullOrEmpty(popupWindow.layerName))
                {
                    if (!layerDict.TryGetValue(popupWindow.layerName, out var list))
                    {
                        list = new List<PopupWindow>();
                        layerDict.Add(popupWindow.layerName, list);
                    }
                    list.Add(popupWindow);
                }
            }
            windowMask.transform.SetParent(transform, false);
            windowMask.transform.SetSiblingIndex(0);
            SetWindowMaskActive(false);
        }
        public void AddPopupWindow(PopupWindow popupWindow)
        {
            if (popupWindow == null)
            {
                return;
            }
            popupWindow.transform.SetParent(transform, false);
            if (!popupWindowList.Contains(popupWindow))
            {
                popupWindowList.Add(popupWindow);
                popupWindow.OnPopupOpened += OnPopupWindowOpen;
                popupWindow.OnPopupClosed += OnPopupWindowClose;
                if (!string.IsNullOrEmpty(popupWindow.layerName))
                {
                    if (!layerDict.TryGetValue(popupWindow.layerName, out var list))
                    {
                        list = new List<PopupWindow>();
                        layerDict.Add(popupWindow.layerName, list);
                    }
                    list.Add(popupWindow);
                }
            }
        }
        public void RemovePopupWindow(PopupWindow popupWindow)
        {
            if (popupWindow == null)
            {
                return;
            }
            if (popupWindowList.Contains(popupWindow))
            {
                popupWindowList.Remove(popupWindow);
                popupWindow.OnPopupOpened -= OnPopupWindowOpen;
                popupWindow.OnPopupClosed -= OnPopupWindowClose;
                if (!string.IsNullOrEmpty(popupWindow.layerName) && layerDict.TryGetValue(popupWindow.layerName, out var list))
                {
                    list.Remove(popupWindow);
                    if (list.Count == 0)
                    {
                        layerDict.Remove(popupWindow.layerName);
                    }
                }
            }
        }
        public void OnPopupWindowOpen(PopupWindow popupWindow)
        {
            SetWindowMaskActive(true);
            SetWindowFront(popupWindow);
        }
        public void OnPopupWindowClose(PopupWindow popupWindow)
        {
            if (popupWindow == null)
            {
                return;
            }
            PopupWindow topPopupWindow = GetTopPopupWindow(popupWindow);
            if (topPopupWindow == null)
            {
                SetWindowMaskActive(false);
            }
            else
            {
                SetWindowMaskActive(true);
                SetWindowFront(topPopupWindow);
            }
        }
        public void SetWindowFront(PopupWindow popupWindow)
        {
            if (popupWindow == null)
            {
                return;
            }
            if (string.IsNullOrEmpty(popupWindow.layerName))
            {
                popupWindow.transform.SetAsLastSibling();
                int maskIndex = popupWindow.transform.GetSiblingIndex() - 1;
                if (maskIndex < 0)
                {
                    maskIndex = 0;
                }
                windowMask.transform.SetSiblingIndex(maskIndex);
            }
            else
            {
                SetWindowFront(popupWindow.layerName);
                popupWindow.transform.SetAsLastSibling();
            }
        }//SetWindowFront
        public void SetWindowFront(string layerName)
        {
            if (string.IsNullOrEmpty(layerName))
            {
                return;
            }

            if (!layerDict.TryGetValue(layerName, out var sameLayerWindows) || sameLayerWindows.Count == 0)
            {
                return;
            }

            List<PopupWindow> orderedWindows = new List<PopupWindow>();
            foreach (var sameLayerWindow in sameLayerWindows)
            {
                if (sameLayerWindow == null || sameLayerWindow.transform.parent != transform)
                {
                    continue;
                }
                orderedWindows.Add(sameLayerWindow);
            }

            if (orderedWindows.Count == 0)
            {
                return;
            }

            orderedWindows.Sort((left, right) => left.transform.GetSiblingIndex().CompareTo(right.transform.GetSiblingIndex()));
            foreach (var orderedWindow in orderedWindows)
            {
                orderedWindow.transform.SetAsLastSibling();
            }

            int layerMaskIndex = orderedWindows[0].transform.GetSiblingIndex() - 1;
            if (layerMaskIndex < 0)
            {
                layerMaskIndex = 0;
            }
            windowMask.transform.SetSiblingIndex(layerMaskIndex);
        }//SetWindowFront
        public PopupWindow GetTopPopupWindow()
        {
            PopupWindow topPopupWindow = null;
            foreach (var popupWindow in popupWindowList)
            {
                if (popupWindow == null || !popupWindow.IsOpen)
                {
                    continue;
                }

                if (topPopupWindow == null || popupWindow.transform.GetSiblingIndex() > topPopupWindow.transform.GetSiblingIndex())
                {
                    topPopupWindow = popupWindow;
                }
            }

            return topPopupWindow;
        }
        public PopupWindow GetTopPopupWindow(PopupWindow popupWindow)
        {
            PopupWindow topPopupWindow = null;
            foreach (var childPopupWindow in popupWindowList)
            {
                if (childPopupWindow == null || childPopupWindow == popupWindow || !childPopupWindow.IsOpen)
                {
                    continue;
                }

                if (topPopupWindow == null || childPopupWindow.transform.GetSiblingIndex() > topPopupWindow.transform.GetSiblingIndex())
                {
                    topPopupWindow = childPopupWindow;
                }
            }

            return topPopupWindow;
        }
        public void SetWindowMaskActive(bool active)
        {
            if (windowMask != null)
            {
                windowMask.gameObject.SetActive(active);
            }
        }
    }
}