using Improbable.Ship;
using Improbable.Unity;
using Improbable.Unity.Visualizer;
using UnityEngine;

namespace Assets.Gamelogic.Pirates.Behaviours
{
    // Add this MonoBehaviour on client workers only
    [WorkerType(WorkerPlatform.UnityClient)]
    public class SinkingBehaviour : MonoBehaviour
    {
        [Require] private Health.Reader HealthReader;
        public Animation SinkingAnimation;
        private bool alreadySunk = false;

        private void OnEnable()
        {
            alreadySunk = false; // This line can be excluded in case prefab pooling is not used.
            InitializeSinkingAnimation();
            // Register callback for when components change
            HealthReader.CurrentHealthUpdated.Add(OnCurrentHealthUpdated);
        }

        void OnDisable()
        {
            // Deregister callback for when components change
            HealthReader.CurrentHealthUpdated.Remove(OnCurrentHealthUpdated);
        }

        private void OnCurrentHealthUpdated(int currentHealth)
        {
            if (!alreadySunk && currentHealth <= 0)
            {
                SinkingAnimation.Play();
                alreadySunk = true;
            }
        }

        private void InitializeSinkingAnimation()
        {
            /*
             * SinkingAnimation is triggered when the ship is first killed. But a worker which checks out
             * the entity after this time (for example, a client connecting to the game later)
             * must not visualize the ship as still alive.
             *
             * Therefore, on checkout, any sunk ships jump to the end of the sinking animation.
             */
            if (HealthReader.Data.currentHealth <= 0)
            {
                foreach (AnimationState state in SinkingAnimation)
                {
                    state.normalizedTime = 1;
                }
                SinkingAnimation.Play();
                alreadySunk = true;
            }
        }
    }
}