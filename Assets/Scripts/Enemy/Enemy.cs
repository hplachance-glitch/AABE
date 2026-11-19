using UnityEngine;

public class Enemy : MonoBehaviour, IDamageable
{

    public EnemyManager enemyManager;
    private int enemeyHealth = 2;

    private Animator spriteAnim;
    private AlignToPlayer alignToPlayer;

    [SerializeField] private GameObject Corpse;

    public bool IsDead = false;

    public GameObject Blood;

    private void Start()
    {
        spriteAnim = GetComponentInChildren<Animator>();
        alignToPlayer = GetComponentInChildren<AlignToPlayer>();

        enemyManager = FindAnyObjectByType<EnemyManager>();
    }

    void Update()
    {
        spriteAnim.SetFloat("spriteRot", alignToPlayer.lastIndex);
    }

    public void PlayAttackAnimation()
    {
        spriteAnim.SetTrigger("attack");
    }

    public void SetWalking(bool isWalking)
    {
        spriteAnim.SetBool("isWalking", isWalking);
    }

    public bool IsAttacking()
    {
        return spriteAnim.GetCurrentAnimatorStateInfo(0).IsName("Attack");
    }
    public void TakeDamage(int damage)
    {
        Instantiate(Blood, transform.position, Quaternion.identity);
        enemeyHealth -= damage;

        if(enemeyHealth <= 0)
        {
            enemyManager.RemoveEnemy(this);
            Instantiate(Corpse, transform.position, transform.rotation);
            Destroy(gameObject);
        }
    }
}
