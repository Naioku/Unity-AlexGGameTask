using Core;
using UnityEngine;

public class DragAndDropController : MonoBehaviour
{
    [SerializeField] private float maxRaycastDistance = 40f;
    [SerializeField] private LayerMask searchingLayers;
    
    private InputReader _inputReader;
    private GameObject _selectedObject;

    private void Awake()
    {
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
        print("Managing dragging...");
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
        print("Picking up...");
    }

    private void PutDown()
    {
        _selectedObject = null;
        print("Putting down...");
    }

    private GameObject GetClickedObject()
    {
        bool hasHit = Physics.Raycast(
            GetMouseRay(_inputReader.MouseScreenPosition),
            out RaycastHit hit,
            maxRaycastDistance,
            searchingLayers);
            
        return hasHit ? hit.collider.gameObject : null;
    }
    
    private static Ray GetMouseRay(Vector2 mousePosition)
    {
        return Camera.main.ScreenPointToRay(mousePosition);
    }
}
