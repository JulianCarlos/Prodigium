using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PumpkinManAI : MonsterAI
{

    [SerializeField] private Color FriendlyColor;
    [SerializeField] private Color AngryColor;

    [SerializeField] private float intensitivy;

    [SerializeField] private Material PumpkinMaterial;

    [SerializeField] private float colorFadeAway;

    private void Start()
    {
        SetupStates();
        StateMachine.InitializeFirstState(wanderState);

        //PumpkinMaterial.SetColor("_EmissionColor", FriendlyColor * intensitivy);
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

    public override IEnumerator Chase()
    {
        intensitivy = 3;
        PumpkinMaterial.SetColor("_EmissionColor", AngryColor * intensitivy);
        return base.Chase();
    }

    public override IEnumerator Wander()
    {
        intensitivy = 5;
        PumpkinMaterial.SetColor("_EmissionColor", FriendlyColor * intensitivy);
        return base.Wander();
    }

    public override IEnumerator Die()
    {
        while (intensitivy > 0)
        {
            intensitivy -= colorFadeAway * Time.deltaTime;
            PumpkinMaterial.SetColor("_EmissionColor", AngryColor * intensitivy);
        }
        return base.Die();
    }
}
