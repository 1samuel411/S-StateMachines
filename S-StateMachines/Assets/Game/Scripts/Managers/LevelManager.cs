using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace SLibrary.StateExample
{
    public class LevelManager : MonoBehaviour
    {

        public static LevelManager instance;

        private void Awake()
        {
            instance = this;    
        }

        public void LoadLevel(int buildIndex)
        {
            SceneManager.LoadSceneAsync(buildIndex).completed += (x) =>
            {
                if (IsGameScene(buildIndex))
                {
                    GameManager.instance.InitializePlayer();

                }
            };
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