using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SLibrary.StateExample
{
    /// <summary>
    /// The Paused controller lets you resume or exit the game.
    /// </summary>
    public class PausedController : Controller<PausedView, PausedModel>
    {
        
        public void Resume()
        {
            GameStateMachineController.instance.SetState(GameStateMachineStates.InGame);
        }

        public void ExitGame()
        {
            GameStateMachineController.instance.SetState(GameStateMachineStates.MainMenu);
        }

    }

    [System.Serializable]
    public class PausedView : View
    {

    }

    [System.Serializable]
    public class PausedModel : Model
    {

    }
}