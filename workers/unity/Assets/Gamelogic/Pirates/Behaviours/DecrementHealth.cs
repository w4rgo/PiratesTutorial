﻿using Improbable.Ship;
using Improbable.Unity;
using Improbable.Unity.Visualizer;
using UnityEngine;

namespace Assets.Gamelogic.Pirates.Behaviours
{
    // Add this MonoBehaviour on server-side workers only
    [WorkerType(WorkerPlatform.UnityWorker)]
    public class DecrementHealth : MonoBehaviour
    {
        [Require] private Health.Writer HealthWriter;

        void OnEnable()
        {
            InvokeRepeating("DecrementCurrentHealth", 0, 1.0f);
        }

        void OnDisable()
        {
            CancelInvoke("DecrementCurrentHealth");
        }

        void DecrementCurrentHealth()
        {
            if (HealthWriter.Data.currentHealth > 0)
            {
                HealthWriter.Send(new Health.Update().SetCurrentHealth(HealthWriter.Data.currentHealth - 20));
            }
        }
    }
}