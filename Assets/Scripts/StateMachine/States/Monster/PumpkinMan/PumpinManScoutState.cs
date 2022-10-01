using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PumpinManScoutState : State<MonsterAI>
{
    public override void EnterState(MonsterAI owner)
    {
        owner.StateType = StateType.Scouting;
        owner.StartCoroutine("Scout");
    }

    public override void ExitState(MonsterAI owner)
    {

    }

    public override void FixedUpdateState(MonsterAI owner)
    {

    }

    public override void UpdateState(MonsterAI owner)
    {
        if(owner.detection.DetectedEntities.Count > 0)
            owner.StateMachine.ChangeState(owner.ChaseState);
    }
}
