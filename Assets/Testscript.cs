using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Testscript : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Actions.OnPlayerJump += Jump;
        Actions.OnMonsterSpawned += MonsterSpawned;
    }

    public void Jump(PlayerInputs inputs)
    {
        Debug.Log("Player jumped");
    }

    public void MonsterSpawned(SpawnController controller)
    {
        Debug.Log("Monster spawned"); 
    }
}
