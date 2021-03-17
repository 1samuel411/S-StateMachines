using UnityEngine;
using UnityEngine.SceneManagement;

namespace SLibrary.StateExample
{
    /// <summary>
    /// This controller is used to power the core gameplay. Keeps track of Game Session Data and handles important game events.
    /// Ideally the player would be managed by a PlayerManager, but for this simple game this is fine :)
    /// </summary>
    [AddComponentMenu("States/State Machine")]
    public class GameStateMachineController : BaseGameStateMachineController
    {

        public static GameStateMachineController instance;

        private GameSessionData gameSessionData = new GameSessionData();
        private CharacterStateMachineController spawnedPlayer;

        protected override void Awake()
        {
            base.Awake();
            instance = this;
            gameSessionData = new GameSessionData();
        }

        protected override void Start()
        {
            base.Start();
        }

        protected override void Update()
        {
            base.Update();
        }

        protected override void OnDrawGizmos()
        {
            base.OnDrawGizmos();
        }

        public void StartNewGame()
        {
            gameSessionData.lives.SetValue(GameProperties.instance.startingLives);
            gameSessionData.score.SetValue(0);
        }

        public void InitializePlayer()
        {
            // Spawn the player
            spawnedPlayer = Instantiate(GameProperties.instance.playerPrefab).GetComponent<CharacterStateMachineController>();
            spawnedPlayer.Teleport(SceneReferences.instance.spawnpointTransform.position);
        }

        /// <summary>
        /// Removes a life, if the lives are all depleted then switch to game over, otherwise reload the level
        /// </summary>
        public void TakeLife()
        {
            gameSessionData.lives.SetValue(gameSessionData.lives.GetValue() - 1);

            if (gameSessionData.lives.GetValue() <= 0)
                GameStateMachineController.instance.SetState(GameStateMachineStates.GameOver);
            else
            {
                LevelManager.instance.LoadLevel(SceneManager.GetActiveScene().buildIndex, true);
                IncrementScore(-25);
            }
        }

        public void IncrementScore(int increment)
        {
            gameSessionData.score.SetValue(gameSessionData.score.GetValue() + increment);
        }

        public CharacterStateMachineController GetSpawnedPlayer()
        {
            return spawnedPlayer;
        }

        public GameSessionData GetGameData()
        {
            return gameSessionData;
        }
    }
}