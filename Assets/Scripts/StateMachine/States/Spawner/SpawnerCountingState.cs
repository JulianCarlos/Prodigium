using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnerCountingState : State<MasterSpawnController>
{
    public override void EnterState(MasterSpawnController owner)
    {
        Debug.Log("Entered Counting State");
    }

    public override void ExitState(MasterSpawnController owner)
    {
        Debug.Log("Exited Counting State");
    }

    public override void FixedUpdateState(MasterSpawnController owner)
    {

    }

    public override void UpdateState(MasterSpawnController owner)
    {

    }
}
