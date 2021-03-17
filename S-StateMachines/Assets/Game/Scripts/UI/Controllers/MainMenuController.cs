using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SLibrary.StateExample
{
    public class MainMenuController : Controller<MainMenuView, MainMenuModel>
    {
        
        public void PlayGame()
        {
            GameManager.instance.gameStateController.SetState(GameStateMachineStates.InGame);
        }

    }

    [System.Serializable]
    public class MainMenuView : View
    {

    }

    [System.Serializable]
    public class MainMenuModel : Model
    {

    }
}