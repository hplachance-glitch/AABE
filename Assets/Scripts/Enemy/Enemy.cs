using UnityEngine;

public class Enemy : MonoBehaviour
{

    public EnemyManager enemyManager;
    private float enemeyHealth = 2f;

    public GameObject Blood;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(enemeyHealth <= 0)
        {
            enemyManager.RemoveEnemy(this);
            Destroy(gameObject);
        }
    }

    public void TakeDamage(float damage)
    {
        Instantiate(Blood, transform.position, Quaternion.identity);
        enemeyHealth -= damage;
    }
}
