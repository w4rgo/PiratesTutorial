using Improbable.Ship;
using Improbable.Unity.Visualizer;
using UnityEngine;

namespace Assets.Gamelogic.Pirates.Cannons
{
    // This MonoBehaviour will be enabled on both client and server-side workers
    public class CannonFirer : MonoBehaviour
    {
        [Require] private ShipControls.Reader ShipControlsReader;
        [Require] private Health.Reader HealthReader;

        private Cannon cannon;

        private void OnEnable()
        {
            ShipControlsReader.FireLeftTriggered.Add(OnFireLeft);
            ShipControlsReader.FireRightTriggered.Add(OnFireRight);
        }

        private void OnDisable()
        {
            ShipControlsReader.FireLeftTriggered.Remove(OnFireLeft);
            ShipControlsReader.FireRightTriggered.Remove(OnFireRight);
        }
        private void Start()
        {
            // Cache entity's cannon gameobject
            cannon = gameObject.GetComponent<Cannon>();
        }

        private void FireCannon(Vector3 direction)
        {
            // Prevent firing for ships which are dead
            if (HealthReader.Data.currentHealth <= 0)
            {
                return;
            }

            if (cannon != null)
            {
                cannon.Fire(direction);
            }
        }

        private void OnFireLeft(FireLeft fireLeft)
        {
            // Process FireLeft event
            AttemptToFireCannons(-transform.right);
        }

        private void OnFireRight(FireRight fireRight)
        {
            // Process FireLeft event
            AttemptToFireCannons(transform.right);
        }

        private void AttemptToFireCannons(Vector3 direction)
        {
            // Prevent firing for ships which are dead
            if (HealthReader.Data.currentHealth <= 0)
            {
                return;
            }

            if (cannon != null)
            {
                cannon.Fire(direction);
            }
        }
    }
}