using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCrawlState : State<PlayerInputs>
{
    private float forwardCrawlSpeed = 1.5f;
    private float sideWalkCrawlSpeed = 1f;

    private string animationStateBool;

    public override void EnterState(PlayerInputs owner)
    {
        owner.ChangeMovementSpeed(forwardCrawlSpeed, sideWalkCrawlSpeed);
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
