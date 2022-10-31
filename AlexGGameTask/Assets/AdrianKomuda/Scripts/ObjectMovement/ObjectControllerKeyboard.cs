using AdrianKomuda.Scripts.Core;
using UnityEngine;

namespace AdrianKomuda.Scripts.ObjectMovement
{
    public class ObjectControllerKeyboard : MonoBehaviour
    {
        [SerializeField] private float movementSpeed = 5;
        
        private InputReader _inputReader;
        private Collider _collider;

        private void Awake()
        {
            _inputReader = GetComponent<InputReader>();
            _collider = GetComponent<Collider>();
        }

        private void Update()
        {
            Vector2 playerMovementInput = _inputReader.PlayerMovementValue;
            Vector3 displacement = new Vector3(playerMovementInput.x, 0, playerMovementInput.y) * movementSpeed * Time.deltaTime;
            transform.Translate(displacement);
        }
    }
}
