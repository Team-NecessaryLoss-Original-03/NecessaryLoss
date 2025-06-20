using UnityEngine;

namespace DebugMarco
{
    public class DEBUG_ThirdPersonCamera : MonoBehaviour
    {
        [Header("Target Settings")]
        [SerializeField] private Transform target; // Player to follow
        [SerializeField] private Vector3 offset = new Vector3(0, 3, -6); // Height and distance from the player

        [Header("Camera Rotation Settings")]
        [SerializeField] private float mouseSensitivity = 5f;
        [SerializeField] private float distance = 6f;
        [SerializeField] private float minYAngle = -30f;
        [SerializeField] private float maxYAngle = 60f;

        private float currentDistance;

        private float rotationX = 0f; // up/down
        private float rotationY = 0f; // left/right

        private void Start()
        {
            Vector3 angles = transform.eulerAngles;
            rotationX = angles.y;
            rotationY = angles.x;

            currentDistance = distance;

            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }

        private void LateUpdate()
        {
            if (!target) return;

            // Handle mouse look
            rotationX += Input.GetAxis("Mouse X") * mouseSensitivity;
            rotationY -= Input.GetAxis("Mouse Y") * mouseSensitivity;
            rotationY = Mathf.Clamp(rotationY, minYAngle, maxYAngle);
            Quaternion rotation = Quaternion.Euler(rotationY, rotationX, 0);

            Vector3 origin = target.position + offset;
            Vector3 desiredCameraPos = origin - (rotation * Vector3.forward * distance);
            Vector3 direction = (desiredCameraPos - origin).normalized;

            float targetDistance = distance;

            // Perform SphereCast to detect obstacles
            if (Physics.SphereCast(origin, 0.3f, direction, out RaycastHit hit, distance))
            {
                targetDistance = hit.distance - 0.3f;
            }

            // Clamp and smooth the distance
            targetDistance = Mathf.Clamp(targetDistance, 1f, distance);
            currentDistance = Mathf.Lerp(currentDistance, targetDistance, Time.deltaTime * 10f); // 10 = smooth speed

            // Set final position and rotation
            Vector3 finalPosition = origin - (rotation * Vector3.forward * currentDistance);
            transform.position = finalPosition;
            transform.rotation = rotation;
        }
    }
}
