using UnityEngine;

namespace Tools
{
    public class FireExtinguisherController : MonoBehaviour
    {
        [SerializeField] private float minHeight = 1;
        [SerializeField] private float maxHeight = 4;

        [Range(0, 1)]
        [SerializeField] private float heightLevel = 0.5f;

        private void OnValidate()
        {
            Vector3 toolPosition = transform.position;
            toolPosition.y = Mathf.Lerp(minHeight, maxHeight, heightLevel);
            transform.position = toolPosition;
        }
    }
}
