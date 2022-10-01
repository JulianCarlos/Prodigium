using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PumpkinManChaseState : State<MonsterAI>
{
    public override void EnterState(MonsterAI owner)
    {
        owner.StateType = StateType.Chasing;
        owner.agent.speed = 6;
        owner.agent.stoppingDistance = 3;
        owner.StartCoroutine("Chase");
    }

    public override void ExitState(MonsterAI owner)
    {
        owner.agent.speed = 3;
        owner.agent.stoppingDistance = 0;
        owner.StopCoroutine("Chase");
    }

    public override void FixedUpdateState(MonsterAI owner)
    {

    }

    public override void UpdateState(MonsterAI owner)
    {

    }
}
