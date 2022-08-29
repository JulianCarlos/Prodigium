using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInputs : Singleton<PlayerInputs>
{
    public Vector2 MoveInput { get; private set; }
    public Vector2 MouseInput { get; private set; }

    public bool RunButtonPressed { get; private set; }  

    private PlayerInputAction playerInputAction;
    private PlayerInteraction playerInteraction;
    private FirstPersonController firstPersonController;

    protected override void Awake()
    {
        base.Awake();

        firstPersonController = GetComponent<FirstPersonController>();
        playerInputAction = new PlayerInputAction();
        
        playerInputAction.Player.Enable();
        playerInputAction.Player.Jump.performed += Jump;

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

    private void Update()
    {
        MoveInput = playerInputAction.Player.Movement.ReadValue<Vector2>();
        MouseInput = playerInputAction.Player.Look.ReadValue<Vector2>();
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
            firstPersonController.StateMachine.ChangeState(firstPersonController.RunState);
            RunButtonPressed = true;
        }
        else if (context.canceled)
        {
            firstPersonController.StateMachine.ChangeState(firstPersonController.WalkState);
            RunButtonPressed = false;
        }
    }
    public void ToggleCrouch(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            if (!RunButtonPressed)
            {
                if(firstPersonController.StateMachine.CurrentState == firstPersonController.CrouchState)
                {
                    firstPersonController.StateMachine.ChangeState(firstPersonController.WalkState);
                }
                else
                {
                    firstPersonController.StateMachine.ChangeState(firstPersonController.CrouchState);
                }
            }
        }
    }
    public void ToggleCrawl(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            if (!RunButtonPressed)
            {
                firstPersonController.StateMachine.ChangeState(firstPersonController.CrawlState);
            }
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
        playerInputAction.Player.Enable();
    }
    private void OnDisable()
    {
        playerInputAction.Player.Disable();
    }
}
