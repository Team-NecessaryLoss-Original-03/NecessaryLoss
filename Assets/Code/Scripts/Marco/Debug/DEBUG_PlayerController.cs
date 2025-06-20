using UnityEngine;

namespace DebugMarco
{
    [RequireComponent(typeof(CharacterController))]
    public class DEBUG_PlayerController : MonoBehaviour
    {
        [Header("References")]
        private CharacterController controller;
        [SerializeField] private Transform camera;

        [Header("--- Movement Settings ---")]
        [SerializeField] private float walkSpeed = 8f;
        [SerializeField] private KeyCode sprintKey = KeyCode.LeftShift;
        [SerializeField] private float sprintSpeed = 4f;
        [SerializeField] private float sprintTransitionSpeed = 5f;
        private bool IsToggledRun;
        [SerializeField] private float turningSpeed = 3f;
        [SerializeField] private float gravity = 9.81f;
        [SerializeField] private float jumpHeight = 2f;

        private float verticalVelocity;
        private float speed;

        [Header("Input")]
        private float moveInput; //wasd
        private float turnInput; //mouse

        private void Awake()
        {
            controller = GetComponent<CharacterController>();
        }

        private void Update()
        {
            InputManagement();
            Movement();
        }

        private void InputManagement()
        {
            moveInput = Input.GetAxis("Vertical");
            turnInput = Input.GetAxis("Horizontal");
        }

        private void Movement()
        {
            GroundMovement();
            Turn();
        }

        private void GroundMovement()
        {
            //V3 => x, y, z
            Vector3 move = new Vector3(turnInput, 0, moveInput);
            //movement aligned with view direction
            move = camera.transform.TransformDirection(move);

            if (IsToggledRun)
            {
                speed = Mathf.Lerp(speed, sprintSpeed, sprintTransitionSpeed * Time.deltaTime);
            }
            else
            {
                speed = Mathf.Lerp(speed, walkSpeed, sprintTransitionSpeed * Time.deltaTime);
            }

            //change walk/run logic
            if (Input.GetKeyDown(sprintKey))
            {
                IsToggledRun = !IsToggledRun;
            }

            //move the character
            move *= speed;
            //gravity
            move.y = VerticalForceCalculation();
            //ensure smooth movement for every frame rate
            controller.Move(Time.deltaTime * move);
        }

        private void Turn()
        {
            if (Mathf.Abs(turnInput) > 0 || Mathf.Abs(moveInput) > 0)
            {
                Vector3 currentLookDirection = controller.velocity.normalized;
                currentLookDirection.y = 0;
                currentLookDirection.Normalize();

                Quaternion targetRotation = Quaternion.LookRotation(currentLookDirection);
                transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * turningSpeed);
            }
        }

        private float VerticalForceCalculation()
        {
            if (controller.isGrounded)
            {
                verticalVelocity = -1f;

                if (Input.GetButtonDown("Jump"))
                {
                    verticalVelocity = Mathf.Sqrt(jumpHeight * gravity * 2);
                }
            }
            else
            {
                float fallMultiplier = Mathf.Clamp(controller.transform.position.y - GetGroundHeight(), 1f, 3f); // Modifica la velocit  di caduta in base all'altezza
                verticalVelocity -= gravity * fallMultiplier * Time.deltaTime;
            }

            return verticalVelocity;
        }

        private float GetGroundHeight()
        {
            RaycastHit hit;
            if (Physics.Raycast(controller.transform.position, Vector3.down, out hit, Mathf.Infinity))
            {
                return hit.point.y;
            }
            return controller.transform.position.y; // player is airborne
        }
    }
}
