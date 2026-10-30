using UnityEngine;

public interface IDamageable
{
    void TakeDamage(float damage);
}

public interface IInteractable
{
    void Interact();
}
