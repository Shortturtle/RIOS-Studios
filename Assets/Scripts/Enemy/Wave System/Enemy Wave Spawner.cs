using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class EnemyWaveSpawner : MonoBehaviour
{
    public EnemyWaves waveHolder;

    private int currentWave;
    private int maxWaves;

    private List<GameObject> enemiesInCurrentWave = new List<GameObject>();

    public float TimeBetweenSpawns;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        currentWave = 1;
        enemiesInCurrentWave = waveHolder.enemyWaves[currentWave -1].wave;
    }

    private void Awake()
    {
        maxWaves = waveHolder.enemyWaves.Count;     
    }

    // Update is called once per frame
    void Update()
    {
       
        
    }
}
