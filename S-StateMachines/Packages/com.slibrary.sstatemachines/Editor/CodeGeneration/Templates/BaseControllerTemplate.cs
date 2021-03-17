using SLibrary.StateMachines;

namespace SNAMESPACE_ENTRY
{
    /// <summary>
    /// State Enum Definition from the ScriptableStateController
    /// </summary>
    public enum SSTATES_ENUM_ENTRY { None, /*SSTATES_ENUM_LIST_ENTRY*/ };

    /// <summary>
    /// This is an automatically generated script that is updated via the ScriptableStateController.
    /// </summary>
    public class BASE_SSTATECONTROLLER_ENTRY : BaseController<SSTATES_ENUM_ENTRY>, IStateMachine<SSTATES_ENUM_ENTRY>
    {
        // ***********************************************************************
        // ************************** Unity Methods ******************************
        // ***********************************************************************
        protected override void Awake()
        {
            base.Awake();
            currentState = SSTATES_ENUM_ENTRY.None;
            lastState = SSTATES_ENUM_ENTRY.None;
            SetState(SSTATES_ENUM_ENTRY.None/*SDEFAULTSTATE_ENTRY*/);
        }

        protected override IState<SSTATES_ENUM_ENTRY> GetStateInstance(SSTATES_ENUM_ENTRY state)
        {
            switch (state)
            {
                case SSTATES_ENUM_ENTRY.None:
                    return new BASE_SSTATE_ENTRY();
                    // ADD_CASE_ENTRIES_ENTRY
            }
            return null;
        }
    }

    [System.Serializable]
    public class BASE_SSTATE_ENTRY : IState<SSTATES_ENUM_ENTRY>
    {
        /// <summary>
        /// The generated Controller script for this state machine
        /// </summary>
        protected SSTATECONTROLLER_ENTRY controller;
        public IStateMachine<SSTATES_ENUM_ENTRY> Controller
        {
            get { return controller; }
            set { controller = value as SSTATECONTROLLER_ENTRY; }
        }
        public virtual bool CanEnter(SSTATES_ENUM_ENTRY lastState) { return true; }
        public virtual void OnEnterState(SSTATES_ENUM_ENTRY lastState) { }
        public virtual void OnExitState(SSTATES_ENUM_ENTRY nextState) { }
        public virtual void Update() { }
        public virtual void LateUpdate() { }
        public virtual void FixedUpdate() { }
        public virtual void OnDrawGizmos() { }
        public virtual void OnRenderObject() { }
    }
}