using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
public class MasterSpawnController : Singleton<MasterSpawnController>
{
    //Statemachine References
    public StateMachine<MasterSpawnController> StateMachine { get; private set; }

    //Spawning State References
    public SpawnerSpawningState SpawnerSpawningState { get; private set; }
    public SpawnerCountingState SpawnerCountingState { get; private set; }
    public SpawnerWaitingState SpawnerWaitingState { get; private set; }

    //Readonly Properties
    public List<Monster> SpawnedMonsters => spawnedMonsters;
    public List<SpawnController> SpawnControllers => spawnControllers;

    //Private Variables
    [SerializeField] private List<Monster> spawnedMonsters = new();
    [SerializeField] private List<SpawnController> spawnControllers = new();

    //Spawn Settings
    [SerializeField] private int monsterSpawnCount = 1;
    [SerializeField] private float intervalBetweenSpawning = 1;

    protected override void Awake()
    {
        base.Awake();
        SetupStates();

        StateMachine = new StateMachine<MasterSpawnController>(this);

        spawnControllers = FindObjectsOfType<SpawnController>().ToList();
        spawnControllers = spawnControllers.OrderBy(x => x.SpawnProbability).ToList();
    }
    private void Start()
    {
        StateMachine.InitializeFirstState(SpawnerSpawningState);
        //SpawnMonsters();
    }
    private void Update()
    {
        StateMachine?.Update();
    }
    private void FixedUpdate()
    {
        StateMachine?.FixedUpdate();
    }

    private void SetupStates()
    {
        SpawnerSpawningState = new();
        SpawnerCountingState = new();
        SpawnerWaitingState = new();
    }

    public void SpawnMonsters()
    {
        StartCoroutine(nameof(Spawning));
    }

    private IEnumerator Spawning()
    {
        for (int i = 0; i < monsterSpawnCount;)
        {
            float randomPercentage = Random.Range(0f, 100f);

            foreach (var spawnController in spawnControllers)
            {
                if (randomPercentage > (100 - spawnController.SpawnProbability))
                {
                    yield return new WaitForSeconds(intervalBetweenSpawning);

                    spawnController.SpawnMonsters();
                    i++;
                }
            }
        }
        StateMachine.ChangeState(SpawnerCountingState);
    }
}
