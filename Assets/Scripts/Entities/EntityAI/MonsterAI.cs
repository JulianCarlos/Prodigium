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
    public State<MonsterAI> AttackState { get => attackState; set => attackState = value; }

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
    [Space(10)]
    [SerializeField] protected float minWanderWaitTime;
    [SerializeField] protected float maxWanderWaitTime;
    [SerializeField] protected float stoppingDistance;

    protected State<MonsterAI> wanderState;
    protected State<MonsterAI> chaseState;
    protected State<MonsterAI> scoutState;
    protected State<MonsterAI> fleeState;
    protected State<MonsterAI> deadState;
    protected State<MonsterAI> attackState;

    protected Monster monster;
    protected Animator animator;
    protected NavMeshAgent agent;

    private void Awake()
    {
        StateMachine = new StateMachine<MonsterAI>(this);
        monster = GetComponent<Monster>();
        animator = GetComponent<Animator>();
        agent = GetComponent<NavMeshAgent>();
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

    public virtual IEnumerator Wander()
    {
        yield return new WaitForSeconds(2f);
        while (true)
        {
            if (monster.IsDead)
                break;

            var waitingTime = Random.Range(minWanderWaitTime, maxWanderWaitTime);
            animator.SetBool("walking", true);
            Wandering();
            yield return new WaitForSeconds(1f);
            yield return new WaitWhile(() => agent.hasPath);
            animator.SetBool("walking", false);
            yield return new WaitForSeconds(waitingTime);
        }
    }

    private void Wandering()
    {
        Vector3 randomDirection = Random.insideUnitSphere * wanderRange;
        randomDirection += transform.position;
        NavMeshHit hit;
        UnityEngine.AI.NavMesh.SamplePosition(randomDirection, out hit, wanderRange, -1);
        Vector3 finalPosition = hit.position;
        agent.SetDestination(finalPosition);
    }
}
