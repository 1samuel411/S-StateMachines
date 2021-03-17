using UnityEngine;

namespace SLibrary.StateExample
{
    /// <summary>
    /// Sets the speed to the walking speed. Listens for left shift to sprint, space to jump, and not grounded to fall.
    /// </summary>
    public class WalkingState : BaseCharacterStateMachineState
    {
        public override bool CanEnter(CharacterStateMachineStates lastState)
        {
            return base.CanEnter(lastState);
        }

        public override void OnEnterState(CharacterStateMachineStates lastState)
        {
            base.OnEnterState(lastState);

            controller.SetControl(controller.normalControl);
            controller.SetSpeed(controller.walkSpeed);
        }

        public override void OnExitState(CharacterStateMachineStates nextState)
        {
            base.OnExitState(nextState);
        }

        public override void Update()
        {
            base.Update();

            controller.SetInputVector();

            if(Input.GetKey(KeyCode.LeftShift))
            {
                controller.SetState(CharacterStateMachineStates.Sprinting);
            }

            if (Input.GetKeyDown(KeyCode.Space))
            {
                controller.SetState(CharacterStateMachineStates.Jumping);
            }

            if (controller.IsGrounded() == false)
            {
                controller.SetState(CharacterStateMachineStates.Falling);
            }
        }

    }
}