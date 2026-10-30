using UnityEngine;
using UnityEngine.InputSystem;


public class Gun : MonoBehaviour

{

    public float range = 20f;
    public float fireRate;

    public float bigDamage = 2f;

    public float smallDamage = 1f;
    public float gunshotradius = 20f;


    public LayerMask raycastLayerMask;

    public LayerMask EnemyLayerMask;

    public EnemyManager enemyManager;

    private float nextTimeToFire;
    private float fireCount = 0;

    public AudioSource gunshotSound;
    public AudioSource reloadSound;

    public float reloadTime = 2f;
    private bool isReloading = false;
    private float reloadEndTime;

    // TO BE IMPLEMENTED RELOAD ANIM
    //public Animator reloadAnimation;

    void Update()
    {
        if (isReloading && Time.time >= reloadEndTime)
        {
            isReloading = false;
        }
        
        if (Mouse.current != null &&

            Mouse.current.leftButton.wasPressedThisFrame &&

            Time.time > nextTimeToFire && !isReloading)

        {
            Fire();
        }
    }

    void Fire()

    {      
        fireCount++;

        // simulate gun shot
        Collider[] enemyColliders;
        enemyColliders = Physics.OverlapSphere(transform.position, gunshotradius, EnemyLayerMask);

        // alert enemies from sound
        foreach (var enemyCollider in enemyColliders)
        {
            EnemyAwareness awareness = enemyCollider.GetComponentInParent<EnemyAwareness>();
            if (awareness != null)
            {
                awareness.IsAggro = true;
            }
        }

        //Gun shot pow pow powww
        gunshotSound.Stop();
        gunshotSound.Play();
        //damage enemies

        RaycastHit hit;
            
        if(Physics.Raycast(transform.position, transform.forward, out hit, range, raycastLayerMask))
        {  
            IDamageable damageable = hit.collider.GetComponentInParent<Enemy>();

            if(damageable != null)
            {

                float dist = hit.distance;

                if (dist > range * 0.5f)
                {
                    damageable.TakeDamage(smallDamage);
                }

                else
                {
                    damageable.TakeDamage(bigDamage);
                }
            }
        }

        //reset timer
        nextTimeToFire = Time.time + fireRate;

        if (fireCount == 12)
        {
            Reload();
        }
    }

    void Reload()
    {
        isReloading = true;
        reloadEndTime = Time.time + reloadTime;
        
        reloadSound.Play();
        //TO BE IMPLEMENTED, PLAY ANIM
        //reloadAnimation.SetTrigger("Reload");

        fireCount = 0;
    }
}