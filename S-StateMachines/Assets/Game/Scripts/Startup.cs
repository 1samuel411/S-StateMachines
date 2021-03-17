using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace SLibrary.StateExample
{
    /// <summary>
    /// Starts the managers and UI.
    /// Detects what level is currently loaded and initializes the game state manager accordingly
    /// </summary>
    public class Startup
    {

        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.AfterSceneLoad)]
        private static void OnRuntimeMethodLoad()
        {
            GameObject managers = GameObject.Instantiate(GameProperties.instance.managersPrefab);
            GameObject.DontDestroyOnLoad(managers);

            // Check if loaded in the main menu scene
            if (SceneManager.GetActiveScene().buildIndex == GameProperties.instance.mainMenuScene)
            {
                GameStateMachineController.instance.SetState(GameStateMachineStates.MainMenu);
                return;
            }

            // Check if loaded in a game level scene
            for (int i = 0; i < GameProperties.instance.gameScenes.Length; i++)
            {
                if (GameProperties.instance.gameScenes[i] == SceneManager.GetActiveScene().buildIndex)
                {
                    GameStateMachineController.instance.SetState(GameStateMachineStates.InGame);
                    break;
                }
            }
        }
    }
}