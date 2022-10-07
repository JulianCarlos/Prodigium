using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class ItemAimController : MonoBehaviour
{
    [SerializeField] private float aimSpeed;

    private Vector3 localPos;

    private bool canAim;

    public bool IsAiming { get; private set; } = false;

    private void Start()
    {
        localPos = transform.localPosition;
    }

    private void OnEnable()
    {
        Actions.OnItemChanged += CheckForAiming;
    }

    private void OnDisable()
    {
        Actions.OnItemChanged -= CheckForAiming;
    }

    public void AimOut()
    {
        StartCoroutine(nameof(C_AimOut));
    }

    private void ResetItemPos()
    {
        transform.localPosition = localPos;
    }

    private void CheckForAiming(Item item)
    {
        if (item == null)
            return;

        if(item.ItemData.ItemCategoryType == ItemCategoryType.Weapons)
        {
            canAim = true;
        }
        else
        {
            canAim= false;
        }
        ResetItemPos();
    }

    public void Aim(InputAction.CallbackContext context)
    {
        if (!canAim)
            return;

        if (context.started)
        {
            StopCoroutine(nameof(C_AimOut));
            StartCoroutine(nameof(C_AimIn));
        }
        else if (context.canceled)
        {
            StopCoroutine(nameof(C_AimIn));
            StartCoroutine(nameof(C_AimOut));
        }
    }

    protected virtual IEnumerator C_AimIn()
    {
        IsAiming = true;

        var targetPos = new Vector3(0, localPos.y, localPos.z);

        while (transform.localPosition != targetPos)
        {
            transform.localPosition = Vector3.MoveTowards(transform.localPosition, targetPos, aimSpeed * Time.deltaTime);
            yield return null;
        }
    }

    protected virtual IEnumerator C_AimOut()
    {
        IsAiming = false;

        while (transform.localPosition != localPos)
        {
            transform.localPosition = Vector3.MoveTowards(transform.localPosition, localPos, aimSpeed * Time.deltaTime);
            yield return null;
        }
    }
}
