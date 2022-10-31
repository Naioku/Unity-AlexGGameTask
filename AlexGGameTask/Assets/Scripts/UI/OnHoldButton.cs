using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

namespace UI
{
    public class OnHoldButton : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
    {
        [SerializeField] private UnityEvent ButtonDown;
        [SerializeField] private UnityEvent ButtonUp;

        public void OnPointerDown(PointerEventData eventData)
        {
            ButtonDown.Invoke();
        }

        public void OnPointerUp(PointerEventData eventData)
        {
            ButtonUp.Invoke();
        }
    }
}
