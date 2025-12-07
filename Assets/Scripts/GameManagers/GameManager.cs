using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    private void Awake()
    {
        if (instance != null)
        {
            Debug.LogError($"Game Manager instance already exists! Remove one of the instances!");
            Destroy(instance);
            instance = this;
        }

        else
        {
            instance = this;
        }
    }

    public GameObject loseUI;
    public GameObject winUI;

    public void LoseGame()
    {
        EnemyWaveManager.instance.LoseWaveClear();
        Debug.Log("YOU LOST");
    }

    public void WinGame()
    {
        Debug.Log("YOU WIN");
    }
}
