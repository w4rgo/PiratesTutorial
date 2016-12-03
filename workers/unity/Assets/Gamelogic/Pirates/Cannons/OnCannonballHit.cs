using Improbable.Ship;
using Improbable.Unity.Visualizer;
using UnityEngine;

namespace Assets.Gamelogic.Pirates.Cannons
{
    public class OnCannonballHit : MonoBehaviour
    {
        [Require]
        private HitByCannonballWriter HitByCannonball;

        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.tag == "Cannonball" && HitByCannonball != null)
            {
                var firerEntityId = other.GetComponent<DestroyCannonball>().firerEntityId;
                HitByCannonball.Update.TriggerHit(firerEntityId).FinishAndSend();
            }
        }
    }
}