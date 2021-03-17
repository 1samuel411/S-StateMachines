using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace SLibrary.StateExample
{
    /// <summary>
    /// Managed all the variables used to drive the game.
    /// </summary>
    public class GameProperties : InstancedScriptableObject<GameProperties>
    {

#if UNITY_EDITOR
        /// <summary>
        /// Every instanced scriptable object needs this to reference and create the scriptable object
        /// </summary>
        [MenuItem("Data/Game Properties")]
        public static void Init()
        {
            CreateMyAsset();
        }
#endif

        [Header("Startup Prefabs")]
        public GameObject managersPrefab;
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