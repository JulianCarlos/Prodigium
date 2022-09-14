using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public abstract class Weapon : Item
{
    protected override void Use(InputAction.CallbackContext context)
    {
        Shoot();
    }

    protected abstract void Shoot();
}
