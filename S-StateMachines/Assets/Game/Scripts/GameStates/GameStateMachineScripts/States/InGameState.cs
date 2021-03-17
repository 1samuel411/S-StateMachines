using UnityEngine;
using UnityEngine.SceneManagement;

namespace SLibrary.StateExample
{
    /// <summary>
    /// The template for a state object
    /// </summary>
    public class InGameState : BaseGameStateMachineState
    {
        public override bool CanEnter(GameStateMachineStates lastState)
        {
            return base.CanEnter(lastState);
        }

        public override void OnEnterState(GameStateMachineStates lastState)
        {
            base.OnEnterState(lastState);

            // Ignore entering this state if we're paused
            if (lastState == GameStateMachineStates.Paused)
                return;

            UIManager.instance.inGameController.gameObject.SetActive(true);

            // Load the game level
            if (GameManager.instance.gameStateController.lastState != GameStateMachineStates.None)
                LevelManager.instance.LoadLevel(GameProperties.instance.defaultLevel);
            else
                LevelManager.instance.LoadLevel(SceneManager.GetActiveScene().buildIndex);

            GameManager.instance.StartNewGame();
        }

        public override void OnExitState(GameStateMachineStates nextState)
        {
            if(nextState == GameStateMachineStates.MainMenu)
            {
                UIManager.instance.inGameController.gameObject.SetActive(false);
            }
            base.OnExitState(nextState);
        }

        public override void Update()
        {
            base.Update();

            if (Input.GetKeyDown(KeyCode.Escape))
            {
                GameManager.instance.gameStateController.SetState(GameStateMachineStates.Paused);
            }
        }

    }
}