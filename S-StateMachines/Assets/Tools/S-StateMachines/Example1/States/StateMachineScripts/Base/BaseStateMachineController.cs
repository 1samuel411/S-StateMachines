using Sirenix.OdinInspector;
using SLibrary.StateMachines;
using SLibrary.StateMachines.ScriptableController;

namespace Example.States
{
    /// <summary>
    /// State Enum Definition from the ScriptableStateController
    /// </summary>
    public enum StateMachineStates { None, StateA,StateB };

    /// <summary>
    /// This is an automatically generated script that is updated via the ScriptableStateController.
    /// </summary>
    public class BaseStateMachineController : BaseController<StateMachineStates>, IStateMachine<StateMachineStates>
    {
        // Reference to the current state instance
        [FoldoutGroup("Current State"), LabelText("$currentState")]
        public new BaseStateMachineController state;

        // ***********************************************************************
        // ************************** Unity Methods ******************************
        // ***********************************************************************
        protected override void Start()
        {
            base.Start();
            SetState(StateMachineStates.None);
        }

        protected override IState<StateMachineStates> GetStateInstance(StateMachineStates state)
        {
            switch (state)
            {
                case StateMachineStates.None:
                    return new BaseStateMachineState();
                    
                case StateMachineStates.StateA:
                    return new StateAState();

                case StateMachineStates.StateB:
                    return new StateBState();

            }
            return null;
        }
    }

    [System.Serializable]
    public class BaseStateMachineState : IState<StateMachineStates>
    {
        /// <summary>
        /// The generated Controller script for this state machine
        /// </summary>
        protected StateMachineController controller;
        public IStateMachine<StateMachineStates> Controller
        {
            get { return controller; }
            set { controller = value as StateMachineController; }
        }
        public virtual bool CanEnter(StateMachineStates lastState) { return true; }
        public virtual void OnEnterState(StateMachineStates lastState) { }
        public virtual void OnExitState(StateMachineStates nextState) { }
        public virtual void Update() { }
        public virtual void LateUpdate() { }
        public virtual void FixedUpdate() { }
        public virtual void OnDrawGizmos() { }
        public virtual void OnRenderObject() { }
    }
}