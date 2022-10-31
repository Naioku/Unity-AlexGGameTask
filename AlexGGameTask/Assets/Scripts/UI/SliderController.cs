using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace UI
{
    public class SliderController : MonoBehaviour
    {
        [SerializeField] private UnityEvent<float> onStart;
        
        void Start()
        {
            onStart.Invoke(GetComponent<Slider>().value);
        }
    }
}
