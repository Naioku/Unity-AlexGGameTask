using System.Collections;
using UnityEngine;

namespace Environment
{
    public class Burnable : MonoBehaviour
    {
        [SerializeField] private bool isBurning = true;
        [SerializeField] private ParticleSystem particles;
        [SerializeField] private float burningUpSpeed = 5;

        private float _burningPercent = 1;
        private ParticleSystem.EmissionModule _emissionModule;
        private float _baseRateOverTime;
        private Coroutine _burningCoroutine;

        private void Awake()
        {
            _emissionModule = particles.emission;
            _baseRateOverTime = _emissionModule.rateOverTimeMultiplier;
        }

        private void Start()
        {
            if (isBurning)
            {
                StartBurning();
            }
        }

        public void Stifle(float extinguishingPercent)
        {
            _burningPercent = Mathf.Max(0, _burningPercent - extinguishingPercent * Time.deltaTime);
        }

        private void StartBurning()
        {
            if (_burningCoroutine != null)
            {
                StopCoroutine(_burningCoroutine);
            }
            
            _burningCoroutine = StartCoroutine(BurningCoroutine());
            particles.Play();
        }

        private IEnumerator BurningCoroutine()
        {
            while (isBurning)
            {
                if (_burningPercent <= 0)
                {
                    StopBurning();
                    yield break;
                }
                _burningPercent = Mathf.Min(1, _burningPercent + burningUpSpeed * Time.deltaTime);
                _emissionModule.rateOverTime = _baseRateOverTime * _burningPercent;
                yield return null;
            }
        }

        private void StopBurning()
        {
            isBurning = false;
            particles.Stop();
            _emissionModule.rateOverTime = _baseRateOverTime;
        }
    }
}
