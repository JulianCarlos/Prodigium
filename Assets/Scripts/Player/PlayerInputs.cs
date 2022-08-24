using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

/// <summary>
/// HUGE IMPROVEMENT NEEDED; FUCKING HELL;;;;
/// </summary>
public class PlayerInputs : Singleton<PlayerInputs>
{
    public PlayerInputAction PlayerInputAction { get; private set; }

    public bool IsRunning { get; private set; }

    public Vector3 MoveInput { get; private set; }

    protected override void Awake()
    {
        base.Awake();

        PlayerInputAction = new PlayerInputAction();
        
        PlayerInputAction.Player.Enable();
        PlayerInputAction.Player.Jump.performed += Jump;
        //playerInputAction.Player.Look.performed += MouseLook;

        PlayerInputAction.Player.Run.started += ToggleRun;
        PlayerInputAction.Player.Run.canceled += ToggleRun;
        PlayerInputAction.Player.Crouch.started += ToggleCrouch;
        PlayerInputAction.Player.Crawl.started += ToggleCrawl;

        PlayerInputAction.Player.OpenInventory.started += OpenInventory;
        PlayerInputAction.Player.PickUp.started += UseOrPickup;
        PlayerInputAction.Player.Reload.started += Reload;
        PlayerInputAction.Player.DropItem.started += DropItem;

        PlayerInputAction.Player.LeftClick.started += LeftClick;
        PlayerInputAction.Player.RightClick.started += RightClick;
    }

    private void Update()
    {
        MoveInput = PlayerInputAction.Player.Movement.ReadValue<Vector2>();
    }

    //Input Actions
    private void MovePlayer(Vector2 moveVector)
    {
        
    }
    public void Jump(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            //velocity.y = Mathf.Sqrt(jumpStrength * -2 * Forces.Gravity.y);
        }
    }

    public void ToggleRun(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            IsRunning = true;
        }
        else if (context.canceled)
        {
            IsRunning= false;
        }
    }
    public void ToggleCrouch(InputAction.CallbackContext context)
    {
        if (context.started)
        {

        }
    }
    public void ToggleCrawl(InputAction.CallbackContext context)
    {
        if (context.started)
        {

        }
    }
    public void OpenInventory(InputAction.CallbackContext context)
    {
        if (context.started)
        {

        }
    }
    public void UseOrPickup(InputAction.CallbackContext context)
    {
        if (context.started)
        {

        }
    }
    public void Reload(InputAction.CallbackContext context)
    {

    }
    public void DropItem(InputAction.CallbackContext context)
    {

    }
    public void LeftClick(InputAction.CallbackContext context)
    {

    }
    public void RightClick(InputAction.CallbackContext context)
    {

    }

    //Script disable/enable
    private void OnEnable()
    {
        PlayerInputAction.Player.Enable();
    }
    private void OnDisable()
    {
        PlayerInputAction.Player.Disable();
    }
}
