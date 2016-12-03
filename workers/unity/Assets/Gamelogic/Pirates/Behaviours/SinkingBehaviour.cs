using Improbable.Ship;
using Improbable.Unity.Visualizer;
using UnityEngine;

namespace Assets.Gamelogic.Pirates.Behaviours
{
    public class SinkingBehaviour : MonoBehaviour
    {

        [Require]
        protected HealthReader Health;

        public Animation SinkingAnimation;

        void OnEnable()
        {
            Health.CurrentUpdated += OnCurrentHealthUpdated;
        }

        private void OnCurrentHealthUpdated(int currentHealth)
        {
            if (currentHealth <= 0)
            {
                SinkingAnimation.Play();
            }
        }

        void OnDisable()
        {
            SinkingAnimation.Rewind();
            Health.CurrentUpdated -= OnCurrentHealthUpdated;
        }
    }
}