using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SLibrary.StateExample
{
    /// <summary>
    /// Allows the user to Play the game from the main menu.
    /// </summary>
    public class MainMenuController : Controller<MainMenuView, MainMenuModel>
    {
        
        public void PlayGame()
        {
            GameStateMachineController.instance.SetState(GameStateMachineStates.InGame);
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