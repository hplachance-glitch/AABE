using UnityEngine;

public class SwordEnemy : Enemy, IEnemyAttack
{
    public float attackRange = 1f;
    private int attackDamage = 2;

    float IEnemyAttack.AttackRange => attackRange;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public void Attack(Transform target)
    {
        IDamageable playerHealth = target.GetComponent<IDamageable>();
        playerHealth.TakeDamage(attackDamage);
    }
}
