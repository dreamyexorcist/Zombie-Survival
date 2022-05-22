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
    [SerializeField] private bool canUseHeadbob = true;
    [SerializeField] private bool WillSlideOnSlopes = true;
    [SerializeField] private bool canZoom = true;
    [SerializeField] private bool canInteract = true;

    [Header("Controls")]
    [SerializeField] private KeyCode sprintKey = KeyCode.LeftShift; //'keycode' gives dropdown menu in inspector
    [SerializeField] private KeyCode jumpKey = KeyCode.Space;
    [SerializeField] private KeyCode crouchKey = KeyCode.LeftControl;
    [SerializeField] private KeyCode zoomKey = KeyCode.Mouse1;
    [SerializeField] private KeyCode interactKey = KeyCode.E;

    [Header("Movement Parameters")]
    [SerializeField] private float walkSpeed = 3.0f;
    [SerializeField] private float sprintSpeed = 6.0f;
    [SerializeField] private float crouchSpeed = 1.5f;
    [SerializeField] private float slopeSpeed = 8f;

    [Header("Look Parameters")]
    [SerializeField, Range(1, 10)] private float lookSpeedX = 2.0f; //, adds 2nd attribute, edtitable inside inspector
    [SerializeField, Range(1, 10)] private float lookSpeedY = 2.0f;
    [SerializeField, Range(1, 180)] private float upperLookLimit = 80.0f; //Player can look 80 degrees UP before cam stops moving
    [SerializeField, Range(1, 180)] private float lowerLookLimit = 80.0f;

    [Header("Jump Parameters")]
    [SerializeField] private float jumpForce = 8.0f;
    [SerializeField] private float gravity = 30.0f;

    [Header("Crouch Parameters")]
    [SerializeField] private float crouchingHeight = 0.5f;
    [SerializeField] private float standingHeight = 2f;
    [SerializeField] private float timeToCrouch = 0.25f;
    [SerializeField] private Vector3 crouchingCenter = new Vector3(0, 0.5f, 0);
    [SerializeField] private Vector3 standingCenter = new Vector3(0, 0, 0);
    private bool isCrouching;
    private bool duringCrouchAnimation;

    [Header("Headbob Parameters")]
    [SerializeField] private float walkBobSpeed = 14f;
    [SerializeField] private float walkBobAmount = 0.05f;
    [SerializeField] private float sprintBobSpeed = 18f;
    [SerializeField] private float sprintBobAmount = 0.1f;
    [SerializeField] private float crouchBobSpeed = 8f;
    [SerializeField] private float crouchBobAmount = 0.025f;
    private float defaultYPos = 0;
    private float timer;

    [Header("Zoom Parameters")]
    [SerializeField] private float timeToZoom = 0.3f;
    [SerializeField] private float zoomFOV = 30f;
    private float defaultFOV;
    private Coroutine zoomRoutine;

    //SLIDING PARAMTETERS
    private Vector3 hitPointNormal; //angle of the floor

    private bool IsSliding
    {
        get //determine if player should be sliding
        {
            Debug.DrawRay(transform.position, Vector3.down, Color.red);
            //get data of the floor player is standing on via (downward) raycast, from player transform/centerpoint of object.
            if (characterController.isGrounded && Physics.Raycast(transform.position, Vector3.down, out RaycastHit slopeHit, 22f))
            {
                hitPointNormal = slopeHit.normal; //gets angle value of floor player is currently standing on.
                return Vector3.Angle(hitPointNormal, Vector3.up) > characterController.slopeLimit; //degree angle between direct vertical and current floor player is standing on.
                                                                                                   //check if value is greater then the set slope limit of character controlers (in inspector).
            }
            else
            {
               return false;
            }
        }
    }

    [Header("Interaction")]
    [SerializeField] private Vector3 interactionRayPoint = default;
    [SerializeField] private float interactionDistance = default;
    [SerializeField] private LayerMask interactionLayer = default;
    private Interactable currentInteractable;

    private Camera playerCamera; //reference to camera
    private CharacterController characterController; //reference to character controller attached to player game object so player can move

    private Vector3 moveDirection; //final move amount applied to character controller
    private Vector2 currentInput; //value given to controller via keyboard input (w,a,s,d keys)

    private float rotationX = 0; //angle that is clamped with upper and lower look limit

    void Awake() //cache components on awake
    {
        playerCamera = GetComponentInChildren<Camera>(); //child object of fps controller. (could be serialized and dragged into component instead)
        characterController = GetComponent<CharacterController>(); //is attached to parent object, component grabs character controller.
        defaultYPos = playerCamera.transform.localPosition.y; //gets cameras defult y position.
        defaultFOV = playerCamera.fieldOfView; //default view set in inspector
        Cursor.lockState = CursorLockMode.Locked; //locks cursor within game window.
        Cursor.visible = false; //hides cursor
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

            if (canUseHeadbob)
                HandleHeadbob();

            if (canZoom)
                HandleZoom();

            if (canInteract)
                HandleInteractionCheck();
                HandleInteractionInput();

            ApplyFinalMovement();
        }
    }

    private void HandleMovementInput()
    {
        //checking crouch before sprint prevents sprinting while crouching.
        currentInput = new Vector2((isCrouching ? crouchSpeed : IsSprinting ? sprintSpeed : walkSpeed) * Input.GetAxis("Vertical"), (isCrouching ? crouchSpeed : IsSprinting ? sprintSpeed : walkSpeed) * Input.GetAxis("Horizontal")); //ternary operator ? : (shortened if else condition)
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

    private void HandleHeadbob()
    {
        if (!characterController.isGrounded) return; //if grounnded no/end headbob

        //Abs = absolute value of float, a positive vlue regardless if positive or not. If float is 1 or -1 mathf.Abs gives 1.
        //Done because movement directions are either positive or negative and the direction of the player faces is not required
        //Since only a value greater than 'player stands still' (>0.1) is needed.
        if (Mathf.Abs(moveDirection.x) > 0.1f || Mathf.Abs(moveDirection.z) > 0.1f)
        {
            timer += Time.deltaTime * (isCrouching ? crouchBobSpeed : IsSprinting ? sprintBobSpeed : walkBobSpeed); //determine player movement so changes can be made.
            //control position of players camera to match expected position.
            //Mathf.Sin gives sin angle of a float, is a value of -1/1. If sin value is in the negative = camera lowers, if positive = camera raises.
            //(timer) determines speed of type of movement (walk, sprint, crouch)
            playerCamera.transform.localPosition = new Vector3(
                playerCamera.transform.localPosition.x,
                defaultYPos + Mathf.Sin(timer) * (isCrouching ? crouchBobAmount : IsSprinting ? sprintBobAmount : walkBobAmount),
                playerCamera.transform.localPosition.z);
        }
    }

    private void HandleZoom()
    {
        if (Input.GetKeyDown(zoomKey)) //if mouse 1 button is pressed
        {
            if (zoomRoutine != null) //check if zoomRoutine is not equal to null. if not equal to null routine is already mid zoom.
            {
                StopCoroutine(zoomRoutine); //then stop zoom routine and set to null.
                zoomRoutine = null;
            }

            zoomRoutine = StartCoroutine(ToggleZoom(true)); //enter zoom state
        }

        if (Input.GetKeyUp(zoomKey)) //if mouse 1 (right mouse button) is released.
        {
            if (zoomRoutine != null) 
            {
                StopCoroutine(zoomRoutine); 
                zoomRoutine = null;
            }

            zoomRoutine = StartCoroutine(ToggleZoom(false)); //zoom back out
        }
    }

    private void HandleInteractionCheck() //constant raycast out to check and look for interactable objects.
    {

    }

    private void HandleInteractionInput() //perfomr action is intercation key is pressed.
    {
        //if (Input.GetKeyDown(interactKey) && currentInteractable != null && Physics.Raycast(playerCamera.ViewportPointToRay(interactionRayPoint)
    }

    private void ApplyFinalMovement()
    {
        if (!characterController.isGrounded)
            moveDirection.y -= gravity * Time.deltaTime; //applies gravity if player is not grounded

        //override normal move direction with hitPointNormal direction
        if (WillSlideOnSlopes && IsSliding) //if player should slide and is sliding then move direction is new vector 3 (slides down / - hitPointNormal.y)
            moveDirection += new Vector3(hitPointNormal.x, -hitPointNormal.y, hitPointNormal.z) * slopeSpeed;

        characterController.Move(moveDirection * Time.deltaTime); 
    }

    private IEnumerator CrouchStand() //lerps from 1 value to another over a set amount of time (from stand height to center point and back)
    {
        //check if anything is above (raycast from camera up) player within 1 unit. Prevents player clipping through objects.
        if (isCrouching && Physics.Raycast(playerCamera.transform.position, Vector3.up, 1f))
            yield break; //if yes then coroutine ends and player stays in crouched postion.

        duringCrouchAnimation = true; //tells character controller crouch animation is happening.
        //parameters. set up where the character controller is going from and to and for how long.
        //once the lerp begins, values changes constantly so character.controller. height can't be used.
        float timeElapsed = 0; 
        float targetHeight = isCrouching ? standingHeight : crouchingHeight; // target height is standing height, else crouching height
        float currentHeight = characterController.height; //reference to current height
        Vector3 targetCenter = isCrouching ? standingCenter : crouchingCenter; 
        Vector3 currentCenter = characterController.center;

        while (timeElapsed < timeToCrouch)
        {
            characterController.height = Mathf.Lerp(currentHeight, targetHeight, timeElapsed/timeToCrouch); //math.f uses floats
            characterController.center = Vector3.Lerp(currentCenter, targetCenter, timeElapsed/timeToCrouch); //uses 3 vector points
            timeElapsed += Time.deltaTime; //increment time elapsed by actual time the frame has taken.
            yield return null; //wait until next frame before continuing while statement 
        }
        //time to render frame can vary fractionally, can result in values slighlty below or above target values. check for excat values every time.
        characterController.height = targetHeight;
        characterController.center = targetCenter;

        isCrouching = !isCrouching; //crouching becomes not crouching only when activated.

        duringCrouchAnimation = false; //tells character controller crouch animation has ended.
    }

    private IEnumerator ToggleZoom(bool isEnter) //1 parameter detemines in and out zoom state
    {
        float targetFOV = isEnter ? zoomFOV : defaultFOV; //if isEnter then zoom value, else default.
        float startingFOV = playerCamera.fieldOfView; //reference to starting FOV so coroutine can pickup at any point.
        float timeElapsed = 0;

        while (timeElapsed < timeToZoom)
        {
            playerCamera.fieldOfView = Mathf.Lerp(startingFOV, targetFOV, timeElapsed / timeToZoom);
            timeElapsed += Time.deltaTime;
            yield return null; //wait for next frame
        }

        playerCamera.fieldOfView = targetFOV; //round target value off
        zoomRoutine = null; 

    }
}
