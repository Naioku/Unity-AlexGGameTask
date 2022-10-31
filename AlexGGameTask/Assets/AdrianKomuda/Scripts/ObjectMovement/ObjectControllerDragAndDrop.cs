using AdrianKomuda.Scripts.Core;
using UnityEngine;

namespace AdrianKomuda.Scripts.ObjectMovement
{
    public class ObjectControllerDragAndDrop : MonoBehaviour
    {
        [SerializeField] private float maxRaycastDistance = 40;
        [SerializeField] private LayerMask draggableLayers;
        [SerializeField] private LayerMask groundLayers;
        [SerializeField] private float pickingUpHeight = 0.25f;
    
        private Camera _mainCamera;
        private InputReader _inputReader;
        private GameObject _selectedObject;

        private void Awake()
        {
            _mainCamera = Camera.main;
            _inputReader = GetComponent<InputReader>();
        }

        private void OnEnable()
        {
            _inputReader.SelectionActivatedEvent += HandleClick;
        }
    
        private void OnDisable()
        {
            _inputReader.SelectionActivatedEvent -= HandleClick;
        }

        private void Update()
        {
            if (_selectedObject)
            {
                ManageDragging();
            }
        }

        private void ManageDragging()
        {
            if (!GetGroundHitPosition(out Vector3 destination)) return;
            destination.y = _selectedObject.transform.position.y;
            _selectedObject.transform.position = destination;
        }
    
        private bool GetGroundHitPosition(out Vector3 result)
        {
            result = Vector3.zero;
            bool hasHit = GetRaycastHit(out RaycastHit hit, groundLayers);
            if (!hasHit) return false;
        
            result = hit.point;
            return true;
        }

        private void HandleClick(bool isPressed)
        {
            if (isPressed)
            {
                PickUp();
            }
            else
            {
                PutDown();
            }
        }

        private void PickUp()
        {
            _selectedObject = GetClickedObject();
            if (!_selectedObject) return;
        
            ToggleDragAndDrop(pickingUpHeight, false);
        }

        private void PutDown()
        {
            if (!_selectedObject) return;
        
            ToggleDragAndDrop(-pickingUpHeight, true);
            _selectedObject = null;
        }
    
        private GameObject GetClickedObject()
        {
            bool hasHit = GetRaycastHit(out RaycastHit hit, draggableLayers);
            return hasHit ? hit.collider.gameObject : null;
        }

        private bool GetRaycastHit(out RaycastHit hit, LayerMask searchingLayers)
        {
            return Physics.Raycast(
                GetMouseRay(_inputReader.MouseScreenPosition),
                out hit,
                maxRaycastDistance,
                searchingLayers);
        }

        private Ray GetMouseRay(Vector2 mousePosition)
        {
            return _mainCamera.ScreenPointToRay(mousePosition);
        }

        private void ToggleDragAndDrop(float pickingUpHeight, bool shouldCursorBeVisible)
        {
            Cursor.visible = shouldCursorBeVisible;
            Vector3 pickingUpPosition = _selectedObject.transform.position;
            pickingUpPosition.y += pickingUpHeight;
            _selectedObject.transform.position = pickingUpPosition;
        }
    }
}
