using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    //Readonly Properties
    public float SpawnProbability => spawnProbability;

    //Private Variables
    [SerializeField] private float spawnProbability;

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, 1);
    }
}
