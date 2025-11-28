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
    private bool bufferActive;
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
        StartBuffer();
    }

    // Update is called once per frame
    void Update()
    {
        ListCleaning();

        WaveEndCheck();

        if (currentWavePlaying && enemiesToSpawn.Count != 0)
        {
            SpawnTimer();
        }

        else if (bufferActive)
        {
            BufferTimer();
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
        StartBuffer();  
    }

    void PrepNextWave()
    {
        currentWave++;
        enemiesToSpawn.Clear();
        enemiesCurrentlyAlive.Clear();
        InitializeWave(waveHolder.enemyWaves[currentWave].wave);    
    }

    void StartBuffer()
    {
        PrepNextWave();
        bufferActive = true;
        waveBufferTimer = waveBufferTime;
    }

    void EndBuffer()
    {
        StartWave();
        bufferActive = false;
        spawnTimer = timeBetweenSpawns;
    }

    void BufferTimer()
    {
        if (waveBufferTimer > 0)
        {
            waveBufferTimer -= Time.deltaTime;
        }

        if (waveBufferTimer <= 0)
        {
            EndBuffer();
        }
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
            listCleanTimer = listCleanDelay;
        }
    }

    void CleanLists()
    {
        List<GameObject> toRemove1 = new List<GameObject>();
        List<GameObject> toRemove2 = new List<GameObject>();

        if (enemiesToSpawn != null)
        {
            foreach (var enemy in enemiesToSpawn)
            {
                var tempEnemy = enemy;
                if (tempEnemy == null)
                {
                    toRemove1.Add(tempEnemy);
                }
            }
        }

        if (enemiesCurrentlyAlive != null)
        {
            foreach (var enemy in enemiesCurrentlyAlive)
            {
                var tempEnemy = enemy;
                if (tempEnemy == null)
                {
                    toRemove2.Add(tempEnemy);
                }
            }
        }

        foreach (var enemy in toRemove1)
        {
            enemiesToSpawn.Remove(enemy);
        }

        foreach(var enemy in toRemove2)
        {
            enemiesCurrentlyAlive.Remove(enemy);
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
            enemiesToSpawn.RemoveAt(0);
            enemiesCurrentlyAlive.Add(tempEnemy);
            currentSpawnLocation++;
            ClampSpawnLocation();
            spawnTimer = timeBetweenSpawns;
        }
    }

    void ClampSpawnLocation()
    {
        if (currentSpawnLocation > spawnLocations.Count - 1)
        {
            currentSpawnLocation = 0;
        }
    }

    void WaveEndCheck()
    {
        if (enemiesCurrentlyAlive.Count == 0 && enemiesToSpawn.Count == 0)
        {
            EndWave();
        }
    }
}
