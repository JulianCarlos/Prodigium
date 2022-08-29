using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public PlayerInputs PlayerInputs { get; private set; }

    [SerializeField] private Vector2 sensitivity = Vector2.zero;
    [SerializeField] private Vector2 smoothAmount = Vector2.zero;
    [SerializeField] private Vector2 lookAngleMinMax = Vector2.zero;

    private float yaw;
    private float pitch;

    private float desiredYaw;
    private float desiredPitch;

    private Transform pitchTransform;
    private Camera cam;

    private PlayerInputAction playerInputAction;

    private void Awake()
    {
        playerInputAction = new PlayerInputAction();
        playerInputAction.Enable();
        PlayerInputs = GetComponentInParent<PlayerInputs>();

        GetComponents();
        InitValues();
        ChangeCursorState();
    }

    private void LateUpdate()
    {
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
        desiredYaw += PlayerInputs.MouseInput.x * sensitivity.x * Time.deltaTime;
        desiredPitch -= PlayerInputs.MouseInput.y * sensitivity.y * Time.deltaTime;

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

    void ChangeCursorState()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }
}
