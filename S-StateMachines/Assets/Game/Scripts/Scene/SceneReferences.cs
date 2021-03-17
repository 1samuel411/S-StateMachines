using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SLibrary.StateExample
{
    /// <summary>
    /// Stored in every level for maintaining references in the Scene. Currently only used for the spawnpoint reference
    /// </summary>
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