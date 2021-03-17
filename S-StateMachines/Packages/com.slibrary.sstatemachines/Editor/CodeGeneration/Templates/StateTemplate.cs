namespace SNAMESPACE_ENTRY
{
    /// <summary>
    /// The template for a state object
    /// </summary>
    public class SSTATE_INSTANCE_ENTRY : BASE_SSTATE_ENTRY
    {
        public override bool CanEnter(SSTATES_ENUM_ENTRY lastState)
        {
            return base.CanEnter(lastState);
        }

        public override void OnEnterState(SSTATES_ENUM_ENTRY lastState)
        {
            base.OnEnterState(lastState);
        }

        public override void OnExitState(SSTATES_ENUM_ENTRY nextState)
        {
            base.OnExitState(nextState);
        }

        public override void Update()
        {
            base.Update();
        }

    }
}