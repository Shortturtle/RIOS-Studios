using System.Collections.Generic;
using UnityEngine;

public class EnemyWaveManager : MonoBehaviour
{
    public static EnemyWaveManager instance;

    public EnemyWaves waveHolder;

    private int currentWave;
    private int maxWaves;

    public List<WaypointManager> spawnLocations = new List<WaypointManager>();
    private int currentSpawnLocation;

    private List<GameObject> enemiesToSpawn = new List<GameObject>();
    public List<GameObject> enemiesCurrentlyAlive = new List<GameObject>();

    public float waveBufferTime;
    private float waveBufferTimer;
    public float timeBetweenSpawns;
    private float spawnTimer;
    private bool currentWavePlaying;

    private float listCleanDelay = 0.1f;
    private float listCleanTimer;

    private void Awake()
    {
        maxWaves = waveHolder.enemyWaves.Count;
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        currentWave = 0;
        InitializeWave(waveHolder.enemyWaves[currentWave].wave);
        listCleanTimer = listCleanDelay;
    }

    // Update is called once per frame
    void Update()
    {
        ListCleaning();

        if (currentWavePlaying && enemiesToSpawn.Count != 0)
        {
            SpawnTimer();
        }
    }

    void StartWave()
    {
        currentWavePlaying = true;
        spawnTimer = timeBetweenSpawns;
    }

    void EndWave()
    {
        currentWavePlaying = false;
        waveBufferTimer = waveBufferTime;
    }

    void PrepNextWave()
    {
        currentWave++;
        enemiesToSpawn.Clear();
        enemiesCurrentlyAlive.Clear();
        InitializeWave(waveHolder.enemyWaves[currentWave].wave);    
    }

    void InitializeWave(List<EnemyWaves.EnemyClump> temp)
    {
        foreach (var enemyClump in temp)
        {
            for (int i = 0; i < enemyClump.NumberToSpawn; i++)
            {
                enemiesToSpawn.Add(enemyClump.EnemyToSpawn);
            }
        }
    }

    void ListCleaning()
    {
        listCleanTimer -= Time.deltaTime;

        if(listCleanTimer < 0)
        {
            CleanLists();
        }
    }

    void CleanLists()
    {
        foreach(var enemy in enemiesToSpawn)
        {
            if(enemy == null)
            {
                enemiesToSpawn.Remove(enemy);
            }
        }

        foreach (var enemy in enemiesCurrentlyAlive)
        {
            if(enemy == null)
            {
                enemiesCurrentlyAlive.Remove(enemy);
            }
        }
    }

    void SpawnTimer()
    {
        if (spawnTimer > 0)
        {
            spawnTimer -= Time.deltaTime;
        }

        if (spawnTimer <= 0)
        {
            SpawnEnemy();
        }
    }

    void SpawnEnemy()
    {
        if (enemiesToSpawn[0] != null)
        {
            GameObject tempEnemy = Instantiate(enemiesToSpawn[0], spawnLocations[currentSpawnLocation].transform.position, Quaternion.identity);
            tempEnemy.GetComponent<BaseEnemyClass>().InitializeEnemy(spawnLocations[currentSpawnLocation]);
            enemiesToSpawn.Remove(tempEnemy);
            enemiesCurrentlyAlive.Add(tempEnemy);
            currentSpawnLocation++;
            ClampSpawnLocation();
        }
    }

    void ClampSpawnLocation()
    {
        if (currentSpawnLocation > spawnLocations.Count - 1)
        {
            currentSpawnLocation = 0;
        }
    }
}
