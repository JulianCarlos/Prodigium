using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerCanvasController : Singleton<PlayerCanvasController>
{
    [SerializeField] private GameObject itemWheel;
    [SerializeField] private GameObject achievementWindow;
    [SerializeField] private GameObject optionsWindow;

    protected override void Awake()
    {
        base.Awake();
    }

    private void Start()
    {
        PlayerInputs.InputAction.Player.TabClick.started += OpenItemWheel;
        PlayerInputs.InputAction.Player.TabClick.canceled += CloseItemWheel;
    }

    //UI Button Methods
    public void OpenOptionsMenu()
    {
        optionsWindow?.SetActive(true);
        PlayerState.ChangePlayerState(PlayerStateType.InMenu);
    }

    public void CloseOptionsMenu()
    {
        optionsWindow?.SetActive(false);
        PlayerState.ChangePlayerState(PlayerStateType.InGame);
    }

    public void LeaveGame()
    {
        TransitionManager.Instance.TransitionToScene(0, TransitionMethod.BlackScreen);
    }

    //Item Wheel

    //public void ToggleItemWheel(InputAction.CallbackContext context)
    //{
    //    if (context.started)
    //    {
    //        OpenItemWheel();
    //    }
    //    else if (context.canceled)
    //    {
    //        CloseItemWheel();
    //    }
    //}

    private void OpenItemWheel(InputAction.CallbackContext context)
    {
        itemWheel?.SetActive(true);

        Cursor.lockState = CursorLockMode.None;
        PlayerState.ChangePlayerState(PlayerStateType.InMenu);
    }

    public void CloseItemWheel()
    {
        itemWheel?.SetActive(false);

        Cursor.lockState = CursorLockMode.Locked;
        PlayerState.ChangePlayerState(PlayerStateType.InGame);
    }

    public void CloseItemWheel(InputAction.CallbackContext context)
    {
        itemWheel?.SetActive(false);

        Cursor.lockState = CursorLockMode.Locked;
        PlayerState.ChangePlayerState(PlayerStateType.InGame);
    }
}
