using Improbable;
using Improbable.Ship;
using Improbable.Unity;
using Improbable.Unity.Core;
using Improbable.Unity.Visualizer;
using UnityEngine;

namespace Assets.Gamelogic.Pirates.Cannons
{
    // Add this MonoBehaviour on UnityWorker (server-side) workers only
    [WorkerType(WorkerPlatform.UnityWorker)]
    public class OnCannonballHit : MonoBehaviour
    {
        // Enable this MonoBehaviour only on the worker with write access for the entity's Health component
        [Require] private Health.Writer HealthWriter;

        private void OnTriggerEnter(Collider other)
        {
            /*
             * Unity's OnTriggerEnter runs even if the MonoBehaviour is disabled, so non-authoritative UnityWorkers
             * must be protected against null writers
             */
            if (HealthWriter == null)
                return;

            // Ignore collision if this ship is already dead
            if (HealthWriter.Data.currentHealth <= 0)
                return;

            if (other != null && other.gameObject.tag == "Cannonball")
            {
                // Reduce health of this entity when hit
                int newHealth = HealthWriter.Data.currentHealth - 200;
                HealthWriter.Send(new Health.Update().SetCurrentHealth(newHealth));

                // Notify firer to increment score if this entity was killed
                if (newHealth <= 0)
                {
                    AwardPointsForKill(new EntityId(other.GetComponent<DestroyCannonball>().firerEntityId.Value.Id));
                }
            }
        }

        private void AwardPointsForKill(EntityId firerEntityId)
        {
            uint pointsToAward = 1;
            // Use Commands API to issue an AwardPoints request to the entity who fired the cannonball
            SpatialOS.Commands.SendCommand(HealthWriter, Score.Commands.AwardPoints.Descriptor, new AwardPoints(pointsToAward), firerEntityId)
                .OnSuccess(OnAwardPointsSuccess)
                .OnFailure(OnAwardPointsFailure);
        }

        private void OnAwardPointsSuccess(AwardResponse response)
        {
            Debug.Log("AwardPoints command succeeded. Points awareded: " + response.amount);
        }

        private void OnAwardPointsFailure(ICommandErrorDetails response)
        {
            Debug.LogError("Failed to send AwardPoints command with error: " + response.ErrorMessage);
        }

    }
}