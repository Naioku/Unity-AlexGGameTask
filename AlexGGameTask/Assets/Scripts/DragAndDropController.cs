using Core;
using UnityEngine;

public class DragAndDropController : MonoBehaviour
{
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

    private void HandleClick(bool isPressed)
    {
        print(isPressed ? "Pressed." : "Released.");
    }
}
