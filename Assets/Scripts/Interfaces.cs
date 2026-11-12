using UnityEngine;

public interface IDamageable
{
    void TakeDamage(int damage);
}

public interface IInteractable
{
    void Interact();
}

public interface IEnemyAttack
{
    float AttackRange { get; }

    void Attack(Transform target);
}
