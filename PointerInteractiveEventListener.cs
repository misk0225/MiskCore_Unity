using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using MiskCore;


namespace MiskCore
{
    public class PointerInteractiveEventListener : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerDownHandler, IPointerUpHandler
    {
        public SequentialEventListeners OnPointerEnterEvent;
        public SequentialEventListeners OnPointerExitEvent;
        public SequentialEventListeners OnPointerDownEvent;
        public SequentialEventListeners OnPointerUpEvent;
        public SequentialEventListeners OnPointerUpInBoundingEvent;
        public SequentialEventListeners OnPointerUpNotInBoundingEvent;

        private bool _HasPointer = false;

        public void OnPointerEnter(PointerEventData eventData)
        {
            OnPointerEnterEvent.Invoke();
            _HasPointer = true;
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            OnPointerExitEvent.Invoke();
            _HasPointer = false;
        }
        public void OnPointerDown(PointerEventData eventData)
        {
            OnPointerDownEvent.Invoke();
        }

        public void OnPointerUp(PointerEventData eventData)
        {
            OnPointerUpEvent.Invoke();
            if (_HasPointer)
            {
                OnPointerUpInBoundingEvent.Invoke();
            }
            else
            {
                OnPointerUpNotInBoundingEvent.Invoke();
            }
        }
    }
}

