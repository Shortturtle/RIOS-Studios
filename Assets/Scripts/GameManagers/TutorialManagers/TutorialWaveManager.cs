using UnityEngine;

public class TutorialWaveManager : EnemyWaveManager
{
    protected override void Start()
    {
        currentWave = 1;
        listCleanTimer = listCleanDelay;
        waveEndEnergyReward = waveHolder.waveEndEnergyReward;
    }

    // Update is called once per frame
    protected override void Update()
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
    public void TutorialStart()
    {
        PrepNextWave();
    }

    public override void StartWave()
    {
        currentWavePlaying = true;
        spawnTimer = timeBetweenSpawns;
    }

    public override void EndWave()
    {
        currentWavePlaying = false;
        ResourceManager.instance.AddEnergy(waveEndEnergyReward);
        currentWave++;
        PrepNextWave();
    }

    public override void EndGame()
    {
        currentWavePlaying = false;
        winYes = true;
    }
}
