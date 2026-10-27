using UnityEngine;

public class EnemyAwareness : MonoBehaviour
{
    public bool IsAggro;
    public float awarenessRadius;
    private Transform playersTransform;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        playersTransform = FindAnyObjectByType<PlayerMove>().transform;
    }

    // Update is called once per frame
    void Update()
    {
        var dis = Vector3.Distance(transform.position, playersTransform.position);

        if(dis < awarenessRadius)
        {
            IsAggro = true;
        }
    }
}
