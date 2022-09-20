using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PumpkinManWalkingState : State<MonsterAI>
{
    public override void EnterState(MonsterAI owner)
    {
        owner.StartCoroutine("Wander");
    }

    public override void ExitState(MonsterAI owner)
    {

    }

    public override void FixedUpdateState(MonsterAI owner)
    {

    }

    public override void UpdateState(MonsterAI owner)
    {

    }
}
