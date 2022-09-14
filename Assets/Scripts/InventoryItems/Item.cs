using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public abstract class Item : MonoBehaviour
{
    [SerializeField] protected ItemData itemData;

    protected abstract void Use(InputAction.CallbackContext context);
}
