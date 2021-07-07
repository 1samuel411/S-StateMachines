using System;

namespace SLibrary.StateMachines
{
    public interface IStateMachine
    {
        bool SetState(Object state, bool forceChange = false);
        Object CurrentState { get; }
        Object LastState { get; }
        float LastTransitionTime { get; }
        float LastTransitionUnscaledTime { get; }
    }

    public interface IStateMachine<T> : IStateMachine where T : Enum
    {
        bool SetState(T state, bool forceChange = false);

        new T CurrentState { get; }
        new T LastState { get; }
    }
}
