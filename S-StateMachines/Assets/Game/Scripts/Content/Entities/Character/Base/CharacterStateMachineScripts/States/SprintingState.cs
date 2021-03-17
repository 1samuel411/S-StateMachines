using UnityEngine;

namespace SLibrary.StateExample
{
    /// <summary>
    /// Sets the character speed to be the sprint speed. Return to walking when releasing shift. Follows the rest of the Walking logic
    /// </summary>
    public class SprintingState : BaseCharacterStateMachineState
    {
        public override bool CanEnter(CharacterStateMachineStates lastState)
        {
            return base.CanEnter(lastState);
        }

        public override void OnEnterState(CharacterStateMachineStates lastState)
        {
            base.OnEnterState(lastState);

            controller.SetControl(controller.normalControl);
            controller.SetSpeed(controller.sprintSpeed);
        }

        public override void OnExitState(CharacterStateMachineStates nextState)
        {
            base.OnExitState(nextState);
        }

        public override void Update()
        {
            base.Update();

            controller.SetInputVector();

            if (Input.GetKey(KeyCode.LeftShift) == false)
            {
                controller.SetState(CharacterStateMachineStates.Walking);
            }

            if (controller.IsGrounded() == false)
            {
                controller.SetState(CharacterStateMachineStates.Falling);
            }

            if (Input.GetKeyDown(KeyCode.Space))
            {
                controller.SetState(CharacterStateMachineStates.Jumping);
            }
        }

    }
}