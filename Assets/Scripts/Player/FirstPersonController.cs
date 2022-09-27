using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class FirstPersonController : MonoBehaviour
{
    public StateMachine<FirstPersonController> StateMachine { get; private set; }

    [SerializeField] private PlayerInputs playerInputs;
    [SerializeField] private PlayerInputAction playerInputAction;

    public PlayerCrawlState CrawlState { get; private set; } = new();
    public PlayerCrouchState CrouchState { get; private set; } = new();
    public PlayerWalkState WalkState { get; private set; } = new();
    public PlayerRunState RunState { get; private set; } = new();

    public bool IsGrounded { get; internal set; }

    [Space, Header("Speed Settings")]
    [SerializeField] private float crouchSpeed;
    [SerializeField] private float walkSpeed;
    [SerializeField] private float runSpeed;
    [SerializeField] private float jumpSpeed;

    [SerializeField] private float moveBackwardsSpeedPercent = 0.5f;
    [SerializeField] private float moveSideSpeedPercent = 0.75f;

    [Space, Header("Run Settings")]
    [SerializeField] private float canRunTreshold = 0.8f;
    [SerializeField] private AnimationCurve runTransitionCurve = AnimationCurve.EaseInOut(0f, 0f, 1f, 1f);

    [Space, Header("Crouch Settings")]
    [SerializeField] private float crouchPercent = 0.6f;
    [SerializeField] private float crouchTransitionDuration = 1f;
    [SerializeField] private AnimationCurve crouchTransitionCurve = AnimationCurve.EaseInOut(0f, 0f, 1f, 1f);

    [Space, Header("Landing Settings")]
    [SerializeField] private float lowLandAmount = 0.1f;
    [SerializeField] private float highLandAmount = 0.6f;
    [SerializeField] private float landTimer = 0.5f;
    [SerializeField] private float landDuration = 1f;
    [SerializeField] private AnimationCurve landCurve = AnimationCurve.EaseInOut(0f, 0f, 1f, 1f);

    [Space, Header("Gravity Settings")]
    [SerializeField] private float gravityMultiplier = 2.5f;
    [SerializeField] private float stickToGroundForce = 5f;

    [Space, Header("Raycast Settigns")]
    [SerializeField] private LayerMask groundLayer = ~0;
    [SerializeField] private float rayLength = 0.1f;
    [SerializeField] private float raySphereRadius = 0.1f;

    [Space, Header("Smooth Settings")]
    [SerializeField] private float smoothRotateSpeed = 5f;
    [SerializeField] private float smoothInputSpeed = 5f;
    [SerializeField] private float smoothVelocitySpeed = 5f;
    [SerializeField] private float smoothFinalDirectionSpeed = 5f;
    [SerializeField] private float smoothHeadBobSpeed = 5f;

    #region Special
    private Vector2 inputVector;
    private Vector2 smoothInputVector;

    private Vector3 finalMoveDir;
    private Vector3 smoothFinalMoveDir;

    private Vector3 finalVelocity;

    private float currentSpeed;
    private float smoothCurrentSpeed;
    private float finalSmoothCurrentSpeed;
    private float walkRunSpeedDifference;

    private bool previouslyGrounded;
    #endregion

    private CharacterController characterController;
    private Transform yawTransform;
    private Transform camTransform;
    public CameraController CameraController;

    private RaycastHit hitInfo;
    private IEnumerator landRoutine;

    private void Awake()
    {
        GetComponents();
        InitVariables();

        StateMachine = new StateMachine<FirstPersonController>(this);
        StateMachine.InitializeFirstState(WalkState);
    }

    private void Update()
    {
        if (yawTransform != null)
            RotateTowardsCamera();

        if (characterController)
        {
            CheckIfGrounded();

            SmoothInput();
            SmoothSpeed();
            SmoothDir();

            CalculateMovementDirection();
            CalculateSpeed();
            CalculateFinalMovement();

            HandleLanding();

            ApplyGravity();

            ApplyMovement();

            previouslyGrounded = IsGrounded;
        }
    }

    private void GetComponents()
    {
        playerInputAction = new PlayerInputAction();
        playerInputAction.Player.Enable();
        characterController = GetComponent<CharacterController>();
        yawTransform = CameraController.transform;
        camTransform = CameraController.GetComponentInChildren<Camera>().transform;
    }

    private void InitVariables()
    {
        IsGrounded = true;
        previouslyGrounded = true;

        walkRunSpeedDifference = runSpeed - walkSpeed;
    }

    private void SmoothInput()
    {
        inputVector = PlayerInputs.MoveInput.normalized;
        smoothInputVector = Vector2.Lerp(smoothInputVector, inputVector, Time.deltaTime * smoothInputSpeed);
    }

    private void SmoothSpeed()
    {
        smoothCurrentSpeed = Mathf.Lerp(smoothCurrentSpeed, currentSpeed, Time.deltaTime * smoothVelocitySpeed);

        if (PlayerInputs.RunButtonPressed && CanRun())
        {
            float walkRunPercent = Mathf.InverseLerp(walkSpeed, runSpeed, smoothCurrentSpeed);
            finalSmoothCurrentSpeed = runTransitionCurve.Evaluate(walkRunPercent) * walkRunSpeedDifference + walkSpeed;
        }
        else
        {
            finalSmoothCurrentSpeed = smoothCurrentSpeed;
        }
    }

    private void SmoothDir()
    {
        smoothFinalMoveDir = Vector3.Lerp(smoothFinalMoveDir, finalMoveDir, Time.deltaTime * smoothFinalDirectionSpeed);
    }

    private void CheckIfGrounded()
    {
        IsGrounded = Physics.CheckSphere(transform.position - (transform.up * rayLength), raySphereRadius, 3);
    }

    private bool CanRun()
    {
        Vector3 normalizeDir = Vector3.zero;

        if (smoothFinalMoveDir != Vector3.zero)
            normalizeDir = smoothFinalMoveDir.normalized;

        float dot = Vector3.Dot(transform.forward, normalizeDir);
        return dot >= canRunTreshold && StateMachine.CurrentState != CrouchState ? true : false;
    }

    private void CalculateMovementDirection()
    {
        Vector3 dirY = transform.forward * smoothInputVector.y;
        Vector3 dirX = transform.right * smoothInputVector.x;

        Vector3 desiredDir = dirY + dirX;
        Vector3 flattenDir = FlattenVectorOnSlopes(desiredDir);

        finalMoveDir = flattenDir;
    }

    private Vector3 FlattenVectorOnSlopes(Vector3 vectorToFlat)
    {
        if (IsGrounded)
        {
            vectorToFlat = Vector3.ProjectOnPlane(vectorToFlat, hitInfo.normal);
        }

        return vectorToFlat;
    }

    private void CalculateSpeed()
    {
        currentSpeed = PlayerInputs.IsRunning && CanRun() ? runSpeed : walkSpeed;
        currentSpeed = StateMachine.CurrentState == CrouchState ? crouchSpeed : currentSpeed;
        currentSpeed = PlayerInputs.MoveInput.magnitude == 0 ? 0f : currentSpeed;
        currentSpeed = PlayerInputs.MoveInput.y == -1 ? currentSpeed * moveBackwardsSpeedPercent : currentSpeed;
        currentSpeed = PlayerInputs.MoveInput.x != 0 && PlayerInputs.MoveInput.y == 0 ? currentSpeed * moveSideSpeedPercent : currentSpeed; 
    }

    private void CalculateFinalMovement()
    {
        float smoothInputVectorMagnitude = 1f;
        Vector3 finalVector = smoothFinalMoveDir * finalSmoothCurrentSpeed * smoothInputVectorMagnitude;

        finalVelocity.x = finalVector.x;
        finalVelocity.z = finalVector.z;

        if (IsGrounded)
        {
            finalVelocity.y = finalVector.y;
        }
    }

    private void HandleJump()
    {
        if (IsGrounded && playerInputAction.Player.Jump.IsPressed() && StateMachine.CurrentState != CrouchState)
        {
            finalVelocity.y = Mathf.Sqrt(jumpSpeed * -2 * (Forces.Gravity.y * gravityMultiplier));

            previouslyGrounded = true;
            IsGrounded = false;
        }
    }

    private void HandleLanding()
    {
        if(!previouslyGrounded && IsGrounded)
        {
            FallDamageCheck();
            //InvokeLandingRoutine();
        }
    }

    private void InvokeLandingRoutine()
    {
        if(landRoutine != null)
            StopCoroutine(landRoutine);

        landRoutine = LandingRoutine();
        StartCoroutine(landRoutine);
    }

    private IEnumerator LandingRoutine()
    {
        float percent = 0f;
        float landAmount = 0f;

        float speed = 1f / landDuration;

        Vector3 originalPos = yawTransform.localPosition;
        Vector3 localPos = originalPos;
        float initLandHeight = localPos.y;

        landAmount = highLandAmount;

        while (percent <= 1f)
        {
            percent += Time.deltaTime * speed;
            float desiredY = landCurve.Evaluate(percent) * 15;

            localPos.y = initLandHeight + desiredY;
            yawTransform.localPosition = localPos;

            yield return null;
        }

        yawTransform.localPosition = originalPos;
    }

    private void FallDamageCheck()
    {
        
    }

    private void ApplyGravity()
    {
        if (IsGrounded)
        {
            finalVelocity.y = -stickToGroundForce;

            HandleJump();
        }
        else
        {
            finalVelocity += Forces.Gravity * gravityMultiplier * Time.deltaTime;
        }
    }

    private void ApplyMovement()
    {
        characterController.Move(finalVelocity * Time.deltaTime);
    }

    private void RotateTowardsCamera()
    {
        Quaternion currentRot = transform.rotation;
        Quaternion desiredRot = yawTransform.rotation;

        transform.rotation = Quaternion.Slerp(currentRot, desiredRot, Time.deltaTime * smoothRotateSpeed);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position + (-transform.up * rayLength), raySphereRadius);
    }
}
