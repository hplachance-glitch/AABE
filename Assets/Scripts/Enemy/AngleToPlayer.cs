using UnityEngine;
using UnityEngine.UIElements;

public class AlignToPlayer : MonoBehaviour
{
    private Transform player;
    private Vector3 targetPos;
    private Vector3 targetDir;

    //temp
    public SpriteRenderer spriteRenderer;
    public Sprite[] sprites;

    public float angle;
    public int lastIndex;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        player = FindAnyObjectByType<PlayerMove>().transform; //change to PlayerMove later
    
    }

    // Update is called once per frame
    void Update()
{
    // Get Target pos and dir based on the parent's position
    targetPos = new Vector3(player.position.x, transform.parent.position.y, player.position.z);
    targetDir = targetPos - transform.parent.position;

    // Get angle using the ENEMY BODY's forward direction, not the sprite's
    angle = Vector3.SignedAngle(targetDir, transform.parent.forward, Vector3.up);

    // Flip sprite
    Vector3 tempScale = Vector3.one;
    if (angle > 0)
    {
        tempScale.x *= -1f;
    }

    spriteRenderer.transform.localScale = tempScale;
    
    lastIndex = GetIndex(angle);

    spriteRenderer.sprite = sprites[lastIndex];
}

    private int GetIndex(float angle)
    {
        //front
        if(angle > -22.5f && angle < 22.5f)
            return 0;
        if(angle >= 22.5f && angle < 67.5f)
            return 7;
        if(angle >= 67.5f && angle < 112.5f)
            return 6;
        if(angle >= 112.5f && angle < 157.5f)
            return 5;

        //back OJWAODIjaw OMG
        if(angle <= -157.5f || angle >= 157.5f) // Fixed: changed && to ||
            return 4;
        if(angle >= -157.5f && angle < -112.5f)
            return 3;
        if(angle >= -112.5f && angle < -67.5f)
            return 2;
        if(angle >= -67.5f && angle <= -22.5f)
            return 1;

        return lastIndex;
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawRay(transform.position, transform.forward);

        Gizmos.color = Color.blue;
        Gizmos.DrawLine(transform.position, targetPos);
    }
}