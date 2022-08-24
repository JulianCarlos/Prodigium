using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

/// <summary>
/// HUGE IMPROVEMENT NEEDED; FUCKING HELL;;;;
/// </summary>
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

    //Inair Settings
    [Header("In Air Settings")]
    [SerializeField] private float drag;
    [SerializeField] private float inAirMovementDivision;

    //Velocity Settings
    [Header("Movement Vectors - Settings")]
    [SerializeField] private Vector3 velocity;

    //GroundCheck Settings
    [Header("GroundCheck - Settings")]
    [SerializeField] private float checkSphereLength;
    [SerializeField] private float checkSphereRadius;
    [SerializeField] private LayerMask ignoreLayer;

    //Camera look Settings
    [Header("CameraRotation - Settings")]
    [SerializeField] private Transform playerBody;
    [SerializeField] private Camera playerCamera;
    [SerializeField] private float sensitivityX;
    [SerializeField] private float sensitivityY;

    //Only for testing purposes
    [SerializeField] private bool canRotate;

    //Damages
    [Header("Damages")]
    [SerializeField] private float takeDamageTreshold;

    private Player player;
    private PlayerInputAction playerInputAction;
    private CharacterController controller;

    private float forwardWalkSpeed;
    private float sideWardWalkSpeed;

    private float xRotation;

    void Awake()
    {
        MoveAnimator = GetComponent<Animator>();
        player = GetComponent<Player>();
        playerInputAction = new PlayerInputAction();
        controller = GetComponent<CharacterController>();
        playerCamera = GetComponentInChildren<Camera>();
        
        playerInputAction.Player.Enable();
        playerInputAction.Player.Jump.performed += Jump;
        playerInputAction.Player.Look.performed += MouseLook;

        playerInputAction.Player.Run.started += ToggleRun;
        playerInputAction.Player.Run.canceled += ToggleRun;
        playerInputAction.Player.Crouch.started += ToggleCrouch;
        playerInputAction.Player.Crawl.started += ToggleCrawl;

        playerInputAction.Player.OpenInventory.started += OpenInventory;
        playerInputAction.Player.PickUp.started += UseOrPickup;
        playerInputAction.Player.Reload.started += Reload;
        playerInputAction.Player.DropItem.started += DropItem;

        playerInputAction.Player.LeftClick.started += LeftClick;
        playerInputAction.Player.RightClick.started += RightClick;
    }
    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;

        StateMachine = new StateMachine<PlayerInputs>(this);
        //StateMachine.InitializeFirstState(WalkState);

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
        FallDamageCheck();

        if (IsGrounded && !playerInputAction.Player.Jump.WasPressedThisFrame())
        {
            velocity = (transform.forward * moveVector.y) * ((moveVector.y > 0) ? forwardWalkSpeed : sideWardWalkSpeed) + (transform.right * moveVector.x) * sideWardWalkSpeed;
        }
        else
        {
            velocity += Vector3.up * Forces.Gravity.y * Time.deltaTime;
        }

        velocity.x *= drag;
        velocity.z *= drag;

        controller.Move(velocity * Time.deltaTime);
    }
    public void Jump(InputAction.CallbackContext context)
    {
        if (context.performed && IsGrounded)
        {
            velocity.y = Mathf.Sqrt(jumpStrength * -2 * Forces.Gravity.y);
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
            //StateMachine.ChangeState(RunState);
            IsRunning = true;
        }
        else if (context.canceled)
        {
            //StateMachine.ChangeState(WalkState);
            IsRunning= false;
        }
    }
    public void ToggleCrouch(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            //if(StateMachine.CurrentState == CrouchState)
            //{
            //    StateMachine.ChangeState(WalkState);
            //}
            //else
            //{
            //    StateMachine.ChangeState(CrouchState);
            //}
        }
    }
    public void ToggleCrawl(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            //StateMachine.ChangeState(WalkState);
        }
    }
    public void OpenInventory(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            Debug.Log("Open Inventory");
        }
    }
    public void UseOrPickup(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            Debug.Log("Use or Pickup");
        }
    }
    public void Reload(InputAction.CallbackContext context)
    {
        Debug.Log("Reloaded");
    }
    public void DropItem(InputAction.CallbackContext context)
    {
        Debug.Log("Dropped Item");
    }
    public void LeftClick(InputAction.CallbackContext context)
    {
        Debug.Log("Clicked Left");
    }
    public void RightClick(InputAction.CallbackContext context)
    {
        Debug.Log("Clicked Right");
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
    private void FallDamageCheck()
    {
        if (IsGrounded && velocity.y < takeDamageTreshold)
        {
            player.TakeFallDamage(CalculateFallDamage(velocity));
        }
    }

    //Fall Damage
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
