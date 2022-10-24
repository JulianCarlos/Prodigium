using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnerCountingState : State<MasterSpawnController>
{
    public override void EnterState(MasterSpawnController masterSpawner)
    {
        masterSpawner.StartCoroutine("C_CheckForMonsters");
        Debug.Log("Entered Counting State");
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
