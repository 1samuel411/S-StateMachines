using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace SLibrary.StateExample
{
    public class InGameController : Controller<InGameView, InGameModel>
    {
        private void OnEnable()
        {
            GameManager.instance.GetGameData().lives.OnUpdate += RefreshLives;
            GameManager.instance.GetGameData().score.OnUpdate += RefreshScore;
        }

        private void OnDisable()
        {
            GameManager.instance.GetGameData().lives.OnUpdate -= RefreshLives;
            GameManager.instance.GetGameData().score.OnUpdate -= RefreshScore;
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