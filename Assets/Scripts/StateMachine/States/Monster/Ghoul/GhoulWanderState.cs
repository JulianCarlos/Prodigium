using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GhoulWanderState : State<MonsterAI>
{
    public override void EnterState(MonsterAI owner)
    {
        owner.StateType = StateType.Wandering;
        Debug.Log("EnteredGhoulWanderState");
    }

    public override void UpdateState(MonsterAI owner)
    {

    }

    public override void FixedUpdateState(MonsterAI owner)
    {

    }

    public override void ExitState(MonsterAI owner)
    {
        Debug.Log("Exited GhoulWanderState");
    }
}
