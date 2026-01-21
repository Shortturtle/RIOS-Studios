using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.InputManagerEntry;

public class EnemyWaveManager : MonoBehaviour
{
    public static EnemyWaveManager instance;

    private void Awake()
    {
        if (instance != null)
        {
            Debug.LogError($"Enemy Wave Manager instance already exists! Remove one of the instances!");
            Destroy(instance);
            instance = this;
        }

        else
        {
            instance = this;
        }

        maxWaves = waveHolder.enemyWaves.Count;
    }

    public EnemyWaves waveHolder;

    public int currentWave;
    public int maxWaves;

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
    private int waveEndEnergyReward;
    private bool winYes = false;

    private float listCleanDelay = 0.2f;
    private float listCleanTimer;

    //medieval season gimmick
    private SeasonsManager seasons;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        currentWave = 1;
        listCleanTimer = listCleanDelay;
        waveEndEnergyReward = waveHolder.waveEndEnergyReward;
        StartBuffer();

        seasons = FindFirstObjectByType<SeasonsManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!winYes)
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
    }

    void StartWave()
    {
        currentWavePlaying = true;
        spawnTimer = timeBetweenSpawns;
    }

    void EndWave()
    {
        currentWavePlaying = false;
        ResourceManager.instance.AddEnergy(waveEndEnergyReward);
        currentWave++;
        StartBuffer();
    }

    void PrepNextWave()
    {
        enemiesToSpawn.Clear();
        enemiesCurrentlyAlive.Clear();
        InitializeWave(waveHolder.enemyWaves[currentWave - 1].wave);    
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
        Debug.Log(temp.Count);
        foreach (var enemyClump in temp)
        {
            for (int i = 0; i < enemyClump.NumberToSpawn; i++)
            {
                enemiesToSpawn.Add(enemyClump.EnemyToSpawn);
            }
        }
    }

    void EndGame()
    {
        currentWavePlaying = false;
        winYes = true;
        GameManager.instance.WinGame();
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
            tempEnemy.GetComponent<BaseEnemyClass>().InitializeEnemy_Start(spawnLocations[currentSpawnLocation]);
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
            if (currentWave == maxWaves && !winYes)
            {
                EndGame();
            }

            else
            {
                EndWave();
            }
        }
    }

    public void LoseWaveClear()
    {
        if (!currentWavePlaying)
        {
            this.enabled = false;
        }

        else if (currentWavePlaying)
        {
            if (enemiesCurrentlyAlive.Count > 0)
            {
                foreach(var enemy in enemiesCurrentlyAlive)
                {
                    Destroy(enemy);
                }
                enemiesCurrentlyAlive.Clear();
            }

            if (enemiesToSpawn.Count > 0)
            {
                enemiesToSpawn.Clear();
            }

            this.enabled = false;
        }
    }

    //to call season changer if there is, to change the season
    private void SeasonChange() { if (seasons != null) { seasons.SeasonsChanger(); } }
    public void AddEnemyDuringWave(GameObject enemy)
    {
        enemiesCurrentlyAlive.Add(enemy);
    }
}
