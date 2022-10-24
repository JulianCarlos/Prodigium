using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

//IMPPORTANT
//Check if full monster data is needed or not


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
    public int MonsterSpawnCount => monsterSpawnCount;
    public float IntervalBetweenSpawning => intervalBetweenSpawning;

    //Private Variables
    [SerializeField] private List<Monster> spawnedMonsters = new();
    [SerializeField] private List<SpawnController> spawnControllers = new();

    //Spawn Settings
    [SerializeField] private int monsterSpawnCount = 1;
    [SerializeField] private float intervalBetweenSpawning = 1;

    [SerializeField] private float spawnWaitingTime;

    [Header("Audio Settings")]
    [SerializeField] private AudioSource spawnerAudioSource;

    [SerializeField] private AudioClip spawnStartedAudioClip;

    protected override void Awake()
    {
        base.Awake();
        SetupStates();

        StateMachine = new StateMachine<MasterSpawnController>(this);

        spawnControllers = FindObjectsOfType<SpawnController>().ToList();
        spawnControllers = spawnControllers.OrderBy(x => x.SpawnProbability).ToList();

        spawnerAudioSource = GetComponent<AudioSource>();
    }
    private void Start()
    {
        Invoke(nameof(StartSpawner), 50f);
    }

    public void RemoveMonster(Monster monster)
    {
        spawnedMonsters.Remove(monster);
    }

    private void StartSpawner()
    {
        StateMachine.InitializeFirstState(SpawnerSpawningState);
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

    public void SpawnCoolDown()
    {
        StopAllCoroutines();
        StartCoroutine(nameof(SpawnCoolDown));
    }
    private IEnumerator C_SpawnCoolDown()
    {
        yield return new WaitForSeconds(spawnWaitingTime);
        StateMachine.ChangeState(SpawnerSpawningState);
    }

    public void CheckForMonsters()
    {
        StopAllCoroutines();
        StartCoroutine(nameof(CheckForMonsters));
    }
    private IEnumerator C_CheckForMonsters()
    {
        while (SpawnedMonsters.Count > 0)
        {
            yield return null;
        }
        StateMachine.ChangeState(SpawnerWaitingState);
    }

    public void SpawnMonsters()
    {
        StopAllCoroutines();
        StartCoroutine(nameof(C_SpawnMonsters));
    }
    private IEnumerator C_SpawnMonsters()
    {
        yield return new WaitForSeconds(intervalBetweenSpawning);
        spawnerAudioSource.PlayOneShot(spawnStartedAudioClip);

        for (int i = 0; i < monsterSpawnCount ;)
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
  