using UnityEngine;

namespace SLibrary.StateExample
{
    /// <summary>
    /// Placed in this state by the SprintingState and WalkingState when you're no longer grounded.
    /// This state will allow you perform 'coyote' jumps, meaning while you're falling you have a grace period to be able to jump again
    /// </summary>
    public class FallingState : BaseCharacterStateMachineState
    {
        private float enterTime;

        private const float returnToWalkingTime = 0.2f;     // Minimum time to wait before returning to walking
        private const float allowJumpingTime = 0.5f;    // How much time to allow the 'coyote' jump for

        public override bool CanEnter(CharacterStateMachineStates lastState)
        {
            return base.CanEnter(lastState);
        }

        public override void OnEnterState(CharacterStateMachineStates lastState)
        {
            base.OnEnterState(lastState);

            enterTime = Time.time;

            controller.SetControl(controller.jumpControl);
        }

        public override void OnExitState(CharacterStateMachineStates nextState)
        {
            base.OnExitState(nextState);
        }

        public override void Update()
        {
            base.Update();

            controller.SetInputVector();

            // Check if we're still able to jump
            if (Time.time <= enterTime + allowJumpingTime)
            {
                if (Input.GetKeyDown(KeyCode.Space))
                    controller.SetState(CharacterStateMachineStates.Jumping);
            }

            // Check if we should return to walking when landing
            if (controller.IsGrounded() && Time.time >= enterTime + returnToWalkingTime)
                controller.SetState(CharacterStateMachineStates.Walking);


        }

    }
}