using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace SLibrary.StateExample
{
    public class Startup
    {

        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.SubsystemRegistration)]
        private static void OnRuntimeMethodLoad()
        {
            GameObject gameManager = GameObject.Instantiate(GameProperties.instance.gameManagerPrefab);
            GameObject.DontDestroyOnLoad(gameManager);
            GameObject uiManager = GameObject.Instantiate(GameProperties.instance.uiManagerPrefab);
            GameObject.DontDestroyOnLoad(uiManager);

            // Check if loaded in the main menu scene
            if (SceneManager.GetActiveScene().buildIndex == GameProperties.instance.mainMenuScene)
            {
                GameManager.instance.gameStateController.SetState(GameStateMachineStates.MainMenu);
                return;
            }

            // Check if loaded in a game level scene
            for (int i = 0; i < GameProperties.instance.gameScenes.Length; i++)
            {
                if (GameProperties.instance.gameScenes[i] == SceneManager.GetActiveScene().buildIndex)
                {
                    GameManager.instance.gameStateController.SetState(GameStateMachineStates.InGame);
                    break;
                }
            }
        }
    }
}