using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWalkState : State<FirstPersonController>
{
    private float targetFOW = 60f;

    public override void EnterState(FirstPersonController owner)
    {
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
