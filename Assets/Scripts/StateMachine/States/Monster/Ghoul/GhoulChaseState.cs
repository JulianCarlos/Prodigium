using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GhoulChaseState : State<MonsterAI>
{
    public override void EnterState(MonsterAI owner)
    {
        owner.StateType = StateType.Chasing;
        Debug.Log("Entered GhoulChaseState");
    }

    public override void UpdateState(MonsterAI owner)
    {

    }

    public override void FixedUpdateState(MonsterAI owner)
    {

    }

    public override void ExitState(MonsterAI owner)
    {
        Debug.Log("Exited GhoulChaseState");
    }
}
