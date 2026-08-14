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

        SaveSystem.CreateSaveFile();
        SaveSystem.Load();
    }

    public GameObject loseUI;
    public AK.Wwise.Event loseSFX;
    public GameObject winUI;
    public AK.Wwise.Event winSFX;
    public AK.Wwise.Event LevelMusic;
    public bool gameEnd = false;

    private void Start()
    {
        PlayLevelMusic();
    }
    public void LoseGame()
    {
        EnemyWaveManager.instance.LoseWaveClear();
        if (MicrogameManager.instance.currentlyPlayingMinigame)
        {
            MicrogameManager.instance.MicrogameEnd();
        }
        gameEnd = true;
        loseUI.SetActive(true);
        StopLevelMusic();
        loseSFX.Post(gameObject);
        Debug.Log("YOU LOST");
    }

    public void WinGame()
    {
        if (MicrogameManager.instance.currentlyPlayingMinigame)
        {
            MicrogameManager.instance.MicrogameEnd();
        }
        gameEnd = true;
        winUI.SetActive(true);
        StopLevelMusic();
        winSFX.Post(gameObject);
        Debug.Log("YOU WIN");
    }

    public void StopLevelMusic()
    {
        LevelMusic.Stop(gameObject);
    }

    public void PlayLevelMusic()
    {
        LevelMusic.Post(gameObject);
    }
}
