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

    protected override void Awake()
    {
        base.Awake();

        InputAction = new PlayerInputAction();
        InputAction.Player.Enable();
    }

    private void OnEnable()
    {
        InputAction.Player.Enable();
        InputAction.Player.Run.started += ApplyRunButtonInput;
        InputAction.Player.Run.canceled += ApplyRunButtonInput;
    }

    private void OnDisable()
    {
        InputAction.Player.Run.started -= ApplyRunButtonInput;
        InputAction.Player.Run.canceled -= ApplyRunButtonInput;
        InputAction.Player.Disable();
    }

    private void Update()
    {
        MoveInput = InputAction.Player.Movement.ReadValue<Vector2>();
        MouseInput = InputAction.Player.Look.ReadValue<Vector2>();
    }

    public void ApplyRunButtonInput(InputAction.CallbackContext context)
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
