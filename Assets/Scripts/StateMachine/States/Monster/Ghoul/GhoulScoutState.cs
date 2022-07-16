using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GhoulScoutState : State<MonsterAI>
{
    public override void EnterState(MonsterAI owner)
    {
        owner.StateType = StateType.Scouting;
        Debug.Log("Entered GhoulWanderState");
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
