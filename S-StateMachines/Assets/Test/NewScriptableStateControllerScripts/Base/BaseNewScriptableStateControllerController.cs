using SLibrary.StateMachines;

namespace Example.States
{
    /// <summary>
    /// State Enum Definition from the ScriptableStateController
    /// </summary>
    public enum NewScriptableStateControllerStates { None, State1,State2,State3 };

    /// <summary>
    /// This is an automatically generated script that is updated via the ScriptableStateController.
    /// </summary>
    public class BaseNewScriptableStateControllerController : BaseController<NewScriptableStateControllerStates>, IStateMachine<NewScriptableStateControllerStates>
    {
        // ***********************************************************************
        // ************************** Unity Methods ******************************
        // ***********************************************************************
        protected override void Awake()
        {
            base.Awake();
            currentState = NewScriptableStateControllerStates.None;
            lastState = NewScriptableStateControllerStates.None;
            SetState(NewScriptableStateControllerStates.State1);
        }

        protected override IState<NewScriptableStateControllerStates> GetStateInstance(NewScriptableStateControllerStates state)
        {
            switch (state)
            {
                case NewScriptableStateControllerStates.None:
                    return new BaseNewScriptableStateControllerState();
                    
                case NewScriptableStateControllerStates.State1:
                    return new State1State();

                case NewScriptableStateControllerStates.State2:
                    return new State2State();

                case NewScriptableStateControllerStates.State3:
                    return new State3State();

            }
            return null;
        }
    }

    [System.Serializable]
    public struct BaseNewScriptableStateControllerState : IState<NewScriptableStateControllerStates>
    {
        /// <summary>
        /// The generated Controller script for this state machine
        /// </summary>
        private NewScriptableStateControllerController controller;
        public IStateMachine<NewScriptableStateControllerStates> Controller
        {
            get { return controller; }
            set { controller = value as NewScriptableStateControllerController; }
        }
        public bool CanEnter(NewScriptableStateControllerStates lastState) { return true; }
        public void OnEnterState(NewScriptableStateControllerStates lastState) { }
        public void OnExitState(NewScriptableStateControllerStates nextState) { }
        public void Update() { }
        public void LateUpdate() { }
        public void FixedUpdate() { }
        public void OnDrawGizmos() { }
        public void OnRenderObject() { }
    }
}