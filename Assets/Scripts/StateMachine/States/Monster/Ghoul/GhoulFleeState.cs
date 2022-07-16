using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GhoulFleeState : State<MonsterAI>
{
    public override void EnterState(MonsterAI owner)
    {
        owner.StateType = StateType.Fleeing;
        Debug.Log("Entered GhoulFleeState");
    }

    public override void UpdateState(MonsterAI owner)
    {

    }

    public override void FixedUpdateState(MonsterAI owner)
    {

    }

    public override void ExitState(MonsterAI owner)
    {
        Debug.Log("Exited GhoulFleeState");
    }
}
