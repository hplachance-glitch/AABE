using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerSlide : MonoBehaviour //AI CODE
{
    private CharacterController myCC;
    private PlayerInput playerInput;
    private PlayerMove playerMove;

    [Header("Slide Physics")]
    public float slideForce = 30f;
    public float slideDuration = 0.5f;
    public float slideDecay = 5f;
    public float minSlideTime = 0.3f; // Editable minimum time before the slide can be canceled

    [Header("Crouch Settings")]
    public float normalHeight = 2f;
    public float slideHeight = 1f;

    private bool isSliding = false;
    private float slideTimer;
    private Vector3 slideDirection;
    private float currentSlideSpeed;
    
    // Variables to track and adjust speed
    private float originalSpeed;
    private bool speedHalved = false;

    // Variables to track slide duration and input release
    private float timeInSlide = 0f;
    private bool wantsToStandUp = false;

    void Start()
    {
        myCC = GetComponent<CharacterController>();
        playerInput = GetComponent<PlayerInput>();
        playerMove = GetComponent<PlayerMove>();
    }

    void Update()
    {
        HandleSlideInput();

        if (isSliding)
        {
            // Track total time spent in the current slide
            timeInSlide += Time.deltaTime;
            
            PerformSlide();

            // Only end the slide if the button is released AND the minimum time has passed
            if (wantsToStandUp && timeInSlide >= minSlideTime)
            {
                if (!Physics.Raycast(transform.position, Vector3.up, out RaycastHit hit, 5f))
                {
                    EndSlide();
                }
            }
        }
        
    }

    void HandleSlideInput()
    {
        if (playerInput.actions["Slide"].WasPressedThisFrame() && !isSliding)
        {
            StartSlide();
        }

        if (playerInput.actions["Slide"].WasReleasedThisFrame() && isSliding)
        {
            // Flag that the player let go, instead of instantly ending the slide
            wantsToStandUp = true;
        }
    }

    void StartSlide()
    {
        isSliding = true;
        wantsToStandUp = false; // Reset the release flag
        timeInSlide = 0f;       // Reset the duration tracker

        slideTimer = slideDuration;
        currentSlideSpeed = slideForce;
        speedHalved = false;

        originalSpeed = playerMove.playerSpeed;

        Vector2 input = playerInput.actions["Move"].ReadValue<Vector2>();
        Vector3 inputVector = new Vector3(input.x, 0f, input.y);

        if (inputVector.magnitude < 0.1f)
        {
            slideDirection = transform.forward;
        }
        else
        {
            inputVector.Normalize();
            slideDirection = transform.TransformDirection(inputVector);
        }

        myCC.height = slideHeight;
    }

    void PerformSlide()
    {
        if (slideTimer > 0f && currentSlideSpeed >= 1f)
        {
            slideTimer -= Time.deltaTime;
            currentSlideSpeed -= currentSlideSpeed * slideDecay * Time.deltaTime;
            myCC.Move(slideDirection * currentSlideSpeed * Time.deltaTime);
        }
        else if (!speedHalved)
        {
            playerMove.playerSpeed = originalSpeed / 2f;
            speedHalved = true;
        }
    }

    void EndSlide()
    {
        isSliding = false;
        wantsToStandUp = false; // Ensure flag is cleared
        
        myCC.height = normalHeight;
        playerMove.playerSpeed = originalSpeed;
    }
}