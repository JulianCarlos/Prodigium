using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public abstract class Item : MonoBehaviour
{
    protected virtual void L_Use(InputAction.CallbackContext context) { }
    protected virtual void R_Use(InputAction.CallbackContext context) { }
}
