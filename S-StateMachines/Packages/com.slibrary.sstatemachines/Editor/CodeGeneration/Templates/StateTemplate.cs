using SLibrary.StateMachines;

namespace SNAMESPACE_ENTRY
{
    /// <summary>
    /// The template for a state object
    /// </summary>

    [System.Serializable]
    public struct SSTATE_INSTANCE_ENTRY : IState<SSTATES_ENUM_ENTRY>
    {
        /// <summary>
        /// The generated Controller script for this state machine
        /// </summary>
        private SSTATECONTROLLER_ENTRY Controller;

        IStateMachine<SSTATES_ENUM_ENTRY> IState<SSTATES_ENUM_ENTRY>.Controller { get => Controller; set => Controller = value as SSTATECONTROLLER_ENTRY; }

        public bool CanEnter(SSTATES_ENUM_ENTRY lastState) 
        { 
            return true;
        }

        public void OnEnterState(SSTATES_ENUM_ENTRY lastState) { }
        public void OnExitState(SSTATES_ENUM_ENTRY nextState) { }
        public void Update() { }
        public void LateUpdate() { }
        public void FixedUpdate() { }
        public void OnDrawGizmos() { }
        public void OnRenderObject() { }
    }
}