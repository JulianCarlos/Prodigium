using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerRunState : State<FirstPersonController>
{
    private float forwardRunSpeed = 5f;
    private float sideWalkRunSpeed = 3f;

    public override void EnterState(FirstPersonController owner)
    {
        //owner.ChangeMovementSpeed(forwardRunSpeed, sideWalkRunSpeed);
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
