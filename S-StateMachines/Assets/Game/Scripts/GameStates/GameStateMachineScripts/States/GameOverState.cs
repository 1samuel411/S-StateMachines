using UnityEngine;

namespace SLibrary.StateExample
{
    /// <summary>
    /// The template for a state object
    /// </summary>
    public class GameOverState : BaseGameStateMachineState
    {
        public override bool CanEnter(GameStateMachineStates lastState)
        {
            return base.CanEnter(lastState);
        }

        public override void OnEnterState(GameStateMachineStates lastState)
        {
            base.OnEnterState(lastState);

            Time.timeScale = 0;
            UIManager.instance.gameOverController.gameObject.SetActive(true);
            UIManager.instance.inGameController.gameObject.SetActive(false); 
        }

        public override void OnExitState(GameStateMachineStates nextState)
        {
            base.OnExitState(nextState);

            Time.timeScale = 1;
            UIManager.instance.gameOverController.gameObject.SetActive(false);
        }

        public override void Update()
        {
            base.Update();
        }

    }
}