using System;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "WaveHolder", menuName = "Waves/EnemyWaves")]
public class EnemyWaves : ScriptableObject
{
    [Serializable]
    public class EnemyClump
    {
        public GameObject EnemyToSpawn;
        public int NumberToSpawn;
    }

    [Serializable]
    public class Wave
    {
        public List<EnemyClump> wave = new List<EnemyClump>();
    }

    [SerializeField] public List<Wave> enemyWaves = new List<Wave>();

    private void OnValidate()
    {
        foreach (var Wave in enemyWaves)
        { 
           if (Wave != null)
            {
                List<EnemyClump> tempWave = Wave.wave;
                foreach (var enemy in tempWave)
                {
                    if (enemy.EnemyToSpawn.GetComponent<BaseEnemyClass>() == null)
                    {
                        tempWave.Remove(enemy);
                    }
                }
            }
        }
    }

}
