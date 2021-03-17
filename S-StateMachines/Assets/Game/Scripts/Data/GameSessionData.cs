using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace SLibrary.StateExample
{
    /// <summary>
    /// Data used during a game session, reset when starting the game.
    /// </summary>
    [System.Serializable]
    public class GameSessionData
    {
        public Bindable<int> score;
        public Bindable<int> lives;

        public GameSessionData()
        {
            score = new Bindable<int>(0);
            lives = new Bindable<int>(0);
        }

    }
}