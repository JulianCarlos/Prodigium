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

    //Readonly
    public Collider Target => target;

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
    internal Detection detection;
    internal Animator animator;
    internal NavMeshAgent agent;

    private float entityNotFoundTimer;

    [SerializeField] private Collider target;

    private void Awake()
    {
        StateMachine = new StateMachine<MonsterAI>(this);
        monster = GetComponent<Monster>();
        detection = GetComponent<Detection>();
        animator = GetComponent<Animator>();
        agent = GetComponent<NavMeshAgent>();
    }

    private void Update()
    {
        StateMachine?.Update();

        animator.SetFloat("floatY",Mathf.Clamp(agent.velocity.magnitude, 0, StateMachine.CurrentState == ChaseState ? 2 : 1));
    }

    private void FixedUpdate()
    {
        StateMachine?.FixedUpdate();
    }

    protected abstract void SetupStates();



    #region Wandering
    public virtual IEnumerator Wander()
    {
        yield return new WaitForSeconds(2f);
        while (true)
        {
            if (monster.IsDead)
                break;

            var waitingTime = Random.Range(minWanderWaitTime, maxWanderWaitTime);
            Wandering();
            yield return new WaitForSeconds(1f);
            yield return new WaitWhile(() => agent.hasPath);
            yield return new WaitForSeconds(waitingTime);
        }
    }

    protected void Wandering()
    {
        Vector3 randomDirection = Random.insideUnitSphere * wanderRange;
        randomDirection += transform.position;
        NavMeshHit hit;
        UnityEngine.AI.NavMesh.SamplePosition(randomDirection, out hit, wanderRange, -1);
        Vector3 finalPosition = hit.position;
        agent.SetDestination(finalPosition);
    }
    #endregion



    #region Scouting
    public virtual IEnumerator Scout()
    {
        yield return null;
        Scouting();
        yield return new WaitWhile(() => agent.hasPath);
        Debug.Log("Reached Point");

        yield return new WaitForSeconds(4f);
        StateMachine.ChangeState(wanderState);
    }

    protected void Scouting()
    {
        Debug.Log("Monster is Scouting");
    }
    #endregion



    #region Chase
    public virtual IEnumerator Chase()
    {
        entityNotFoundTimer = 0;

        target = detection.DetectedEntities[0];

        while(StateMachine.CurrentState == chaseState)
        {
            Chasing();
            yield return null;
        }
    }

    protected void Chasing()
    {
        if(detection.DetectedEntities.Count > 0)
        {
            entityNotFoundTimer = 0;
        }
        else
        {
            entityNotFoundTimer += 1 * Time.deltaTime;
            if(entityNotFoundTimer >= 10)
            {
                StateMachine.ChangeState(ScoutState);
            }
        }
        agent.SetDestination(target.transform.position);

        if (Vector3.Distance(transform.position, target.transform.position) <= maxAttackRange)
        {
            StateMachine.ChangeState(attackState);
        }
    }
    #endregion

    #region Attack
    public virtual IEnumerator Attack()
    {
        while (StateMachine.CurrentState == attackState && target)
        {
            Attacking();
            yield return null;
        }
    }

    protected void Attacking()
    {
        agent.SetDestination(target.transform.position);

        if (Vector3.Distance(target.transform.position, transform.position) > maxAttackRange)
        {
            StateMachine.ChangeState(chaseState);
        }
    }
    #endregion

    #region Flee
    public virtual IEnumerator Flee()
    {
        Fleeing();
        yield return null;
    }

    protected void Fleeing()
    {
        Debug.Log("Monster is Fleeing");
    }
    #endregion

    public virtual IEnumerator Die()
    {
        yield return null;
    }
}
