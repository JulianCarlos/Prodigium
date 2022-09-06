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

    public PlayerInputAction PlayerInputAction;
    private PlayerInteraction playerInteraction;
    private FirstPersonController firstPersonController;

    protected override void Awake()
    {
        base.Awake();

        firstPersonController = GetComponent<FirstPersonController>();
        PlayerInputAction = new PlayerInputAction();
        
        PlayerInputAction.Player.Enable();
        PlayerInputAction.Player.Jump.performed += Jump;

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

        PlayerInputAction.Player.TabClick.started += TabClick;
        PlayerInputAction.Player.TabClick.canceled += TabClick;
    }

    private void Update()
    {
        MoveInput = PlayerInputAction.Player.Movement.ReadValue<Vector2>();
        MouseInput = PlayerInputAction.Player.Look.ReadValue<Vector2>();
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
        if (context.started && PlayerInputAction.Player.Movement.ReadValue<Vector2>().y > 0)
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

    public void TabClick(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            PlayerCanvasController.Instance.OpenItemWheel();
        }
        if (context.canceled)
        {
            PlayerCanvasController.Instance.CloseItemWheel();
        }
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
