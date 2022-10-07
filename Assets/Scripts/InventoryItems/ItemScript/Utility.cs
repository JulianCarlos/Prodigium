using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Animator))]
public abstract class Utility : Item
{
    protected void OnEnable()
    {
        PlayerInputs.InputAction.Player.LeftClick.started += L_Use;
        PlayerInputs.InputAction.Player.LeftClick.canceled += L_Use;

        PlayerInputs.InputAction.Player.RightClick.started += R_Use;
        PlayerInputs.InputAction.Player.RightClick.canceled += R_Use;
    }

    protected void OnDisable()
    {
        PlayerInputs.InputAction.Player.LeftClick.started -= L_Use;
        PlayerInputs.InputAction.Player.LeftClick.canceled -= L_Use;

        PlayerInputs.InputAction.Player.RightClick.started -= R_Use;
        PlayerInputs.InputAction.Player.RightClick.canceled -= R_Use;
    }

    protected override void L_Use(InputAction.CallbackContext context)
    {

    }

    protected override void R_Use(InputAction.CallbackContext context)
    {

    }
}
