using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerRunState : State<PlayerInputs>
{
    private float forwardRunSpeed = 5f;
    private float sideWalkRunSpeed = 3f;

    private string animationStateBool;

    public override void EnterState(PlayerInputs owner)
    {
        owner.ChangeMovementSpeed(forwardRunSpeed, sideWalkRunSpeed);
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
