using UnityEngine;
using UnityEngine.AI;

public class Cavallini : MonoBehaviour
{
    public Transform targetPoint;
    public KeyCode activationKey = KeyCode.E;
    public Interactables interactableToRemove; // Assegna da Inspector

    private NavMeshAgent agent;
    private bool playerInTrigger = false;
    private bool hasMoved = false;
    private bool hasArrived = false;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
    }

    void Update()
    {
        // Se il player è nel trigger, preme E e l’NPC non si è ancora mosso
        if (playerInTrigger && !hasMoved && Input.GetKeyDown(activationKey))
        {
            agent.SetDestination(targetPoint.position);
            hasMoved = true;
            if (interactableToRemove != null)
            {
                Destroy(interactableToRemove);
                Debug.Log("Script Interactable rimosso.");
            }
            else
            {
                Debug.LogWarning("Nessun riferimento a Interactable da rimuovere.");
            }
        }

        // Controlla se ha raggiunto la destinazione
        if (hasMoved && !agent.pathPending && agent.remainingDistance <= agent.stoppingDistance && !hasArrived)
        {
            hasArrived = true;
            agent.isStopped = true;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerInTrigger = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerInTrigger = false;
        }
    }
}
