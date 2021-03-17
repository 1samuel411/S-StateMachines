using UnityEngine;

namespace SLibrary.StateExample
{
    /// <summary>
    /// Automatically performs a jump if entered. Only able to enter if you're not grounded, unless you're falling
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

            controller.SetControl(controller.jumpControl);
            controller.Jump();
        }

        public override void OnExitState(CharacterStateMachineStates nextState)
        {
            base.OnExitState(nextState);
        }

        public override void Update()
        {
            base.Update();

            controller.SetInputVector();

            if (controller.IsGrounded() && Time.time >= enterTime + timeToExit)
            {
                controller.SetState(controller.lastState);
            }
        }

    }
}