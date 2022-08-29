using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateMachine<T>
{
    public T Owner;
    public State<T> CurrentState { get; private set; }

    public StateMachine(T owner)
    {
        Owner = owner;
        CurrentState = null;    
    }

    public void InitializeFirstState(State<T> firstState)
    {
        ChangeState(firstState);
    }

    public void ChangeState(State<T> newState)
    {
        CurrentState?.ExitState(Owner);
        CurrentState = newState;
        CurrentState.EnterState(Owner);
        Debug.Log(CurrentState.ToString());
    }

    public void Update()
    {
        CurrentState?.UpdateState(Owner);
    }

    public void FixedUpdate()
    {
        CurrentState?.FixedUpdateState(Owner);
    }
}
