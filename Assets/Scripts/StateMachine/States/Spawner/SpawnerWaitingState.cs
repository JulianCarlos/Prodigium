using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnerWaitingState : State<MasterSpawnController>
{
    public override void EnterState(MasterSpawnController owner)
    {
        Debug.Log("Entered Waiting State");
    }

    public override void ExitState(MasterSpawnController owner)
    {
        Debug.Log("Exited Waiting State");
    }

    public override void FixedUpdateState(MasterSpawnController owner)
    {

    }

    public override void UpdateState(MasterSpawnController owner)
    {

    }
}
