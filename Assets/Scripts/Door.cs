using System.Collections;
using UnityEngine;

public class Door : MonoBehaviour, IInteractable
{
    [SerializeField] private Transform player;

    public float openSpeed = 2f;
    public float openHeight = 3f;
    public float openDuration = 3f;
    public float openDistance = 3f;

    private bool isOpen = false;

    Vector3 closedPosition;
    Vector3 openPosition;

    public void Awake()
    {
        closedPosition = transform.position;
        openPosition = closedPosition + Vector3.up * openHeight;
    }

    public void Update()
    {
        if (Vector3.Distance (player.position, transform.position) <= openDistance)
        {
            Interact();
        }
    }
    public void Interact()
    {
        if (!isOpen)
        {
            StartCoroutine(OpenAndCloseDoor());
        }
    }

    private IEnumerator OpenAndCloseDoor()
    {
        isOpen = true;
        yield return MoveDoor(openPosition);

        yield return new WaitForSeconds(openDuration);

        yield return MoveDoor(closedPosition);

        isOpen = false;
    }

    private IEnumerator MoveDoor(Vector3 position)
    {
        while (Vector3.Distance(transform.position, position) > 0.01f)
        {
            transform.position = Vector3.MoveTowards(transform.position, position, openSpeed * Time.deltaTime);

            yield return null;
        }

        transform.position = position;
    }


}
