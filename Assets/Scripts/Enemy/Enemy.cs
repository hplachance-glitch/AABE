using UnityEngine;

public class Enemy : MonoBehaviour, IDamageable
{

    public EnemyManager enemyManager;
    private int enemeyHealth = 2;

    public GameObject Blood;

    public void TakeDamage(int damage)
    {
        Instantiate(Blood, transform.position, Quaternion.identity);
        enemeyHealth -= damage;

        if(enemeyHealth <= 0)
        {
            enemyManager.RemoveEnemy(this);
            Destroy(gameObject);
        }
    }
}
