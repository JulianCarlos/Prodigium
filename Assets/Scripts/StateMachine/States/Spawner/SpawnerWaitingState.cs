using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnerWaitingState : State<MasterSpawnController>
{
    public override void EnterState(MasterSpawnController masterSpawner)
    {
        masterSpawner.StartCoroutine("C_SpawnCoolDown");
    }

    public override void ExitState(MasterSpawnController masterSpawner)
    {

    }

    public override void FixedUpdateState(MasterSpawnController masterSpawner)
    {

    }

    public override void UpdateState(MasterSpawnController masterSpawner)
    {

    }
}
