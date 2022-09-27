using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInputs : Singleton<PlayerInputs>
{
    public static Vector2 MoveInput { get; private set; }
    public static Vector2 MouseInput { get; private set; }

    public static bool RunButtonPressed { get; private set; }

    public static PlayerInputAction InputAction;

    public static bool IsRunning { get { return MoveInput.y > 0 && RunButtonPressed; } }
    public static bool IsWalking { get { return !RunButtonPressed && (Mathf.Abs(MoveInput.y) > 0 || Mathf.Abs(MoveInput.x) > 0); } }

    public static bool IsCrouching = false;

    public static bool IsCrawling = false;

    protected override void Awake()
    {
        base.Awake();

        InputAction = new PlayerInputAction();
        InputAction.Player.Enable();
    }

    private void OnEnable()
    {
        InputAction.Player.Enable();
        InputAction.Player.Run.started += ToggleRun;
        InputAction.Player.Run.canceled += ToggleRun;
    }

    private void OnDisable()
    {
        InputAction.Player.Run.started -= ToggleRun;
        InputAction.Player.Run.canceled -= ToggleRun;
        InputAction.Player.Disable();
    }

    private void Update()
    {
        MoveInput = InputAction.Player.Movement.ReadValue<Vector2>();
        MouseInput = InputAction.Player.Look.ReadValue<Vector2>();
    }

    public void ToggleRun(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            RunButtonPressed = true;
        }
        else if (context.canceled)
        {
            RunButtonPressed= false;
        }
    }
}
