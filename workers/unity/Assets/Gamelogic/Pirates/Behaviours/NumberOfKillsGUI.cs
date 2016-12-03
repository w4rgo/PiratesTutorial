using Improbable.Ship;
using Improbable.Unity;
using Improbable.Unity.Visualizer;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Gamelogic.Pirates.Behaviours
{

    [EngineType(EnginePlatform.Client)]
    public class NumberOfKillsGUI : MonoBehaviour
    {
        [Require] private ScoreReader Score;
        [Require] private PlayerControlsWriter LocalPlayerCheck;

        private Text numberOfKillsGUI;

        private void Awake()
        {
            numberOfKillsGUI = GameObject.Find("Canvas").GetComponentInChildren<Text>();
            GameObject.Find("Background").GetComponent<Image>().color = Color.clear;
        }

        private void OnEnable()
        {
            Score.NumberOfKillsUpdated += updateGUI;
        }

        void updateGUI(int newNumberOfKills)
        {
            if (newNumberOfKills > 0)
            {
                GameObject.Find("Background").GetComponent<Image>().color = Color.white;
                var text = "Score/number of kills: " + newNumberOfKills.ToString() + " ";
                numberOfKillsGUI.text = text;
            }
            else
            {
                GameObject.Find("Background").GetComponent<Image>().color = Color.clear;
            }
        }
    }
}