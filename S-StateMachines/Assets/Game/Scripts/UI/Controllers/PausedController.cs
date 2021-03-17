using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SLibrary.StateExample
{
    public class PausedController : Controller<PausedView, PausedModel>
    {
        
        public void Resume()
        {
            GameManager.instance.gameStateController.SetState(GameStateMachineStates.InGame);
        }

        public void ExitGame()
        {
            GameManager.instance.gameStateController.SetState(GameStateMachineStates.MainMenu);
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