using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class SpawnController : MonoBehaviour
{
    //Readonly property
    public Monster MonsterPrefab => monsterPrefab;
    public float SpawnProbability => spawnProbability;
    public List<Spawner> Spawners => spawners;
    public List<Monster> SpawnedMonsters => spawnedMonsters;

    //Property
    public Color SpawnColor;

    //Private References
    [SerializeField] private MasterSpawnController owner;
    [SerializeField] private Monster monsterPrefab;

    //Private fields
    [SerializeField] private List<Spawner> spawners = new();
    [SerializeField] private List<Monster> spawnedMonsters = new();

    //Spawn Settings
    [SerializeField] private float spawnProbability;

    [SerializeField] private uint maxSpawnableMonsters;
    [SerializeField] private uint minSpawnAbleMonsters;

    //Visuals

    private void Awake()
    {
        owner = FindObjectOfType<MasterSpawnController>();
        spawners = GetComponentsInChildren<Spawner>().ToList();
        spawners = spawners.OrderBy(x => x.SpawnProbability).ToList();
    }

    public void SpawnMonsters()
    {
        float randomPercentage = Random.Range(0f, 100f);

        foreach (var spawner in spawners)
        {
            if (randomPercentage > (100 - spawner.SpawnProbability))
            {
                Monster monster = Instantiate(monsterPrefab, spawner.transform.position, Quaternion.identity);
                owner.SpawnedMonsters.Add(monster);
                return;
            }
        }
        Monster monster1 = Instantiate(monsterPrefab, spawners[spawners.Count -1].transform.position, Quaternion.identity);
        owner.SpawnedMonsters.Add(monster1);
    }
}
