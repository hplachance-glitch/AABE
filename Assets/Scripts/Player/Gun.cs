using System.Diagnostics;

using UnityEngine;

using UnityEngine.InputSystem;



public class Gun : MonoBehaviour

{

    public float range = 20f;

    public float verticalRange = 20f;

    public float fireRate;

    public float bigDamage = 2f;

    public float smallDamage = 1f;
    public float gunshotradius = 20f;



    public LayerMask raycastLayerMask;

    public LayerMask EnemyLayerMask;

    public EnemyManager enemyManager;



    private float nextTimeToFire;

    private BoxCollider gunTrigger;



    // Start is called once before the first execution of Update after the MonoBehaviour is created

    void Start()

    {

        gunTrigger = GetComponent<BoxCollider>();

        gunTrigger.size = new Vector3(1, verticalRange, range);

        gunTrigger.center = new Vector3(0, 0, range/2);

    }



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
            enemyCollider.GetComponent<EnemyAwareness>().IsAggro = true;
        }

        //Gun shot pow pow powww
        GetComponent<AudioSource>().Stop();
        GetComponent<AudioSource>().Play();
        //damage enemies

        foreach (var enemy in enemyManager.enemiesInTrigger)

        {

            var dir = enemy.transform.position - transform.position;

            RaycastHit hit;

            if(Physics.Raycast(transform.position, dir, out hit, range * 1.5f, raycastLayerMask))

            {

                if(hit.transform == enemy.transform)

                {

                    float dist = Vector3.Distance(enemy.transform.position, transform.position);



                    if (dist > range * 0.5f)

                    {

                        enemy.TakeDamage(smallDamage);

                    }

                    else

                    {

                        enemy.TakeDamage(bigDamage);

                    }

                }

            }

        }



        //reset timer

        nextTimeToFire = Time.time + fireRate;

    }



    private void OnTriggerEnter(Collider other)

    {

        Enemy enemy = other.transform.GetComponent<Enemy>();

        if (enemy)

        {

            enemyManager.AddEnemy(enemy);

        }

    }



    private void OnTriggerExit(Collider other)

    {

        Enemy enemy = other.transform.GetComponent<Enemy>();

        if (enemy)

        {

            enemyManager.RemoveEnemy(enemy);

        }

    }

} 