using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PumpkinManAttackState : State<MonsterAI>
{
    public override void EnterState(MonsterAI owner)
    {
        owner.agent.stoppingDistance = 2;
        owner.StateType = StateType.Attacking;
        owner.animator.SetBool("attacking", true);
        owner.StartCoroutine("Attack");
    }

    public override void ExitState(MonsterAI owner)
    {
        owner.animator.SetBool("attacking", false);
        owner.StopCoroutine("Attack");
    }

    public override void FixedUpdateState(MonsterAI owner)
    {

    }

    public override void UpdateState(MonsterAI owner)
    {

    }
}
