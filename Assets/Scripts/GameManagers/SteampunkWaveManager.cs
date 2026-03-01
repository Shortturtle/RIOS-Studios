using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SteampunkWaveManager : EnemyWaveManager
{
    public List<RotatingClockHands> allRotatingClockHands = new List<RotatingClockHands>(3);
    public List<WaypointManager> selectedWaypoints = new List<WaypointManager>(3);
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    protected override void Start()
    {
        SpinAllHands();
        currentWave = 1;
        listCleanTimer = listCleanDelay;
        waveEndEnergyReward = waveHolder.waveEndEnergyReward;
        StartBuffer();
    }

    public override void EndWave()
    {
        currentWavePlaying = false;
        ResourceManager.instance.AddEnergy(waveEndEnergyReward);
        currentWave++;
        StartBuffer();
        SpinAllHands();
    }

    protected override void SpawnEnemy()
    {
        if (enemiesToSpawn[0] != null)
        {
            GameObject tempEnemy = Instantiate(enemiesToSpawn[0], selectedWaypoints[currentSpawnLocation].transform.position, Quaternion.identity);
            tempEnemy.GetComponent<BaseEnemyClass>().InitializeEnemy_Start(selectedWaypoints[currentSpawnLocation]);
            enemiesToSpawn.RemoveAt(0);
            enemiesCurrentlyAlive.Add(tempEnemy);
            currentSpawnLocation++;
            ClampSpawnLocation();
            spawnTimer = timeBetweenSpawns;
        }
    }

    protected override void ClampSpawnLocation()
    {
        if (currentSpawnLocation > selectedWaypoints.Count - 1)
        {
            currentSpawnLocation = 0;
        }
    }

    protected void SpinAllHands()
    {
        int i = 0;
        foreach (RotatingClockHands hand in allRotatingClockHands)
        {
            selectedWaypoints[i] = hand.SpinPickAndPoint();
            i++;
        }
    }
}
