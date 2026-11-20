using UnityEngine;

public class HealthPickup : MonoBehaviour, IInteractable
{
    public int healAmount;

    public float floatSpeed = 2f;
    public float floatHeight = 0.2f;

    public Vector3 startPosition;
    private PlayerHealth playerHealth;

    void Start()
    {
        startPosition = transform.position;
    }

    void Update()
    {
        transform.position = startPosition + Vector3.up * Mathf.Sin(Time.time * floatSpeed) * floatHeight;
    }

    void OnTriggerEnter(Collider other)
    {
        playerHealth = other.GetComponentInParent<PlayerHealth>();
        Interact();
    }

    public void Interact()
    {
        playerHealth.Heal(healAmount);
        Destroy(gameObject);
    }
    
}
