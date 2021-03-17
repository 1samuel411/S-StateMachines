using UnityEngine;

namespace SLibrary.StateExample
{
    /// <summary>
    /// Pauses the game and displays the pause UI.
    /// Checks for esc input to exit state.
    /// </summary>
    public class PausedState : BaseGameStateMachineState
    {
        public override bool CanEnter(GameStateMachineStates lastState)
        {
            return base.CanEnter(lastState);
        }

        public override void OnEnterState(GameStateMachineStates lastState)
        {
            base.OnEnterState(lastState);

            Time.timeScale = 0;
            UIManager.instance.pausedController.gameObject.SetActive(true);
        }

        public override void OnExitState(GameStateMachineStates nextState)
        {
            base.OnExitState(nextState);

            Time.timeScale = 1;
            UIManager.instance.pausedController.gameObject.SetActive(false);

            if(nextState == GameStateMachineStates.MainMenu)
            {
                UIManager.instance.inGameController.gameObject.SetActive(false);
            }
        }

        public override void Update()
        {
            base.Update();

            if (Input.GetKeyDown(KeyCode.Escape))
            {
                GameStateMachineController.instance.SetState(GameStateMachineStates.InGame);
            }
        }

    }
}