using System;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "WaveHolder", menuName = "Waves/EnemyWaves")]
public class EnemyWaves : ScriptableObject
{
    [Serializable]
    public class Wave
    {
        public List<GameObject> wave = new List<GameObject>();
    }

    [SerializeField] public List<Wave> enemyWaves = new List<Wave>();

    private void OnValidate()
    {
        foreach (var Wave in enemyWaves)
        { List<GameObject> tempWave = Wave.wave;
            foreach (var enemy in tempWave)
            {
                if (enemy.GetComponent<BaseEnemyClass>() == null)
                {
                    tempWave.Remove(enemy);
                }
            }
        }
    }
}
