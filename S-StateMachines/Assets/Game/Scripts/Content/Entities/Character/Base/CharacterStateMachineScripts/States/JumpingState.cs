using UnityEngine;

namespace SLibrary.StateExample
{
    /// <summary>
    /// The template for a state object
    /// </summary>
    public class JumpingState : BaseCharacterStateMachineState
    {
        private const float timeToExit = 0.2f;
        private float enterTime;

        public override bool CanEnter(CharacterStateMachineStates lastState)
        {
            if (controller.IsGrounded() == false && lastState != CharacterStateMachineStates.Falling)
                return false;

            return base.CanEnter(lastState);
        }

        public override void OnEnterState(CharacterStateMachineStates lastState)
        {
            base.OnEnterState(lastState);
            
            enterTime = Time.time;

            controller.Jump();
        }

        public override void OnExitState(CharacterStateMachineStates nextState)
        {
            base.OnExitState(nextState);
        }

        public override void Update()
        {
            base.Update();

            if(controller.IsGrounded() && Time.time >= enterTime + timeToExit)
            {
                controller.SetState(controller.lastState);
            }
        }

    }
}