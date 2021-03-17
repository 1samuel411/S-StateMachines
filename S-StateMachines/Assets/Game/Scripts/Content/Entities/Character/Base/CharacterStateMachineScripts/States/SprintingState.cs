using UnityEngine;

namespace SLibrary.StateExample
{
    /// <summary>
    /// The template for a state object
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