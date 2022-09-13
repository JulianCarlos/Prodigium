using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class CameraController : MonoBehaviour
{
    [SerializeField] private Player player;
    [SerializeField] private PlayerInputs playerInputs;

    [SerializeField] private Vector2 sensitivity = Vector2.zero;
    [SerializeField] private Vector2 smoothAmount = Vector2.zero;
    [SerializeField] private Vector2 lookAngleMinMax = Vector2.zero;

    private float yaw;
    private float pitch;

    private float desiredYaw;
    private float desiredPitch;

    private Transform pitchTransform;
    private Camera cam;

    private float cameraToBodyDistance;

    [SerializeField] private float targetFOW;
    [SerializeField] private float zoomSpeed;

    private void Awake()
    {
        cameraToBodyDistance = transform.position.y - player.gameObject.transform.position.y;
        cam = GetComponentInChildren<Camera>();

        GetComponents();
        InitValues();

        Cursor.lockState = CursorLockMode.Locked;
    }

    private void Update()
    {
        transform.position = new Vector3(player.transform.position.x, player.transform.position.y + cameraToBodyDistance, player.transform.position.z);

        ChangeFOW();

        CalculateRotation();
        SmoothRotation();
        ApplyRotation();
    }

    void GetComponents()
    {
        pitchTransform = transform.GetChild(0).transform;
        cam = GetComponentInChildren<Camera>();
    }

    void InitValues()
    {
        yaw = transform.eulerAngles.y;
        desiredYaw = yaw;
    }

    void CalculateRotation()
    {
        if (PlayerState.PlayerStateType == PlayerStateType.InMenu)
            return;

        desiredYaw += playerInputs.MouseInput.x * sensitivity.x * Time.deltaTime;
        desiredPitch -= playerInputs.MouseInput.y * sensitivity.y * Time.deltaTime;

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
        cam.fieldOfView = Mathf.MoveTowards(cam.fieldOfView, targetFOW, zoomSpeed * Time.deltaTime);
    }

    public void ApplyFOWValue(float target)
    {
        targetFOW = target;
    }
    // Test Only
    public void OnReload()
    {
        Debug.Log("Reload pressed");
    }
}
