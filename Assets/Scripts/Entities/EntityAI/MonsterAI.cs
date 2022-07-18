using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(MonsterAI))]
[RequireComponent(typeof(NavMeshAgent))]
public abstract class MonsterAI : Entity
{
    public StateMachine<MonsterAI> StateMachine { get; private set; }

    //Types (Readonly)
    public StateType StateType { get { return stateType; } set { stateType = value; } }
    public BehaviourType BehaviourType { get { return behaviourType; } set { behaviourType = value; } }

    public State<MonsterAI> WanderState { get => wanderState; set => wanderState = value; }
    public State<MonsterAI> ChaseState { get => chaseState; set => chaseState = value; }
    public State<MonsterAI> ScoutState { get => scoutState; set => scoutState = value; }
    public State<MonsterAI> FleeState { get => fleeState; set => fleeState = value; }
    public State<MonsterAI> DeadState { get => deadState; set => deadState = value; }

    //Types
    [SerializeField] protected StateType stateType;
    [SerializeField] protected BehaviourType behaviourType;

    //AI Settings
    [Space(15), Header("AI General Settings")]
    [SerializeField] protected float jumpStrength;
    [SerializeField] protected Vector3 jumpForce;

    //AI Wander Settings
    [Space(15), Header("AI Range Settings")]
    [SerializeField] protected float fleeRange;
    [SerializeField] protected float chaseRange;
    [SerializeField] protected float attackRange;
    [SerializeField] protected float scoutRange;
    [SerializeField] protected float wanderRange;
    [Space(10)]
    [SerializeField] protected float minAttackRange;
    [SerializeField] protected float maxAttackRange;
    [SerializeField] protected float stoppingDistance;

    protected State<MonsterAI> wanderState;
    protected State<MonsterAI> chaseState;
    protected State<MonsterAI> scoutState;
    protected State<MonsterAI> fleeState;
    protected State<MonsterAI> deadState;

    protected Monster monster;
    protected NavMeshAgent agent;

    private void Awake()
    {
        StateMachine = new StateMachine<MonsterAI>(this);
        agent = GetComponent<NavMeshAgent>();
        monster = GetComponent<Monster>();
    }

    private void Update()
    {
        StateMachine?.Update();
    }

    private void FixedUpdate()
    {
        StateMachine?.FixedUpdate();
    }

    protected abstract void SetupStates();

}
