using System.Collections;
using Environment;
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
        [SerializeField] private Burnable burnable;

        private float _remainingTimeOfUse;
        private bool _isWorking;
        private Coroutine _stifleCoroutine;
        private float _currentHeightLevelNormalized;

        private bool HasPowder => _remainingTimeOfUse > 0;

        private void Start()
        {
            _remainingTimeOfUse = powderLevel * durationWhenMax;
            DisplayPowderLevel();
        }

        public void SetHeightLevel(float value)
        {
            _currentHeightLevelNormalized = value;
            Vector3 toolPosition = transform.position;
            toolPosition.y = Mathf.Lerp(minHeight, maxHeight, value);
            transform.position = toolPosition;
        }

        public void StartStifle()
        {
            if (!HasPowder) return;
            if (_stifleCoroutine != null)
            {
                StopCoroutine(_stifleCoroutine);
                particles.Stop();
            }
            _stifleCoroutine = StartCoroutine(StifleCoroutine());
            particles.Play();
        }
        
        public void StopStifle()
        {
            if (!HasPowder) return;
            StopCoroutine(_stifleCoroutine);
            particles.Stop();
        }

        private IEnumerator StifleCoroutine()
        {
            while (HasPowder)
            {
                _remainingTimeOfUse = Mathf.Max(0, _remainingTimeOfUse - Time.deltaTime);
                powderLevel = _remainingTimeOfUse / durationWhenMax;
                DisplayPowderLevel();
                burnable.Stifle(_currentHeightLevelNormalized);
                yield return null;
            }
            particles.Stop();
        }

        private void DisplayPowderLevel()
        {
            powderLevelLabel.text = Mathf.Round(powderLevel * 100) + "%";
        }
    }
}
