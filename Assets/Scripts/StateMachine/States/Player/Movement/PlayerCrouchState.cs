using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCrouchState : State<PlayerInputs>
{
    private float forwardCrouchSpeed = 2.5f;
    private float sideWalkCrouchSpeed = 2f;

    public override void EnterState(PlayerInputs owner)
    {
        owner.ChangeMovementSpeed(forwardCrouchSpeed, sideWalkCrouchSpeed);
    }

    public override void ExitState(PlayerInputs owner)
    {

    }

    public override void FixedUpdateState(PlayerInputs owner)
    {

    }

    public override void UpdateState(PlayerInputs owner)
    {

    }
}
