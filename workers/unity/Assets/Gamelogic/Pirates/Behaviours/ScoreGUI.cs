using Improbable.Ship;
using Improbable.Unity;
using Improbable.Unity.Visualizer;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Gamelogic.Pirates.Behaviours
{
	// Enable this MonoBehaviour on client workers only
	[WorkerType(WorkerPlatform.UnityClient)]
	public class ScoreGUI : MonoBehaviour
	{
		/*
         * Client will only have write access for their own designated PlayerShip entity's ShipControls component,
         * so this MonoBehaviour will be enabled on the client's designated PlayerShip GameObject only and not on
         * the GameObject of other players' ships.
         */
		[Require] private ShipControls.Writer ShipControlsWriter;
		[Require] private Score.Reader ScoreReader;

		private Canvas scoreCanvasUI;
		private Text totalPointsGUI;

		private void Awake()
		{
			scoreCanvasUI = GameObject.Find("Canvas").GetComponent<Canvas>();
			totalPointsGUI = scoreCanvasUI.GetComponentInChildren<Text>();
			scoreCanvasUI.enabled = false;
			updateGUI(0);
		}

		private void OnEnable()
		{
			// Register callback for when components change
			ScoreReader.NumberOfPointsUpdated.Add(OnNumberOfPointsUpdated);
		}

		private void OnDisable()
		{
			// Deregister callback for when components change
			ScoreReader.NumberOfPointsUpdated.Remove(OnNumberOfPointsUpdated);
		}

		// Callback for whenever one or more property of the Score component is updated
		private void OnNumberOfPointsUpdated(int numberOfPoints)
		{
			updateGUI(numberOfPoints);
		}

		void updateGUI(int score)
		{
			if (score > 0)
			{
				scoreCanvasUI.enabled = true;
				totalPointsGUI.text = score.ToString();
			}
			else
			{
				scoreCanvasUI.enabled = false;
			}
		}
	}
}