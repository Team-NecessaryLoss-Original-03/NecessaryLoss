using UnityEngine;
using UnityEngine.AI;

public class Cavallini : MonoBehaviour
{
    public Transform targetPoint; // Lasciato per compatibilità
    public KeyCode activationKey = KeyCode.E;
    public Interactables interactableToRemove; // Assegnabile da Inspector

    private NavMeshAgent agent;
    private bool hasMoved = false;
    private bool hasArrived = false;

    private static bool cavallinoInMovimento = false;
    private static Transform playerTransform;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        agent.updateRotation = false; // Gestiamo rotazione manualmente

        if (playerTransform == null)
        {
            GameObject playerObj = GameObject.FindGameObjectWithTag("Player");
            if (playerObj != null)
            {
                playerTransform = playerObj.transform;
            }
            else
            {
                Debug.LogError("Player con tag 'Player' non trovato.");
            }
        }
    }

    void Update()
    {
        // Attiva movimento con E
        if (!hasMoved && !cavallinoInMovimento && Input.GetKeyDown(activationKey))
        {
            if (playerTransform != null)
            {
                Vector3 targetPosition = playerTransform.position;

                if (NavMesh.SamplePosition(targetPosition, out NavMeshHit hit, 1.0f, NavMesh.AllAreas))
                {
                    agent.SetDestination(hit.position);
                    hasMoved = true;
                    cavallinoInMovimento = true;

                    if (interactableToRemove != null)
                    {
                        Destroy(interactableToRemove);
                        Debug.Log("Script Interactable rimosso.");
                    }
                }
            }
        }

        // Rotazione verso direzione movimento con +180° su Y
        if (hasMoved && !hasArrived && agent.velocity.sqrMagnitude > 0.01f)
        {
            Vector3 moveDirection = agent.velocity.normalized;
            moveDirection.y = 0f; // Solo orizzontale

            if (moveDirection != Vector3.zero)
            {
                // Rotazione base verso direzione movimento
                Quaternion baseRotation = Quaternion.LookRotation(moveDirection);
                // Aggiungo 180 gradi sull'asse Y
                Quaternion targetRotation = baseRotation * Quaternion.Euler(0, 180f, 0);

                transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * 10f);
            }
        }

        // Quando arriva a destinazione
        if (hasMoved && !agent.pathPending && agent.remainingDistance <= agent.stoppingDistance && !hasArrived)
        {
            hasArrived = true;
            agent.isStopped = true;
            agent.enabled = false;

            // Cambia il layer in "Map"
            gameObject.layer = LayerMask.NameToLayer("Map");

            cavallinoInMovimento = false;
        }
    }
}

