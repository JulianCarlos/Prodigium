using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PumpkinManWalkingState : State<MonsterAI>
{
    public override void EnterState(MonsterAI owner)
    {
        owner.StateType = StateType.Wandering;
        owner.StartCoroutine("Wander");
    }

    public override void ExitState(MonsterAI owner)
    {
        owner.StopCoroutine("Wander");
        owner.agent.ResetPath();
    }

    public override void FixedUpdateState(MonsterAI owner)
    {

    }

    public override void UpdateState(MonsterAI owner)
    {
        if (owner.detection.DetectedEntities.Count > 0)
            owner.StateMachine.ChangeState(owner.ChaseState);
    }
}
