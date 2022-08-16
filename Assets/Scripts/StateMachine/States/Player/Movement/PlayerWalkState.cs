using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWalkState : State<PlayerInputs>
{
    private float forwardWalkSpeed = 3.5f;
    private float sideWalkWalkSpeed = 3f;

    public override void EnterState(PlayerInputs owner)
    {
        owner.ChangeMovementSpeed(forwardWalkSpeed, sideWalkWalkSpeed);
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
