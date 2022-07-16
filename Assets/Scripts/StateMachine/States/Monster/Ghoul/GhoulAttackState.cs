using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GhoulAttackState : State<MonsterAI>
{
    public override void EnterState(MonsterAI owner)
    {
        owner.StateType = StateType.Attacking;
        Debug.Log("Entered GhoulAttackState");
    }

    public override void UpdateState(MonsterAI owner)
    {

    }

    public override void FixedUpdateState(MonsterAI owner)
    {
        
    }

    public override void ExitState(MonsterAI owner)
    {
        Debug.Log("Exited GhoulAttackState");
    }
}
