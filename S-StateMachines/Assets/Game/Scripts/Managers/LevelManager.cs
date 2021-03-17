using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace SLibrary.StateExample
{
    /// <summary>
    /// Manages the Levels for us. Automatically initializes player when loading a game scene
    /// </summary>
    public class LevelManager : MonoBehaviour
    {

        public static LevelManager instance;

        private void Awake()
        {
            instance = this;    
        }

        public void LoadLevel(int buildIndex, bool force = false)
        {
            // Check if already loaded in level
            if(SceneManager.GetActiveScene().buildIndex == buildIndex && !force)
            {
                LevelWasLoaded();
                return;
            }

            // Load the new level
            SceneManager.LoadSceneAsync(buildIndex).completed += (x) => LevelWasLoaded();
        }

        private void LevelWasLoaded()
        {
            if (IsGameScene(SceneManager.GetActiveScene().buildIndex))
            {
                GameStateMachineController.instance.InitializePlayer();
            }
        }

        private static bool IsGameScene(int buildIndex)
        {
            for (int i = 0; i < GameProperties.instance.gameScenes.Length; i++)
            {
                if (GameProperties.instance.gameScenes[i] == buildIndex)
                {
                    return true;
                }
            }
            return false;
        }
    }
}