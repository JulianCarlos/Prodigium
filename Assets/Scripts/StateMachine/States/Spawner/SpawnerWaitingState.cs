using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnerWaitingState : State<MasterSpawnController>
{
    public override void EnterState(MasterSpawnController masterSpawner)
    {
        Debug.Log("Entered Waiting State");
    }

    public override void ExitState(MasterSpawnController masterSpawner)
    {
        Debug.Log("Exited Waiting State");
    }

    public override void FixedUpdateState(MasterSpawnController masterSpawner)
    {

    }

    public override void UpdateState(MasterSpawnController masterSpawner)
    {

    }
}
