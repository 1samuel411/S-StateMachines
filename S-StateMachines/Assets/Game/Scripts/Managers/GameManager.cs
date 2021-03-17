using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace SLibrary.StateExample
{
    public class GameManager : MonoBehaviour
    {

        public static GameManager instance;

        public GameStateMachineController gameStateController;

        private void Awake()
        {
            instance = this;
            inGameData = new InGameRunTimeData();
        }

        private void Update()
        {
            if(gameStateController.currentState == GameStateMachineStates.InGame)
            {
                // Check if the user fell out of bounds
                if (IsOutOfBounds() && !userLost)
                {
                    userLost = true;
                    inGameData.lives.SetValue(inGameData.lives.GetValue() - 1);


                    if (inGameData.lives.GetValue() <= 0)
                    {
                        gameStateController.SetState(GameStateMachineStates.GameOver);
                    }
                    else
                    {
                        LevelManager.instance.LoadLevel(SceneManager.GetActiveScene().buildIndex);
                        GameManager.instance.GetGameData().score.SetValue(GameManager.instance.GetGameData().score.GetValue() - 25);
                    }
                }
            }
        }

        private bool IsOutOfBounds()
        {
            return spawnedPlayer != null && spawnedPlayer.transform.position.y < -8;
        }

        private InGameRunTimeData inGameData = new InGameRunTimeData();
        private CharacterStateMachineController spawnedPlayer;
        private bool userLost = false;

        public void StartNewGame()
        {
            inGameData.lives.SetValue(GameProperties.instance.startingLives);
            inGameData.score.SetValue(0);
        }

        public void InitializePlayer()
        {
            userLost = false;
            // Spawn the player
            spawnedPlayer = Instantiate(GameProperties.instance.playerPrefab).GetComponent<CharacterStateMachineController>();

            spawnedPlayer.Teleport(SceneReferences.instance.spawnpointTransform.position);
        }
        
        public CharacterStateMachineController GetSpawnedPlayer()
        {
            return spawnedPlayer;
        }

        public InGameRunTimeData GetGameData()
        {
            return inGameData;
        }
    }
}