using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnerSpawningState : State<MasterSpawnController>
{
    public override void EnterState(MasterSpawnController owner)
    {
        Debug.Log("Entered Spawning State");
        owner.SpawnMonsters();
    }

    public override void ExitState(MasterSpawnController owner)
    {
        Debug.Log("Exited Spawning State");
    }

    public override void FixedUpdateState(MasterSpawnController owner)
    {

    }

    public override void UpdateState(MasterSpawnController owner)
    {

    }
}
