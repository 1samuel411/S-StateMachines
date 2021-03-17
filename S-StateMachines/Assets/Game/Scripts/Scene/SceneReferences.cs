using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SLibrary.StateExample
{
    public class SceneReferences : MonoBehaviour
    {

        public static SceneReferences instance;

        public Transform spawnpointTransform;

        private void Awake()
        {
            instance = this;
        }

    }
}