using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class ItemAimController : MonoBehaviour
{
    [SerializeField] private float aimSpeed;

    private Vector3 localPos;

    private void Start()
    {
        PlayerInputs.InputAction.Player.RightClick.started += R_Use;
        PlayerInputs.InputAction.Player.RightClick.canceled += R_Use;

        localPos = transform.localPosition;
    }

    protected virtual void R_Use(InputAction.CallbackContext context)
    {
        if (PlayerState.PlayerStateType == PlayerStateType.InMenu)
            return;

        Aim(context);
    }

    protected virtual void Aim(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            Debug.Log("Aim context Started");
            StopCoroutine(nameof(C_AimOut));
            StartCoroutine(nameof(C_AimIn));
        }
        else if (context.canceled)
        {
            Debug.Log("Aim context canceled");
            StopCoroutine(nameof(C_AimIn));
            StartCoroutine(nameof(C_AimOut));
        }
    }

    protected virtual IEnumerator C_AimIn()
    {
        Debug.Log("Started AimIn");
        var targetPos = new Vector3(0, localPos.y, localPos.z);

        while (transform.localPosition != targetPos)
        {
            transform.localPosition = Vector3.MoveTowards(transform.localPosition, targetPos, aimSpeed * Time.deltaTime);
            yield return null;
        }
    }

    protected virtual IEnumerator C_AimOut()
    {
        Debug.Log("Canceled AimIn");
        while (transform.localPosition != localPos)
        {
            transform.localPosition = Vector3.MoveTowards(transform.localPosition, localPos, aimSpeed * Time.deltaTime);
            yield return null;
        }
    }
}
