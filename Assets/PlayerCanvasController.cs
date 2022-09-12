using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCanvasController : Singleton<PlayerCanvasController>
{
    [SerializeField] private GameObject itemWheel;
    [SerializeField] private GameObject achievementWindow;
    [SerializeField] private GameObject optionsWindow;

    protected override void Awake()
    {
        base.Awake();
    }

    public void OpenItemWheel()
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
}
