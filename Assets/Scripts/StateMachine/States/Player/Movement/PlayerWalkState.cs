using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWalkState : State<FirstPersonController>
{
    private float forwardWalkSpeed = 3.5f;
    private float sideWalkWalkSpeed = 3f;

    public override void EnterState(FirstPersonController owner)
    {
        //owner.ChangeMovementSpeed(forwardWalkSpeed, sideWalkWalkSpeed);
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
