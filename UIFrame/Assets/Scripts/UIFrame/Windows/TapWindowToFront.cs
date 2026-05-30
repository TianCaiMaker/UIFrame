using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
namespace UIFrame
{
    public class TapWindowToFront : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
    {
        public void OnPointerDown(PointerEventData eventData)
        {
        }

        public void OnPointerUp(PointerEventData eventData)
        {
            Transform parent = transform.parent;
            if (parent == null)
            {
                return;
            }
            transform.SetAsLastSibling();
        }
    }
}