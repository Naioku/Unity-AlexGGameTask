using System;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Core
{
    public class InputReader : MonoBehaviour, Controls.IPlayerActions
    {
        public event Action<bool> SelectionActivatedEvent;
        public Vector2 MouseScreenPosition { get; private set; }

        private Controls _controls;

        private void Start()
        {
            _controls = new Controls();
            _controls.Player.SetCallbacks(this);

            _controls.Player.Enable();
        }
        
        public void OnDragDrop(InputAction.CallbackContext context)
        {
            if (context.performed)
            {
                SelectionActivatedEvent?.Invoke(true);
            }
            else if (context.canceled)
            {
                SelectionActivatedEvent?.Invoke(false);
            }
        }

        public void OnMouseMove(InputAction.CallbackContext context)
        {
            MouseScreenPosition = context.ReadValue<Vector2>();
        }
    }
}
