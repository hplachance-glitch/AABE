using UnityEngine;

public class EnemyAwareness : MonoBehaviour
{
    public bool IsAggro;
    public float awarenessRadius;
    public LayerMask obstacleLayer;
    private Transform playersTransform;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        playersTransform = FindAnyObjectByType<PlayerMove>().transform;
    }

    // Update is called once per frame
    void Update()
    {
        if (IsAggro)
        {
            return;
        }

        var dis = Vector3.Distance(transform.position, playersTransform.position);

        // AI GENERATED
        if(dis < awarenessRadius)
        {
            Vector3 sightPosition = transform.position + Vector3.up *1.5f;
            Vector3 playerPosition = playersTransform.position + Vector3.up * 1f;

            Vector3 direction = playerPosition - sightPosition;

            // Check if an obstacle blocks the view
            if (!Physics.Raycast(sightPosition, direction.normalized,
                                 direction.magnitude, obstacleLayer))
            {
                IsAggro = true;
            }
        }
    }
}
