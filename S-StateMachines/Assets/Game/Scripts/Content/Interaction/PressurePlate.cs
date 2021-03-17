using UnityEngine;
using UnityEngine.Events;

namespace SLibrary.StateExample
{
    /// <summary>
    /// Let's you specify a unity event for when a collider's trigger is entered or exited. Used to drive progress in the game. User needs to hit the pressure plate and it'll either load the next level or end the game.
    /// </summary>
    public class PressurePlate : MonoBehaviour
    {

        public UnityEvent onTriggerEnterEvent;
        public UnityEvent onTriggerExitEvent;

        private void OnTriggerEnter(Collider other)
        {
            onTriggerEnterEvent.Invoke();
        }

        private void OnTriggerExit(Collider other)
        {
            onTriggerExitEvent.Invoke();
        }

        /// <summary>
        /// Loads the level specified from the LevelManager. 
        /// Typically I'd be able to use Bolt and send an event to the graph for when the pressure plate has been activated and deactivated, allowing me to call LevelManager.instance.LoadLevel inside of bolt.
        /// </summary>
        /// <param name="level"></param>
        public void LoadLevelEvent(int level)
        {
            LevelManager.instance.LoadLevel(level, true);
            GameStateMachineController.instance.IncrementScore(100);
        }

        /// <summary>
        /// Sets the game state to game over
        /// Typically I'd be able to use Bolt and send an event to the graph for when the pressure plate has been activated and deactivated, allowing me to call the game state change inside of bolt.
        /// </summary>
        /// <param name="level"></param>
        public void GameWin()
        {
            GameStateMachineController.instance.SetState(GameStateMachineStates.GameOver);
        }
    }
}