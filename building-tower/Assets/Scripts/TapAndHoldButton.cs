using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
namespace BuildingTower
{
    public class TapAndHoldButton : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
    {

        public UnityEvent OnTap;
        public UnityEvent OnUnTap;

        public bool isTap = false;

        public void OnPointerDown(PointerEventData eventData)
        {
            if (!isTap)
            {
                isTap = true;
                OnTap.Invoke();
            }
        }

        public void OnPointerUp(PointerEventData eventData)
        {
            if (isTap)
            {
                if (Input.touchCount == 0)
                {
                    isTap = false;
                    OnUnTap.Invoke();
                }
            }
        }
    }
}