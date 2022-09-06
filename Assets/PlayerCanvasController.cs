using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCanvasController : Singleton<PlayerCanvasController>
{
    [SerializeField] private GameObject itemWheel;

    protected override void Awake()
    {
        base.Awake();
    }

    public void OpenItemWheel()
    {
        itemWheel.SetActive(true);
        Cursor.lockState = CursorLockMode.None;
        PlayerState.ChangePlayerState(PlayerStateType.InMenu);
    }

    public void CloseItemWheel()
    {
        itemWheel.SetActive(false);
        Cursor.lockState = CursorLockMode.Locked;
        PlayerState.ChangePlayerState(PlayerStateType.InGame);
    }
}
