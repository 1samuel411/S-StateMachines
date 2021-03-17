using UnityEngine;
using UnityEngine.Events;

namespace SLibrary.StateExample
{
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
            LevelManager.instance.LoadLevel(level);
            GameManager.instance.GetGameData().score.SetValue(GameManager.instance.GetGameData().score.GetValue() + 100);
        }

        /// <summary>
        /// Sets the game state to game over
        /// Typically I'd be able to use Bolt and send an event to the graph for when the pressure plate has been activated and deactivated, allowing me to call the game state change inside of bolt.
        /// </summary>
        /// <param name="level"></param>
        public void GameWin()
        {
            GameManager.instance.gameStateController.SetState(GameStateMachineStates.GameOver);
        }
    }
}