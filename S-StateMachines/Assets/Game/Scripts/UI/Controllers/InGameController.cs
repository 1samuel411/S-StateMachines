using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace SLibrary.StateExample
{
    /// <summary>
    /// Displays the user's lives and scores. Using a highly optimized method of binding data and listening for changes.
    /// </summary>
    public class InGameController : Controller<InGameView, InGameModel>
    {
        private void OnEnable()
        {
            GameStateMachineController.instance.GetGameData().lives.OnUpdate += RefreshLives;
            GameStateMachineController.instance.GetGameData().score.OnUpdate += RefreshScore;
        }

        private void OnDisable()
        {
            GameStateMachineController.instance.GetGameData().lives.OnUpdate -= RefreshLives;
            GameStateMachineController.instance.GetGameData().score.OnUpdate -= RefreshScore;
        }

        void RefreshLives(int val)
        {
            view.livesText.text = val.ToString();
        }

        void RefreshScore(int val)
        {
            view.scoreText.text = val.ToString();
        }
    }

    [System.Serializable]
    public class InGameView : View
    {
        public Text scoreText;
        public Text livesText;
    }

    [System.Serializable]
    public class InGameModel : Model
    {

    }
}