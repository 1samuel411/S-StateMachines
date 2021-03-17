using System;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace SLibrary.StateExample
{
    /// <summary>
    /// Used to drive the character movement
    /// </summary>
    public class CharacterStateMachineController : BaseCharacterStateMachineController
    {
        public float walkSpeed = 2.0f;
        public float sprintSpeed = 4.0f;
        public float jumpHeight = 1.0f;
        public float gravityValue = -9.81f;
        public float normalControl = 15;
        public float jumpControl = 7;

        private CharacterController controller;
        private Vector2 inputVector;
        private Vector3 playerVelocity;
        private bool groundedPlayer;
        private float curSpeed;
        private float curControl;
        private bool lost = false;

        protected override void Awake()
        {
            base.Awake();

            controller = gameObject.GetComponent<CharacterController>();

        }

        protected override void Start()
        {
            base.Start();

        }

        protected override void Update()
        {
            base.Update();
         
            ApplyMovement();

            CheckIfLost();
        }

        protected override void OnDrawGizmos()
        {
            base.OnDrawGizmos();
        }

        void ApplyMovement()
        {
            groundedPlayer = controller.isGrounded;
            
            // Check if we're grounded
            if (groundedPlayer && playerVelocity.y < 0)
            {
                playerVelocity.y = gravityValue * Time.deltaTime;
            }

            playerVelocity.y += gravityValue * Time.deltaTime;
            controller.Move(playerVelocity * Time.deltaTime);

            playerVelocity = Vector3.Lerp(playerVelocity, new Vector3(inputVector.x * curSpeed, playerVelocity.y, inputVector.y * curSpeed), curControl * Time.deltaTime);
            
            // Looks towards movement
            if(inputVector != Vector2.zero)
                transform.forward = Vector3.Lerp(transform.forward, new Vector3(inputVector.x, 0, inputVector.y), 12 * Time.deltaTime);
            
            controller.Move(playerVelocity * Time.deltaTime);
        }

        void CheckIfLost()
        {
            // Check if the user fell out of bounds
            if (IsOutOfBounds() && !lost)
            {
                lost = true;

                GameStateMachineController.instance.TakeLife();
            }
        }

        internal void Teleport(Vector3 pos)
        {
            controller.enabled = false;
            transform.position = pos;
            controller.enabled = true;
        }

        public void SetSpeed(float speed)
        {
            curSpeed = speed;
        }


        public void SetControl(float control)
        {
            curControl = control;
        }

        public void SetInputVector()
        {
            inputVector = Vector2.zero;
            if (Input.GetKey(KeyCode.W))
                inputVector.y = 1;
            if (Input.GetKey(KeyCode.S))
                inputVector.y = -1;
            if (Input.GetKey(KeyCode.D))
                inputVector.x = 1;
            if (Input.GetKey(KeyCode.A))
                inputVector.x = -1;
        }

        public void Jump()
        {
            playerVelocity.y += Mathf.Sqrt(jumpHeight * -3.0f * gravityValue);
        }

        public bool IsGrounded()
        {
            return groundedPlayer;
        }
        public bool IsOutOfBounds()
        {
            return transform.position.y < -4;
        }

    }
}