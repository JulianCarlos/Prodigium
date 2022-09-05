using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class PlayerState
{
    public static PlayerStateType PlayerStateType = PlayerStateType.InGame;

    public static void ChangePlayerState(PlayerStateType playerState)
    {
        if(PlayerStateType != playerState)
        {
            PlayerStateType = playerState;

            if (PlayerStateType == PlayerStateType.InMenu)
            {
                Cursor.lockState = CursorLockMode.None;
            }
            else
            {
                Cursor.lockState = CursorLockMode.Locked;
            }
        }
    }
}
