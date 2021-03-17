using UnityEngine;
using UnityEngine.SceneManagement;

namespace SLibrary.StateExample
{
    /// <summary>
    /// The state where the player is able to move around and progress in the game.
    /// Upon entering this state will load either a new game, or the currently opened scene if entering on the default state.
    /// Checks for Esc to pause the game.
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
            if (GameStateMachineController.instance.lastState != GameStateMachineStates.None)
                LevelManager.instance.LoadLevel(GameProperties.instance.defaultLevel, false);
            else
                LevelManager.instance.LoadLevel(SceneManager.GetActiveScene().buildIndex, false);

            GameStateMachineController.instance.StartNewGame();
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
                GameStateMachineController.instance.SetState(GameStateMachineStates.Paused);
            }
        }

    }
}