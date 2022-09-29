using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public abstract class Item : MonoBehaviour
{
    public ItemData ItemData => itemData;

    [SerializeField] private ItemData itemData;

    protected virtual void L_Use(InputAction.CallbackContext context) { }
    protected virtual void R_Use(InputAction.CallbackContext context) { }

    public virtual void SetValues(ItemData data)
    {
        itemData = data;
    }
}
