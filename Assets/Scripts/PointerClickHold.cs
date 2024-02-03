using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

namespace SpaceShooter
{
    public class PointerClickHold : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
    {
        private bool isHold;
        public bool IsHold => isHold;

        public void OnPointerDown(PointerEventData eventData)
        {
            isHold = true;
        }

        public void OnPointerUp(PointerEventData eventData)
        {
            isHold = false;
        }
    }
}