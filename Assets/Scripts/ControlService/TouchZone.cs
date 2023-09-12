using System;
using UnityEngine;
using UnityEngine.EventSystems;

namespace ControlSystem
{
    public class TouchZone : MonoBehaviour, IPointerDownHandler, IPointerUpHandler, IDragHandler, IPointerClickHandler
    {
        public event Action<PointerEventData> TouchStart;
        public event Action<PointerEventData> TouchEnd;
        public event Action<PointerEventData> TouchMoved;
        public event Action<PointerEventData> OnClick;

        public void OnPointerDown(PointerEventData eventData)
        {
            TouchStart?.Invoke(eventData);
        }

        public void OnPointerUp(PointerEventData eventData)
        {
            TouchEnd?.Invoke(eventData);
        }

        public void OnDrag(PointerEventData eventData)
        {
            TouchMoved?.Invoke(eventData);
        }

        public void OnPointerClick(PointerEventData eventData)
        {
            OnClick?.Invoke(eventData);
        }
    }
}