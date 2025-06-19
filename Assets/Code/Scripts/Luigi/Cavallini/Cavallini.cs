using UnityEngine;
using UnityEngine.AI;

public class Cavallini : MonoBehaviour
{
    public Transform targetPoint;
    public KeyCode activationKey = KeyCode.E;
    private NavMeshAgent agent;
    private bool playerInTrigger = false;
    private bool hasMoved = false;

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
        }

        // Ferma l’NPC quando ha raggiunto la destinazione
        if (hasMoved && !agent.pathPending && agent.remainingDistance <= agent.stoppingDistance)
        {
            agent.isStopped = true;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("Entro");
            playerInTrigger = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("Esco");
            playerInTrigger = false;
        }
    }
}
