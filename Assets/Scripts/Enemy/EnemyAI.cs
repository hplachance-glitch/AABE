using UnityEngine;
using UnityEngine.AI;

public class EnemyAI : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private EnemyAwareness enemyAwareness;
    private Transform playersTransform;
    private NavMeshAgent enemyNavMeshAgent;

    private void Start()
    {
        enemyAwareness = GetComponent<EnemyAwareness>();
        playersTransform = FindAnyObjectByType<PlayerMove>().transform;
        enemyNavMeshAgent = GetComponent<NavMeshAgent>();

    }

    private void Update()
    {
        if (enemyAwareness.IsAggro)
        {
            enemyNavMeshAgent.SetDestination(playersTransform.position);
        }

        else
        {
            enemyNavMeshAgent.SetDestination(transform.position);
        }
        
        

    }
}
