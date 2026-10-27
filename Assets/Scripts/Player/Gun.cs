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


    // Update is called once per frame

    void Update()

    {

        if (Mouse.current != null &&

        Mouse.current.leftButton.wasPressedThisFrame &&

        Time.time > nextTimeToFire)

        {

            Fire();

        }

    }



    void Fire()

    {      
        // simulate gun shoy
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
        GetComponent<AudioSource>().Stop();
        GetComponent<AudioSource>().Play();
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
    }
}