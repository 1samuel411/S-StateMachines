using Sirenix.OdinInspector;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace SLibrary.StateMachines.ScriptableController
{

    public class BaseController<T> : MonoBehaviour, IStateMachine<T> where T : Enum
    {
        [FoldoutGroup("State"), LabelText("$currentState")]
        public IState<T> state = null;

        // Public Variables
        [FoldoutGroup("Base Controller")]
        public bool active = true;
        [FoldoutGroup("Base Controller")]
        public int debugLevel = 1;  // 0 - Off, 1 - Minimal, 2 - Complete 

        // Read-Only Variables
        [ReadOnly, FoldoutGroup("Base Controller")]
        public T currentState;
        [ReadOnly, FoldoutGroup("Base Controller")]
        public T lastState;
        [ReadOnly, FoldoutGroup("Base Controller")]
        public float lastTransitionTime;
        [ReadOnly, FoldoutGroup("Base Controller")]
        public float lastTransitionUnscaledTime;

        // Callbacks
        public delegate void OnStateChange(T from, T to);
        public OnStateChange onStateChange;

        // ***********************************************************************
        // ************************** Unity Methods ******************************
        // ***********************************************************************
        protected virtual void Start() { }

        protected virtual void Update()
        {
            if (state != null)
                state.Update();
        }

        protected virtual void LateUpdate()
        {
            if (state != null)
                state.LateUpdate();
        }

        protected virtual void FixedUpdate()
        {
            if (state != null)
                state.FixedUpdate();
        }

        protected virtual void OnDrawGizmos()
        {
            if (state != null)
                state.OnDrawGizmos();
        }

        protected virtual void OnRenderObject()
        {
            if (state != null)
                state.OnRenderObject();
        }

        // ***********************************************************************
        // ************************* Virtual Methods *****************************
        // ***********************************************************************
        protected virtual IState<T> GetStateInstance(T state)
        {
            return null;
        }

        /// <summary>
        /// Set the current state in this machine.
        /// Returns false if switching to the same state, regardless of forceChange.
        /// </summary>
        /// <param name="state">The State Enum to switch to.</param>
        /// <param name="forceChange">Force the state to switch regardless of the CanEnter result.</param>
        /// <returns>Was the change successful</returns>
        public virtual bool SetState(T state, bool forceChange = false)
        {
            bool response = HandleStateChange(state, forceChange);
            if (debugLevel == 1)
                DebugMessage((response ? "Successfully " : "Failed to ") + (forceChange ? "<b>FORCE</b> " : "") + "SET STATE to " + state.ToString() + ", from " + lastState.ToString(), response ? 1 : 2);
            return response;
        }

        // ***********************************************************************
        // ************************* Private Methods *****************************
        // ***********************************************************************

        /// <summary>
        /// Used to print a message with the proper prefix and also color code the result. 
        /// Example: <b>[BaseExampleController]</b> <color=CONTEXT>MESSAGE</color>
        /// </summary>
        /// <param name="message">The message to be printed following the prefix</param>
        /// <param name="context">0 - None, 1 - Postiive, 2 - Negative</param>
        private void DebugMessage(string message, int context = 0)
        {
            string printedMessage = "<b>[" + this.name + "]</b> " + "<color=";
            switch (context)
            {
                case 0: // No Context
                    printedMessage += "gray";
                    break;
                case 1: // Positive Context
                    printedMessage += "green";
                    break;
                case 2: // Negative Context
                    printedMessage += "red";
                    break;
            }
            printedMessage += ">" + message + "</color>";
            Debug.Log(printedMessage);
        }

        // ***********************************************************************
        // ************************* Private Methods *****************************
        // ***********************************************************************
        private bool HandleStateChange(T newState, bool forceChange)
        {
            if (active == false)
            {
                if (debugLevel == 2 || debugLevel == 1)
                    DebugMessage("Failed State Change because machine is not active " + (forceChange ? " <b>FORCED</b> " : ""), 2);

                return false;
            }

            if (EqualityComparer<T>.Default.Equals(newState, currentState) == false || state == null)
            {
                IState<T> tempNewState = GetStateInstance(newState);
                if (tempNewState == null)
                {
                    DebugMessage("Failed to GET STATE INSTANCE : " + newState.ToString() + " From, " + currentState.ToString() + (forceChange ? " <b>FORCED</b> " : ""), 2);
                    return false;
                }
                tempNewState.Controller = this;

                // Check if we can enter this new state
                if (forceChange == false)
                {
                    if (!tempNewState.CanEnter(currentState))
                    {
                        if (debugLevel == 2 || debugLevel == 1)
                            DebugMessage("Failed State Change during CAN ENTER: " + newState.ToString() + " From, " + currentState.ToString() + (forceChange ? " <b>FORCED</b> " : ""), 2);
                        return false;
                    }
                }

                // Call exit state on last state
                if (state != null)
                {
                    state.OnExitState(currentState);
                }

                // Update state
                state = tempNewState;
                lastState = currentState;
                currentState = newState;

                // Call enter state on new state
                if (state != null)
                    state.OnEnterState(lastState);

                // Record time
                lastTransitionTime = Time.time;
                // Record time
                lastTransitionUnscaledTime = Time.unscaledTime;

                // Call delegate
                onStateChange?.Invoke(lastState, currentState);

                if (debugLevel == 2)
                    DebugMessage("Successfully handled state switch to: " + currentState.ToString() + ", from: " + lastState, 1);

                return true;
            }

            return false;
        }
    }
}