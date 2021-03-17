using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace SLibrary.StateExample
{
    public class GameProperties : InstancedScriptableObject<GameProperties>
    {

        [MenuItem("Data/Game Properties")]
        public static void Init()
        {
            if(instance == null)
            {
                CreateMyAsset();
            }
            else
            {
                Selection.activeObject = instance;
            }
        }

        [Header("Startup Prefabs")]
        public GameObject gameManagerPrefab;
        public GameObject uiManagerPrefab;
        [Header("In Game Prefabs")]
        public GameObject playerPrefab;

        [Header("Scenes")]
        public int mainMenuScene;
        public int[] gameScenes;

        [Header("In Game Settings")]
        public int startingLives = 3;
        public int defaultLevel = 1;
    }
}