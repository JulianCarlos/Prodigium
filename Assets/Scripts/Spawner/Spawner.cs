using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    //Readonly Properties
    public float SpawnProbability => spawnProbability;

    //Private Variables
    [SerializeField] private float spawnProbability;

    private SpawnController owner;

    private void OnValidate()
    {
        owner = GetComponentInParent<SpawnController>();
    }

    private void Awake()
    {
        owner = GetComponentInParent<SpawnController>();
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = owner.SpawnColor;
        Gizmos.DrawWireSphere(transform.position, 1);
    }
}
