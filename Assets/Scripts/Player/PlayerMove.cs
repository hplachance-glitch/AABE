using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Video;

public class PlayerMove : MonoBehaviour
{
    
    private PlayerInput playerInput;

    public float playerSpeed = 20f;
    private float myGravity = -10f;

    private CharacterController myCC;
    public Animator camAnim;
    private bool isWalking;

    private Vector3 inputVector;
    private Vector3 movementVector;

    void Start()
    {
        myCC = GetComponent<CharacterController>();
        playerInput = GetComponent<PlayerInput>();
    }

    void Update()
    {
        GetInput();
        MovePlayer();
        CheckForHeadBob();

        camAnim.SetBool("isWalking", isWalking);
    }

    void GetInput()
    {
        // AI Generated
        Vector2 input = playerInput.actions["Move"].ReadValue<Vector2>();
        // End AI Generation

        inputVector = new Vector3(input.x, 0f, input.y);
        inputVector.Normalize();
        inputVector = transform.TransformDirection(inputVector);

        movementVector = (inputVector * playerSpeed) + (Vector3.up * myGravity);
    }

    void MovePlayer()
    {
        myCC.Move(movementVector * Time.deltaTime);
    }

    void CheckForHeadBob()
    {
        if (myCC.velocity.magnitude > 0.1f)
        {
            isWalking = true;
        }
        else
        {
            isWalking = false;
        }
    }
}
