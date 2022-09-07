using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class GhoulAI : MonsterAI
{
    private void Start()
    {
        SetupStates();
        StateMachine.InitializeFirstState(wanderState);
    }
    
    protected override void SetupStates()
    {
        wanderState = new GhoulWanderState();
        ChaseState = new GhoulChaseState();
        attackState = new GhoulAttackState();
        FleeState = new GhoulFleeState();
        DeadState = new GhoulDeadState();
        ScoutState = new GhoulScoutState();
    }
}
