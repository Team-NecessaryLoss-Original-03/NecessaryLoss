using System;
using UnityEngine;

public class Interactable : MonoBehaviour
{
    [Header("--- Interaction elements ---")]
    [SerializeField] private Transform playerTransform;
    [SerializeField] private float interactionAngle = 20f;
    public static Action<string> OnActivation;

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (IsLookingAt(this.transform))
            {
                // TODO: insert UX "press E to use"
                //OnLookingAtInteractable?.Invoke();

                // TODO: change getkey to new InputSystem
                if (Input.GetKeyDown(KeyCode.E))
                {
                    ActivateLeverOrButton();
                }
            }
        }
    }

    void ActivateLeverOrButton()
    {
        Debug.Log("Actiion HERE");
        OnActivation?.Invoke(name);
    }

    /*private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("TEST");
        }
    }*/

    private bool IsLookingAt(Transform target)
    {
        Vector3 directionToTarget = (target.position - playerTransform.position).normalized;

        float playerAngle = Vector3.Angle(playerTransform.forward, directionToTarget);

        return playerAngle < interactionAngle;
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawLine(playerTransform.position, playerTransform.position + playerTransform.forward * 2);

    }
}

/*
using static UnityEngine.GraphicsBuffer;
[Header("--- Interaction elements ---")]
[SerializeField] private Transform playerTransform;
[SerializeField] private Transform cameraTransform;
[SerializeField] private float interactionAngle = 20f;

private bool IsLookingAt(Transform target)
{
        
Vector3 directionToTarget = (target.position - playerTransform.position).normalized;

float playerAngle = Vector3.Angle(playerTransform.forward, directionToTarget);
float cameraAngle = Vector3.Angle(cameraTransform.forward, directionToTarget);

return playerAngle < interactionAngle && cameraAngle < interactionAngle;
        

Vector3 toTarget = (target.position - cameraTransform.position).normalized;

Vector3 cameraForward = Vector3.ProjectOnPlane(cameraTransform.forward, Vector3.up).normalized;

Vector3 flatDirectionToTarget = Vector3.ProjectOnPlane(toTarget, Vector3.up).normalized;

float angle = Vector3.Angle(cameraForward, flatDirectionToTarget);

return angle < interactionAngle;
}

private void OnTriggerStay(Collider other)
{
if (other.CompareTag("Player"))
{
if (IsLookingAt(this.transform))
{
// TODO: insert UX "press E to use"
//OnLookingAtInteractable?.Invoke();

// TODO: change getkey to new InputSystem
if (Input.GetKeyDown(KeyCode.E))
{
ActivateLeverOrButton();
}
}
}
}

void ActivateLeverOrButton()
{
Debug.Log("Actiion HERE");
}

private void OnDrawGizmosSelected()
{
Gizmos.color = Color.yellow;
Gizmos.DrawLine(playerTransform.position, playerTransform.position + playerTransform.forward * 2);
Gizmos.color = Color.cyan;
Gizmos.DrawLine(cameraTransform.position, cameraTransform.position + cameraTransform.forward * 2);

Vector3 camForwardFlat = Vector3.ProjectOnPlane(cameraTransform.forward, Vector3.up).normalized;
Gizmos.color = Color.blue;
Gizmos.DrawLine(cameraTransform.position, cameraTransform.position + camForwardFlat * 2);
}
*/
