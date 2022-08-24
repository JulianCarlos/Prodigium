using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCrouchState : State<FirstPersonController>
{
    private float forwardCrouchSpeed = 2.5f;
    private float sideWalkCrouchSpeed = 2f;

    public override void EnterState(FirstPersonController owner)
    {
        //owner.ChangeMovementSpeed(forwardCrouchSpeed, sideWalkCrouchSpeed);
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
