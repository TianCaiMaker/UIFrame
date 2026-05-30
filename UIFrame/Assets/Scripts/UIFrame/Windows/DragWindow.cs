using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class DragWindow : MonoBehaviour, IDragHandler, IBeginDragHandler, IEndDragHandler
{
    public bool dragSelf = true;
    public bool bringToFrontOnBeginDrag = true;

    RectTransform targetRectTransform;
    Canvas rootCanvas;

    public void OnBeginDrag(PointerEventData eventData)
    {
        if (dragSelf)
        {
            targetRectTransform = transform as RectTransform;
        }
        else
        {
            targetRectTransform = transform.parent as RectTransform;
        }

        if (bringToFrontOnBeginDrag && targetRectTransform != null)
        {
            targetRectTransform.SetAsLastSibling();
        }

        rootCanvas = GetComponentInParent<Canvas>();
        if (rootCanvas != null)
        {
            rootCanvas = rootCanvas.rootCanvas;
        }
    }

    public void OnDrag(PointerEventData eventData)
    {
        if (targetRectTransform == null)
        {
            return;
        }

        float scaleFactor = 1f;
        if (rootCanvas != null && rootCanvas.scaleFactor > 0f)
        {
            scaleFactor = rootCanvas.scaleFactor;
        }

        targetRectTransform.anchoredPosition += eventData.delta / scaleFactor;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        targetRectTransform = null;
        rootCanvas = null;
    }
}
