using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

public class EnemyAI : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private EnemyAwareness enemyAwareness;
    private Transform playersTransform;
    private NavMeshAgent enemyNavMeshAgent;
    private IEnemyAttack enemyAttack;

    public float attackCooldown = 1.5f;
    private float nextAttackTime;
    private float stoppingDistance;

    private void Start()
    {
        enemyAwareness = GetComponent<EnemyAwareness>();
        playersTransform = FindAnyObjectByType<PlayerMove>().transform;
        enemyNavMeshAgent = GetComponent<NavMeshAgent>();
        enemyAttack = GetComponent<IEnemyAttack>();
        if (GetComponent<SwordEnemy>() != null)
        {
            stoppingDistance = 1;
        }
        else if (GetComponent<GunEnemy>() != null)
        {
            stoppingDistance = 3;
        }

        enemyNavMeshAgent.stoppingDistance = stoppingDistance;

    }

    private void Update()
    {
        if (enemyAwareness.IsAggro)
        {
            enemyNavMeshAgent.SetDestination(playersTransform.position);
            float distance = Vector3.Distance(transform.position, playersTransform.position);

            if (distance <= enemyAttack.AttackRange && Time.time >= nextAttackTime)
            {
                enemyAttack.Attack(playersTransform);
                nextAttackTime = Time.time + attackCooldown;
            }
        }

        else
        {
            enemyNavMeshAgent.SetDestination(transform.position);
        } 
    }
}
