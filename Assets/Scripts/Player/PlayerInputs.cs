using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInputs : MonoBehaviour
{
    //Component References
    public StateMachine<PlayerInputs> StateMachine { get; private set; }
    public Animator MoveAnimator { get; private set; }

    //State References
    public PlayerCrawlState CrawlState { get; private set; } = new();
    public PlayerCrouchState CrouchState { get; private set; } = new();
    public PlayerWalkState WalkState { get; private set; } = new();
    public PlayerRunState RunState { get; private set; } = new();

    //Properties
    public float ForwardWalkSpeed { get { return forwardWalkSpeed; } set { forwardWalkSpeed = value; } }
    public float SideWalkSpeed { get { return sideWardWalkSpeed; } set { sideWardWalkSpeed = value; } }
    public bool IsRunning { get; private set; } = false;
    public bool IsGrounded { get; private set; } = false;

    //Air Settings
    [Space(10), SerializeField] private float jumpStrength;
    [SerializeField] private float speedAccelerationMultiplier;

    //Inair Settings
    [Space(15), Header("In Air Settings")]
    [SerializeField] private float drag;
    [SerializeField] private float inAirMovementDivision;    

    //Velocity Settings
    [Space(15), Header("Movement Vectors - Settings")]
    [SerializeField] private Vector3 velocity;

    [SerializeField] private float currentForwardSpeed;
    [SerializeField] private float currentSideWardSpeed;

    [SerializeField] private bool instantGroundResponse = false;
    private bool isFalling;

    //GroundCheck Settings
    [Space(15), Header("GroundCheck - Settings")]
    [SerializeField] private float checkSphereLength;
    [SerializeField] private float checkSphereRadius;
    [SerializeField] private LayerMask ignoreLayer;

    //Camera look Settings
    [Space(15), Header("CameraRotation - Settings")]
    [SerializeField] private Transform playerBody;
    [SerializeField] private Camera playerCamera;
    [SerializeField] private float sensitivityX;
    [SerializeField] private float sensitivityY;

    //Only for testing purposes
    [SerializeField] private bool canRotate;

    //Damages
    [Space(15), Header("Damages")]
    [SerializeField] private float takeDamageTreshold;

    private Player player;
    private CharacterController controller;
    private PlayerInputAction playerInputAction;

    private Vector3 currentVelocity;

    private float forwardWalkSpeed;
    private float sideWardWalkSpeed;

    private float xRotation;

    void Awake()
    {
        MoveAnimator = GetComponent<Animator>();
        player = GetComponent<Player>();
        controller = GetComponent<CharacterController>();
        playerInputAction = new PlayerInputAction();
        playerCamera = GetComponentInChildren<Camera>();
        
        playerInputAction.Player.Enable();
        playerInputAction.Player.Jump.performed += Jump;
        playerInputAction.Player.Look.performed += MouseLook;

        playerInputAction.Player.Run.started += ToggleRun;
        playerInputAction.Player.Run.canceled += ToggleRun;
        playerInputAction.Player.Crouch.started += ToggleCrouch;
        playerInputAction.Player.Crawl.started += ToggleCrawl;
    }
    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;

        StateMachine = new StateMachine<PlayerInputs>(this);
        StateMachine.InitializeFirstState(WalkState);

        sensitivityY = PlayerPrefs.GetFloat("sensY");
        sensitivityX = PlayerPrefs.GetFloat("sensX");
    }
    void Update()
    {
        GroundCheck();
        MovePlayer(playerInputAction.Player.Movement.ReadValue<Vector2>());
    }

    //Input Actions
    private void MovePlayer(Vector2 moveVector)
    {
        if (IsGrounded && !playerInputAction.Player.Jump.WasPressedThisFrame())
        {
            //Bool if player should instantly react when standing on ground
            if (instantGroundResponse)
            {
                currentForwardSpeed = moveVector.y * ((moveVector.y > 0) ? forwardWalkSpeed : sideWardWalkSpeed);
                currentSideWardSpeed = moveVector.x * sideWardWalkSpeed;

                currentVelocity = transform.forward * currentForwardSpeed + transform.right * currentSideWardSpeed;

                velocity = new Vector3(currentVelocity.x, velocity.y, currentVelocity.z);
            }
            else
            {
                //if run button is pressed, forward movement is faster then side movement
                currentForwardSpeed = Mathf.MoveTowards(currentForwardSpeed, moveVector.y * ((moveVector.y > 0)?forwardWalkSpeed : sideWardWalkSpeed), speedAccelerationMultiplier * Time.deltaTime);
                currentSideWardSpeed = Mathf.MoveTowards(currentSideWardSpeed, moveVector.x * sideWardWalkSpeed, speedAccelerationMultiplier * Time.deltaTime);

                currentVelocity = transform.forward * currentForwardSpeed + transform.right * currentSideWardSpeed;

                velocity = new Vector3(currentVelocity.x, velocity.y, currentVelocity.z);
            }
        }
        else
        {
            //Keeps the velocity in air
            velocity.x *= drag;
            velocity.z *= drag;
            ApplyGravity();
        }

        MoveAnimator.SetFloat("forwardWalkSpeed", currentForwardSpeed);
        MoveAnimator.SetFloat("sideWalkSpeed", currentSideWardSpeed);

        controller.Move(velocity * Time.deltaTime);
    }
    public void Jump(InputAction.CallbackContext context)
    {
        if (context.performed && IsGrounded)
        {
            velocity.y = Mathf.Sqrt(jumpStrength * -2 * Forces.Gravity.y);
            StartCoroutine(FallDamageCheck());
        }
    }
    public void MouseLook(InputAction.CallbackContext context)
    {
        if (canRotate)
        {
            Vector2 input = context.ReadValue<Vector2>();

            float mouseX = input.x / 100 * sensitivityX;
            float mouseY = input.y / 100 * sensitivityY;

            xRotation -= mouseY;
            xRotation = Mathf.Clamp(xRotation, -90f, 90f);

            playerCamera.transform.localRotation = Quaternion.Euler(xRotation, 0, 0);
            playerBody.Rotate(Vector3.up * mouseX);
        }
    }
    public void ToggleRun(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            StateMachine.ChangeState(RunState);
            IsRunning = true;
        }
        else if (context.canceled)
        {
            StateMachine.ChangeState(WalkState);
            IsRunning= false;
        }
    }
    public void ToggleCrouch(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            if(StateMachine.CurrentState == CrouchState)
            {
                StateMachine.ChangeState(WalkState);
            }
            else
            {
                StateMachine.ChangeState(CrouchState);
            }
        }
    }
    public void ToggleCrawl(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            StateMachine.ChangeState(CrawlState);
        }
    }

    //Applying and Resetting Gravity, depending on if Player is grounded
    private void ApplyGravity()
    {
        velocity += Forces.Gravity * Time.deltaTime;
    }

    //Changes
    public void ChangeMovementSpeed(float forwardSpeed, float sideWalkSpeed)
    {
        ForwardWalkSpeed = forwardSpeed;
        SideWalkSpeed = sideWalkSpeed;
    }

    //Checks
    private void GroundCheck()
    {
        IsGrounded = Physics.CheckSphere(transform.position - (transform.up * checkSphereLength), checkSphereRadius, 3);
    }

    //Fall Damage
    private IEnumerator FallDamageCheck()
    {
        yield return null;
        yield return new WaitWhile(() => !IsGrounded);
        player.TakeFallDamage(CalculateFallDamage(velocity));
        yield return null;
        velocity.y = 0;
    }
    private float CalculateFallDamage(Vector3 velocity)
    {
        var damage = (velocity.y * -1) * 3;
        return damage;
    }

    //Gizmos
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position - (transform.up * checkSphereLength), checkSphereRadius);

        Gizmos.color = Color.blue;
        Gizmos.DrawLine(transform.position, transform.position + (velocity.normalized * 5));
    }

    //Script disable/enable
    private void OnEnable()
    {
        playerInputAction.Player.Enable();
    }
    private void OnDisable()
    {
        playerInputAction.Player.Disable();
    }
}
