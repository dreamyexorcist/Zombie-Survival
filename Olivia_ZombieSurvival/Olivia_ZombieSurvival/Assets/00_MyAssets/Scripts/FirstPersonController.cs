using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FirstPersonController : MonoBehaviour
{
    //public property to determine if payer is in control of character
    public bool CanMove { get; private set; } = true;

    //is only true if canSprint is true and sprint key is pressed. (instead of 'double if check' a property is used for cleanliness of code)
    private bool IsSprinting => canSprint && Input.GetKey(sprintKey);

    //is checked every frame. If key is pressed (true) and charcter is grounded, jumping is activated. 
    private bool ShouldJump => Input.GetKeyDown(jumpKey) && characterController.isGrounded;

    //if pressed (true) and not mid crouch or mid stand, and if player is grounded, crouching is activated.
    private bool ShouldCrouch => Input.GetKeyDown(crouchKey) && !duringCrouchAnimation && characterController.isGrounded;

    [Header("Functional Options")] //header organises inspector
    [SerializeField] private bool canSprint = true;
    [SerializeField] private bool canJump = true;
    [SerializeField] private bool canCrouch = true;

    [Header("Controls")]
    [SerializeField] private KeyCode sprintKey = KeyCode.LeftShift; //'keycode' gives dropdown menu in inspector
    [SerializeField] private KeyCode jumpKey = KeyCode.Space;
    [SerializeField] private KeyCode crouchKey = KeyCode.LeftControl;

    [Header("Movement Parameters")]
    [SerializeField] private float walkSpeed = 3.0f;
    [SerializeField] private float sprintSpeed = 6.0f;
       
    [Header("Look Parameters")]
    [SerializeField, Range(1, 10)] private float lookSpeedX = 2.0f; //, adds 2nd attribute, edtitable inside inspector
    [SerializeField, Range(1, 10)] private float lookSpeedY = 2.0f;
    [SerializeField, Range(1, 180)] private float upperLookLimit = 80.0f; //Player can look 80 degrees UP before cam stops moving
    [SerializeField, Range(1, 180)] private float lowerLookLimit = 80.0f;

    [Header("Jump Parameters")]
    [SerializeField] private float jumpForce = 8.0f;
    [SerializeField] private float gravity = 30.0f;

    [Header("Crouch Parameters")]
    [SerializeField] private float crouchHeight = 0.5f;
    [SerializeField] private float standHeight = 2f;
    [SerializeField] private float timeToCrouch = 0.25f;
    [SerializeField] private Vector3 crouchingCenter = new Vector3(0, 0.5f, 0);
    [SerializeField] private Vector3 standingCenter = new Vector3(0, 0, 0);
    private bool isCrouching;
    private bool duringCrouchAnimation;


    private Camera playerCamera; //reference to camera
    private CharacterController characterController; //reference to character controller attached to player game object so player can move

    private Vector3 moveDirection; //final move amount applied to character controller
    private Vector2 currentInput; //value given to controller via keyboard input (w,a,s,d keys)

    private float rotationX = 0; //angle that is clamped with upper and lower look limit

    void Awake() //cache components
    {
        playerCamera = GetComponentInChildren<Camera>(); //child object of fps controller. (could be serialized and dragged into component instead)
        characterController = GetComponent<CharacterController>(); //is attached to parent object, component grabs character controller.
        Cursor.lockState = CursorLockMode.Locked; //locks cursor within game window
        Cursor.visible = false; //hides cursor
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (CanMove)
        {
            HandleMovementInput();
            HandleMouseLook();

            //only runs when set to true
            if (canJump)
                HandleJump();

            if (canCrouch)
                HandleCrouch();

            ApplyFinalMovement();
        }
    }

    private void HandleMovementInput()
    {
        currentInput = new Vector2((IsSprinting ? sprintSpeed : walkSpeed) * Input.GetAxis("Vertical"), (IsSprinting ? sprintSpeed : walkSpeed) * Input.GetAxis("Horizontal")); //ternary operator ? : (shortened if else condition)
        //left & right movement. calculates direction on Vector 2 of player input multiplied by the walking speed

        float moveDirectionY = moveDirection.y;
        //cache movement temporaily on y-axis so actual vertical going upwards away from floor, or towards floor.
        //If Vector3 updates, y position needs to be reset to its original value.

        moveDirection = (transform.TransformDirection(Vector3.forward) * currentInput.x) + (transform.TransformDirection(Vector3.right) * currentInput.y);
        //Calculates move direction based on characters orientation
        //forward axis multiplied by current x plus sideways axis multiplied by current input y
        //Vector3 inside move direction which adheares to current players orientation.
        moveDirection.y = moveDirectionY;
    }

    private void HandleMouseLook()
    {
        //rotation based on mouse input (mouse value controls x rotation of camera)
        //determines y axis on mouse which is up and down value
        rotationX -= Input.GetAxis("Mouse Y") * lookSpeedY;

        //clamp this rotation using upper and lower lock limits (stop 360 degree rotation)
        //Clamp conists of 3 values: rotation on x, minimum value it should be (-80), max value (80). player can look 80 degrees up and down.
        rotationX = Mathf.Clamp(rotationX, -upperLookLimit, lowerLookLimit);
        //apply rotation to camera
        playerCamera.transform.localRotation = Quaternion.Euler(rotationX, 0, 0);
        //rotates gameobject instead of camera. 
        transform.rotation *= Quaternion.Euler(0, Input.GetAxis("Mouse X") * lookSpeedX, 0);

    }

    private void HandleJump()
    {
        if (ShouldJump)
            moveDirection.y = jumpForce; //applies jump force to y movement
    }

    private void HandleCrouch()
    {
        if (ShouldCrouch)
            StartCoroutine(CrouchStand()); //coroutine performs both actions for crouching and standing 
    }

    private void ApplyFinalMovement()
    {
        if (!characterController.isGrounded)
            moveDirection.y -= gravity * Time.deltaTime; //applies gravity if player is not grounded

        characterController.Move(moveDirection * Time.deltaTime); 
    }

    private IEnumerator CrouchStand() //lerps from 1 value to another over a set amount of time (from stand height to center point and back)
    {
        duringCrouchAnimation = true; //tells character controller crouch animation is happening.

        float timeElapsed = 0;
        float targetHeight = isCrouching ? standHeight : crouchHeight;
        float currentHeight = characterController.height;
        Vector3 targetCenter = isCrouching ? standingCenter : crouchingCenter;
        Vector3 currentCenter = characterController.center;

        while (timeElapsed < timeToCrouch)
        {
            characterController.height = Mathf.Lerp(currentHeight, targetHeight, timeElapsed);
            characterController.center = Vector3.Lerp(currentCenter, targetCenter, timeElapsed);
            yield return null;
        }

        characterController.height = targetHeight;
        characterController.center = targetCenter;

        isCrouching = !isCrouching;

        duringCrouchAnimation = false; //tells character controller crouch animation has ended.
    }
}
