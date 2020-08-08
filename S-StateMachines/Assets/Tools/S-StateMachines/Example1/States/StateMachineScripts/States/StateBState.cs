namespace Example.States
{
    /// <summary>
    /// The template for a state object
    /// </summary>
    public class StateBState : BaseStateMachineState
    {
        public override bool CanEnter(StateMachineStates lastState)
        {
            return base.CanEnter(lastState);
        }

        public override void OnEnterState(StateMachineStates lastState)
        {
            base.OnEnterState(lastState);
        }

        public override void OnExitState(StateMachineStates nextState)
        {
            base.OnExitState(nextState);
        }

        public override void Update()
        {
            base.Update();
        }

    }
}