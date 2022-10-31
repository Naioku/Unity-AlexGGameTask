using UnityEngine;

namespace Environment
{
    public class Burnable : MonoBehaviour
    {
        [SerializeField] private bool isBurning = true;
        [SerializeField] private ParticleSystem particles;
        [SerializeField] private AnimationCurve stiflingNormalizedEfficiency;

        public float _burningPercent = 1;
        
        private void Start()
        {
            if (isBurning)
            {
                particles.Play();
            }
        }

        public void Stifle(float extinguisherHeightNormalized)
        {
            float valueToSubtract = stiflingNormalizedEfficiency.Evaluate(extinguisherHeightNormalized);
            _burningPercent = Mathf.Max(0, _burningPercent - valueToSubtract * Time.deltaTime);
        }
    }
}
