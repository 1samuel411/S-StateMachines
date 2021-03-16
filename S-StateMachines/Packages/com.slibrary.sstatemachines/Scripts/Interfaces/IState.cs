using System;

namespace SLibrary.StateMachines
{
    public interface IState<T> where T : Enum
    {

        /// <summary>
        /// The generated Controller script for this state machine
        /// </summary>
        IStateMachine<T> Controller { get; set; }

        // Methods the state can override
        bool CanEnter(T lastState); // Returns whether the state could be entered. Useful for checking if you can jump.
        void OnEnterState(T lastState); // Returns whether the state could be entered. Useful for checking if you can jump.
        void Update(); // Called every frame when the state is active. 
        void LateUpdate();// Called every frame when the state is active. In LateUpdate
        void FixedUpdate();// Called every frame when the state is active. In LateUpdate
        void OnExitState(T nextState);// Called when a state is being switched to another.
        void OnDrawGizmos();// Used to draw gizmos into the editor.
        void OnRenderObject();// Called every time the object is rendered.

    }
}