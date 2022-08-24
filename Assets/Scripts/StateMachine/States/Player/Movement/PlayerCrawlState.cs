using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCrawlState : State<FirstPersonController>
{
    private float forwardCrawlSpeed = 1.5f;
    private float sideWalkCrawlSpeed = 1f;

    public override void EnterState(FirstPersonController owner)
    {
        //owner.ChangeMovementSpeed(forwardCrawlSpeed, sideWalkCrawlSpeed);
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
