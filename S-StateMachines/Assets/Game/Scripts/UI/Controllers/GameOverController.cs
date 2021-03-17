using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SLibrary.StateExample
{
    public class GameOverController : Controller<GameOverView, GameOverModel>
    {
        
        public void PlayAgain()
        {
            GameManager.instance.gameStateController.SetState(GameStateMachineStates.InGame);
        }

        public void MainMenu()
        {
            GameManager.instance.gameStateController.SetState(GameStateMachineStates.MainMenu);
        }

    }

    [System.Serializable]
    public class GameOverView : View
    {

    }

    [System.Serializable]
    public class GameOverModel : Model
    {

    }
}