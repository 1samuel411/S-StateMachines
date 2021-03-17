using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SLibrary.StateExample
{
    /// <summary>
    /// Follows the player controller around forever if it exists.
    /// </summary>
    public class CameraController : MonoBehaviour
    {
        public Vector3 offset;
        public float followSpeed;

        public void LateUpdate()
        {
            if (GameStateMachineController.instance != null && GameStateMachineController.instance.GetSpawnedPlayer() != null)
            {
                transform.position = Vector3.Lerp(transform.position, GameStateMachineController.instance.GetSpawnedPlayer().transform.position + offset, followSpeed * Time.deltaTime);
            }
        }
    }
}