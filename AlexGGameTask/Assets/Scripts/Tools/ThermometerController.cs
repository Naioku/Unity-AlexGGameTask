using System.Collections;
using Environment;
using TMPro;
using UnityEngine;

namespace Tools
{
    public class ThermometerController : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI label;
        [SerializeField] private float shiftSpeed = 5;

        private float _currentTemperature;
        private Coroutine _runningCoroutine;

        private void OnTriggerEnter(Collider other)
        {
            if (!other.TryGetComponent(out AreaController areaController)) return;

            if (_runningCoroutine != null)
            {
                StopCoroutine(_runningCoroutine);
            }
            _runningCoroutine = StartCoroutine(ShiftTempCoroutine(_currentTemperature, areaController.Temperature));
        }

        private IEnumerator ShiftTempCoroutine(float startValue, float endValue)
        {
            var t = 0.0f;
            while (t <= 1.0f)
            {
                t += shiftSpeed * Time.deltaTime;
                _currentTemperature = Mathf.Lerp(startValue, endValue, t);
                DisplayTemperature(_currentTemperature);
                yield return null;
            }
        }

        private void DisplayTemperature(float value)
        {
            label.text = Mathf.Round(value).ToString();
        }
    }
}
