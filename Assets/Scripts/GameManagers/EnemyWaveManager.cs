using System.Collections.Generic;
using UnityEngine;

public class EnemyWaveManager : MonoBehaviour
{
    static EnemyWaveManager instance;

    public EnemyWaves waveHolder;

    public int currentWave;
    public int maxWaves;

    private List<GameObject> enemiesInCurrentWave = new List<GameObject>();

    public float TimeBetweenSpawns;

    private void Awake()
    {
        maxWaves = waveHolder.enemyWaves.Count;
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        currentWave = 0;
        enemiesInCurrentWave = waveHolder.enemyWaves[currentWave - 1].wave;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
