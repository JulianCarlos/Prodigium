using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnerSpawningState : State<MasterSpawnController>
{
    public override void EnterState(MasterSpawnController masterSpawner)
    {
        Debug.Log("Entered Spawning State");
        masterSpawner.SpawnMonsters();
    }

    public override void ExitState(MasterSpawnController masterSpawner)
    {
        Debug.Log("Exited Spawning State");
    }

    public override void FixedUpdateState(MasterSpawnController masterSpawner)
    {

    }

    public override void UpdateState(MasterSpawnController masterSpawner)
    {

    }
}
