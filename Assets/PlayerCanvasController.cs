using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerCanvasController : Singleton<PlayerCanvasController>
{
    [SerializeField] private GameObject itemWheel;
    [SerializeField] private GameObject achievementWindow;
    [SerializeField] private GameObject optionsWindow;
    [SerializeField] private GameObject ingameWindow;

    private bool isIngame;

    protected override void Awake()
    {
        base.Awake();

        isIngame = PlayerState.PlayerStateType == PlayerStateType.InGame;
    }

    private void Start()
    {
        PlayerInputs.InputAction.Player.TabClick.started += OpenItemWheel;
        PlayerInputs.InputAction.Player.TabClick.canceled += CloseItemWheel;

        PlayerInputs.InputAction.Player.EscClick.started += ToggleMenu;
    }

    //UI Button Methods
    public void ToggleMenu(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            isIngame = !isIngame;

            if (!isIngame)
            {
                OpenOptionsMenu();
            }
            else
            {
                CloseOptionsMenu();
            }
        }
    }

    private void OpenOptionsMenu()
    {
        CloseItemWheel();

        ingameWindow?.SetActive(false);
        optionsWindow?.SetActive(true);
        PlayerState.ChangePlayerState(PlayerStateType.InMenu);
    }

    public void CloseOptionsMenu()
    {
        isIngame = true;

        optionsWindow?.SetActive(false);
        ingameWindow?.SetActive(true);
        PlayerState.ChangePlayerState(PlayerStateType.InGame);
    }

    public void HeadToHomeBase()
    {
        TransitionManager.Instance.TransitionToScene(1, TransitionMethod.BlackScreen);
    }

    public void HeadToMainMenu()
    {
        TransitionManager.Instance.TransitionToScene(0, TransitionMethod.BlackScreen);
    }

    private void OpenItemWheel(InputAction.CallbackContext context)
    {
        if (PlayerState.PlayerStateType == PlayerStateType.InMenu)
            return;

        itemWheel?.SetActive(true);

        PlayerState.ChangePlayerState(PlayerStateType.InMenu);
    }

    public void CloseItemWheel()
    {
        itemWheel?.SetActive(false);

        PlayerState.ChangePlayerState(PlayerStateType.InGame);
    }

    public void CloseItemWheel(InputAction.CallbackContext context)
    {
        if (itemWheel.activeSelf == false)
            return;

        itemWheel?.SetActive(false);

        PlayerState.ChangePlayerState(PlayerStateType.InGame);
    }
}
