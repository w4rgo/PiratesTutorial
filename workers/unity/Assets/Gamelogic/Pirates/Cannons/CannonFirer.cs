using Improbable.Ship;
using Improbable.Unity.Visualizer;
using UnityEngine;

namespace Assets.Gamelogic.Pirates.Cannons
{
    public class CannonFirer : MonoBehaviour
    {
        [Require] private PlayerControlsReader PlayerControls;

        private Cannon cannon;

        private void Start()
        {
            cannon = gameObject.GetComponent<Cannon>();
        }

        void OnEnable()
        {
            PlayerControls.FireLeft += OnFireLeft;
            PlayerControls.FireRight += OnFireRight;
        }

        private void OnFireLeft(FireLeft fireLeft)
        {
            FireCannons(-transform.right);
        }

        private void OnFireRight(FireRight fireRight)
        {
            FireCannons(transform.right);
        }

        private void FireCannons(Vector3 direction)
        {
            if (cannon != null)
            {
                cannon.Fire(direction);
            }
        }

        void OnDisable()
        {
            PlayerControls.FireLeft -= OnFireLeft;
            PlayerControls.FireRight -= OnFireRight;
        }
    }
}