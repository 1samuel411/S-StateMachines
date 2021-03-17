using UnityEngine;

namespace SLibrary.StateExample
{
    /// <summary>
    /// The template for a state object
    /// </summary>
    public class FallingState : BaseCharacterStateMachineState
    {
        private float enterTime;
        private const float fallRecoveryTime = 0.2f;
        private const float allowJumpingTime = 2;

        public override bool CanEnter(CharacterStateMachineStates lastState)
        {
            return base.CanEnter(lastState);
        }

        public override void OnEnterState(CharacterStateMachineStates lastState)
        {
            base.OnEnterState(lastState);
            enterTime = Time.time;
        }

        public override void OnExitState(CharacterStateMachineStates nextState)
        {
            base.OnExitState(nextState);
        }

        public override void Update()
        {
            base.Update();

            if (Time.time <= enterTime + allowJumpingTime)
            {
                if (Input.GetKeyDown(KeyCode.Space))
                    controller.SetState(CharacterStateMachineStates.Jumping);
            }

            if (controller.IsGrounded() && Time.time >= enterTime + fallRecoveryTime)
                controller.SetState(CharacterStateMachineStates.Walking);


        }

    }
}