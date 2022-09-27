using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PumpkinManAI : MonsterAI
{
    private void Start()
    {
        SetupStates();
        StateMachine.InitializeFirstState(wanderState);
    }

    protected override void SetupStates()
    {
        wanderState = new PumpkinManWalkingState();
        ChaseState = new PumpkinManChaseState();
        FleeState = new PumpkinManFleeState();
        deadState = new PumpkinManDeadState();
        attackState = new PumpkinManAttackState();
        ScoutState = new PumpinManScoutState();
    }
}
