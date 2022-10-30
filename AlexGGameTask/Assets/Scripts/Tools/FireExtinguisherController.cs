using System.Collections;
using TMPro;
using UnityEngine;

namespace Tools
{
    public class FireExtinguisherController : MonoBehaviour
    {
        [SerializeField] private float minHeight = 1;
        [SerializeField] private float maxHeight = 4;
        [Range(0, 1)]
        [SerializeField] private float powderLevel = 1;
        [SerializeField] private TextMeshProUGUI powderLevelLabel;
        [Tooltip("On how long should last when filled to max value? [seconds]")]
        [SerializeField] private float durationWhenMax = 10;
        [SerializeField] private ParticleSystem particles;

        private float _remainingTimeOfUse;
        private bool _isWorking;
        private Coroutine _stifleCoroutine;

        private bool ShouldWorking => _remainingTimeOfUse > 0;

        private void Start()
        {
            _remainingTimeOfUse = powderLevel * durationWhenMax;
            DisplayPowderLevel();
        }

        public void SetHeightLevel(float value)
        {
            Vector3 toolPosition = transform.position;
            toolPosition.y = Mathf.Lerp(minHeight, maxHeight, value);
            transform.position = toolPosition;
        }

        public void StartStifle()
        {
            if (!ShouldWorking) return;
            particles.Play();

            if (_stifleCoroutine != null)
            {
                StopCoroutine(_stifleCoroutine);
                particles.Stop();
            }
            _stifleCoroutine = StartCoroutine(StifleCoroutine());
        }
        
        public void StopStifle()
        {
            if (!ShouldWorking) return;
            particles.Stop();
        }

        private IEnumerator StifleCoroutine()
        {
            while (ShouldWorking)
            {
                _remainingTimeOfUse = Mathf.Max(0, _remainingTimeOfUse - Time.deltaTime);
                powderLevel = _remainingTimeOfUse / durationWhenMax;
                DisplayPowderLevel();
                yield return null;
            }
            particles.Stop();
        }

        private void DisplayPowderLevel()
        {
            powderLevelLabel.text = Mathf.Round(powderLevel * 100).ToString() + "%";
        }
    }
}
