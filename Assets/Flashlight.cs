using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Flashlight : Utility
{
    [SerializeField] private Light flashLight;

    protected override void R_Use(InputAction.CallbackContext context)
    {
        if (context.started) 
        { 
            ToggleLight();
        }
    }

    private void ToggleLight()
    {
        flashLight.enabled = !flashLight.enabled;
    }
}
