using SLibrary.StateMachines;

namespace Example.States
{
    /// <summary>
    /// The template for a state object
    /// </summary>

    [System.Serializable]
    public struct State2State : IState<NewScriptableStateControllerStates>
    {
        /// <summary>
        /// The generated Controller script for this state machine
        /// </summary>
        private NewScriptableStateControllerController Controller;

        IStateMachine<NewScriptableStateControllerStates> IState<NewScriptableStateControllerStates>.Controller { get => Controller; set => Controller = value as NewScriptableStateControllerController; }

        public bool CanEnter(NewScriptableStateControllerStates lastState) 
        { 
            return true;
        }

        public void OnEnterState(NewScriptableStateControllerStates lastState) { }
        public void OnExitState(NewScriptableStateControllerStates nextState) { }
        public void Update() { }
        public void LateUpdate() { }
        public void FixedUpdate() { }
        public void OnDrawGizmos() { }
        public void OnRenderObject() { }
    }
}