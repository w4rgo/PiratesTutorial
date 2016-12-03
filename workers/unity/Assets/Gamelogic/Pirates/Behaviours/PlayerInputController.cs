using Improbable.Ship;
using Improbable.Unity.Visualizer;
using UnityEngine;

namespace Assets.Gamelogic.Pirates.Behaviours
{
    public class PlayerInputController : MonoBehaviour {
    
        [Require] private PlayerControlsWriter PlayerControls;
    
        void Update ()
        {
            PlayerControls.Update
                .TargetSpeed(Mathf.Clamp01(Input.GetAxis("Vertical")))
                .TargetSteering(Input.GetAxis("Horizontal"))
                .FinishAndSend();

            if (Input.GetKeyDown(KeyCode.E))
            {
                PlayerControls.Update.TriggerFireRight().FinishAndSend();
            }

            if (Input.GetKeyDown(KeyCode.Q))
            {
                PlayerControls.Update.TriggerFireLeft().FinishAndSend();
            }
        }
    }
}
