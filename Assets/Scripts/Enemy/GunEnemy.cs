using UnityEngine;

public class GunEnemy : Enemy, IEnemyAttack
{
    public float attackRange = 15f;
    private int attackDamage = 5;

    public Transform gunPoint;

    float IEnemyAttack.AttackRange => attackRange;

    public void Attack(Transform target)
    {
        // AI GENERATED
        // Look into specifics of the physics behind raycast
        Vector3 targetPosition = target.position + Vector3.up;
        Vector3 direction = targetPosition - gunPoint.position;

        RaycastHit hit;

        if(Physics.Raycast(gunPoint.position, direction.normalized, out hit, attackRange))
        {
            IDamageable playerHealth = hit.collider.GetComponentInParent<IDamageable>();

            if (playerHealth != null && hit.collider.transform.root == target.root)
            {
                playerHealth.TakeDamage(attackDamage);
            }
        }
        // END AI GENERATION
    }
}
