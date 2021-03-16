using System;

namespace SLibrary.StateMachines
{
    public interface IStateMachine<T> where T : Enum
    {
        bool SetState(T state, bool forceChange = false);
    }
}