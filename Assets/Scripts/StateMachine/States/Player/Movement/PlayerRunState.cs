using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerRunState : State<FirstPersonController>
{
    private float targetFOW = 90f;

    public override void EnterState(FirstPersonController owner)
    {
        Debug.Log("Entered Run State");
        owner.CameraController.ApplyFOWValue(targetFOW);
    }

    public override void ExitState(FirstPersonController owner)
    {

    }

    public override void FixedUpdateState(FirstPersonController owner)
    {

    }

    public override void UpdateState(FirstPersonController owner)
    {

    }
}
