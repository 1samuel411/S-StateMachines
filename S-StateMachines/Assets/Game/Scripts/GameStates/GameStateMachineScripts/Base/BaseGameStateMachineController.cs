using SLibrary.StateMachines;

namespace SLibrary.StateExample
{
    /// <summary>
    /// State Enum Definition from the ScriptableStateController
    /// </summary>
    public enum GameStateMachineStates { None, MainMenu,InGame,Paused,GameOver };

    /// <summary>
    /// This is an automatically generated script that is updated via the ScriptableStateController.
    /// </summary>
    public class BaseGameStateMachineController : BaseController<GameStateMachineStates>, IStateMachine<GameStateMachineStates>
    {
        // ***********************************************************************
        // ************************** Unity Methods ******************************
        // ***********************************************************************
        protected override void Awake()
        {
            base.Awake();
            currentState = GameStateMachineStates.None;
            lastState = GameStateMachineStates.None;
            SetState(GameStateMachineStates.None);
        }

        protected override IState<GameStateMachineStates> GetStateInstance(GameStateMachineStates state)
        {
            switch (state)
            {
                case GameStateMachineStates.None:
                    return new BaseGameStateMachineState();
                    
                case GameStateMachineStates.MainMenu:
                    return new MainMenuState();

                case GameStateMachineStates.InGame:
                    return new InGameState();

                case GameStateMachineStates.Paused:
                    return new PausedState();

                case GameStateMachineStates.GameOver:
                    return new GameOverState();

            }
            return null;
        }
    }

    [System.Serializable]
    public class BaseGameStateMachineState : IState<GameStateMachineStates>
    {
        /// <summary>
        /// The generated Controller script for this state machine
        /// </summary>
        protected GameStateMachineController controller;
        public IStateMachine<GameStateMachineStates> Controller
        {
            get { return controller; }
            set { controller = value as GameStateMachineController; }
        }
        public bool IsLoaded { get => true; }
        public virtual bool CanEnter(GameStateMachineStates lastState) { return true; }
        public virtual void OnEnterState(GameStateMachineStates lastState) { }
        public virtual void OnExitState(GameStateMachineStates nextState) { }
        public virtual void Update() { }
        public virtual void LateUpdate() { }
        public virtual void FixedUpdate() { }
        public virtual void OnDrawGizmos() { }
        public virtual void OnRenderObject() { }
    }
}