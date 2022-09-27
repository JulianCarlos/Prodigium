using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class CameraController : MonoBehaviour
{
    [Header("Player Settings")]
    [SerializeField] private Player player;
    [SerializeField] private Camera playerCamera;
    [SerializeField] private Transform cameraPivot;
    [SerializeField] private FirstPersonController controller;
    [SerializeField] private PlayerInputs playerInputs;

    [Header("Sensitivity Settings")]
    [SerializeField] private Vector2 sensitivity = Vector2.zero;
    [SerializeField] private Vector2 smoothAmount = Vector2.zero;
    [SerializeField] private Vector2 lookAngleMinMax = Vector2.zero;

    [Header("Camera Settings")]
    [SerializeField] private float targetFOW;
    [SerializeField] private float zoomSpeed;

    [Header("Headbob Settings")]
    [SerializeField] private float walkBobSpeed = 14f;
    [SerializeField] private float walkBobAmount = 0.05f;
    [SerializeField] private float sprintBobSpeed = 18f;
    [SerializeField] private float sprintBobAmount = 0.1f;
    [SerializeField] private float crouchBobSpeed = 8f;
    [SerializeField] private float crouchBobAmount = 0.025f;
    [SerializeField] private bool canUseHeadBob;
    private float defaultYPos = 0;
    private float timer;

    private float yaw;
    private float pitch;

    private float desiredYaw;
    private float desiredPitch;

    private Transform pitchTransform;

    private float cameraToBodyDistance;


    private void Awake()
    {
        cameraToBodyDistance = transform.position.y - player.gameObject.transform.position.y;
        
        sensitivity.x = PlayerPrefs.GetFloat("sensX");
        sensitivity.y = PlayerPrefs.GetFloat("sensY");

        GetComponents();
        InitValues();

        Cursor.lockState = CursorLockMode.Locked;
    }

    private void Start()
    {
        Actions.OnSensitivityChanged += UpdateSensValues;
    }

    private void Update()
    {
        transform.position = new Vector3(player.transform.position.x, player.transform.position.y + cameraToBodyDistance, player.transform.position.z);

        ChangeFOW();

        CalculateRotation();
        SmoothRotation();

        if (canUseHeadBob)
            HandleHeadBob();

        ApplyRotation();
    }

    private void HandleHeadBob()
    {
        if (!controller.IsGrounded)
            return;

        if (Mathf.Abs(PlayerInputs.MoveInput.x) > 0.1f || Mathf.Abs(PlayerInputs.MoveInput.y) > 0.1f)
        {
            timer += Time.deltaTime * (PlayerInputs.IsCrouching ? crouchBobSpeed : PlayerInputs.IsRunning ? sprintBobSpeed : walkBobSpeed);

            var sinValue = Mathf.Sin(timer);

            cameraPivot.transform.localPosition = new Vector3(
                cameraPivot.transform.localPosition.x,
                defaultYPos + sinValue * (PlayerInputs.IsCrouching ? crouchBobAmount : PlayerInputs.IsRunning ? sprintBobAmount : walkBobAmount),
                cameraPivot.transform.localPosition.z);
        }
    }

    private void UpdateSensValues(Vector2 sensitivityValue)
    {
        sensitivity.x = sensitivityValue.x;
        sensitivity.y = sensitivityValue.y;
    }

    void GetComponents()
    {
        pitchTransform = transform.GetChild(0).transform;
        playerCamera = GetComponentInChildren<Camera>();
    }

    void InitValues()
    {
        yaw = transform.eulerAngles.y;
        desiredYaw = yaw;
        defaultYPos = cameraPivot.transform.localPosition.y;
    }

    void CalculateRotation()
    {
        if (PlayerState.PlayerStateType == PlayerStateType.InMenu)
            return;

        desiredYaw += (PlayerInputs.MouseInput.x / 3) * sensitivity.x * Time.deltaTime;
        desiredPitch -= (PlayerInputs.MouseInput.y / 3) * sensitivity.y * Time.deltaTime;

        desiredPitch = Mathf.Clamp(desiredPitch, lookAngleMinMax.x, lookAngleMinMax.y);
    }

    void SmoothRotation()
    {
        yaw = Mathf.Lerp(yaw, desiredYaw, smoothAmount.x * Time.deltaTime);
        pitch = Mathf.Lerp(pitch, desiredPitch, smoothAmount.y * Time.deltaTime);
    }

    void ApplyRotation()
    {
        transform.eulerAngles = new Vector3(0f, yaw, 0f);
        pitchTransform.localEulerAngles = new Vector3(pitch, 0f, 0f);
    }

    private void ChangeFOW()
    {
        playerCamera.fieldOfView = Mathf.MoveTowards(playerCamera.fieldOfView, targetFOW, zoomSpeed * Time.deltaTime);
    }

    public void ApplyFOWValue(float target)
    {
        targetFOW = target;
    }
}
