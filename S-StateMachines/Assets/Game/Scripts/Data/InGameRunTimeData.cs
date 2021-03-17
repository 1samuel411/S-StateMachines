using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SLibrary.StateExample
{
    [System.Serializable]
    public class InGameRunTimeData
    {
        public Bindable<int> score;
        public Bindable<int> lives;

        public InGameRunTimeData()
        {
            score = new Bindable<int>(0);
            lives = new Bindable<int>(0);
        }
    }
}