using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SLibrary.StateExample
{
    /// <summary>
    /// Used to allow the user to play again or quit after winning or losing
    /// </summary>
    public class GameOverController : Controller<GameOverView, GameOverModel>
    {
        
        public void PlayAgain()
        {
            GameStateMachineController.instance.SetState(GameStateMachineStates.InGame);
        }

        public void MainMenu()
        {
            GameStateMachineController.instance.SetState(GameStateMachineStates.MainMenu);
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