using SLibrary.StateMachines;

namespace SLibrary.StateExample
{
    /// <summary>
    /// State Enum Definition from the ScriptableStateController
    /// </summary>
    public enum CharacterStateMachineStates { None, Walking,Sprinting,Jumping,Falling };

    /// <summary>
    /// This is an automatically generated script that is updated via the ScriptableStateController.
    /// </summary>
    public class BaseCharacterStateMachineController : BaseController<CharacterStateMachineStates>, IStateMachine<CharacterStateMachineStates>
    {
        // ***********************************************************************
        // ************************** Unity Methods ******************************
        // ***********************************************************************
        protected override void Awake()
        {
            base.Awake();
            currentState = CharacterStateMachineStates.None;
            lastState = CharacterStateMachineStates.None;
            SetState(CharacterStateMachineStates.Walking);
        }

        protected override IState<CharacterStateMachineStates> GetStateInstance(CharacterStateMachineStates state)
        {
            switch (state)
            {
                case CharacterStateMachineStates.None:
                    return new BaseCharacterStateMachineState();
                    
                case CharacterStateMachineStates.Walking:
                    return new WalkingState();

                case CharacterStateMachineStates.Sprinting:
                    return new SprintingState();

                case CharacterStateMachineStates.Jumping:
                    return new JumpingState();

                case CharacterStateMachineStates.Falling:
                    return new FallingState();

            }
            return null;
        }
    }

    [System.Serializable]
    public class BaseCharacterStateMachineState : IState<CharacterStateMachineStates>
    {
        /// <summary>
        /// The generated Controller script for this state machine
        /// </summary>
        protected CharacterStateMachineController controller;
        public IStateMachine<CharacterStateMachineStates> Controller
        {
            get { return controller; }
            set { controller = value as CharacterStateMachineController; }
        }
        public virtual bool CanEnter(CharacterStateMachineStates lastState) { return true; }
        public virtual void OnEnterState(CharacterStateMachineStates lastState) { }
        public virtual void OnExitState(CharacterStateMachineStates nextState) { }
        public virtual void Update() { }
        public virtual void LateUpdate() { }
        public virtual void FixedUpdate() { }
        public virtual void OnDrawGizmos() { }
        public virtual void OnRenderObject() { }
    }
}