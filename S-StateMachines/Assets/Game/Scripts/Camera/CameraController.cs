using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SLibrary.StateExample
{
    public class CameraController : MonoBehaviour
    {
        public Vector3 offset;
        public float followSpeed;

        public void LateUpdate()
        {
            if (GameManager.instance != null && GameManager.instance.GetSpawnedPlayer() != null)
            {
                transform.position = Vector3.Lerp(transform.position, GameManager.instance.GetSpawnedPlayer().transform.position + offset, followSpeed * Time.deltaTime);
            }
        }
    }
}