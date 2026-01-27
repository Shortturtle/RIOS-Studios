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
    public AK.Wwise.Event loseSFX;
    public GameObject winUI;
    public AK.Wwise.Event winSFX;
    public AK.Wwise.Event LevelMusic;

    private void Start()
    {
        AudioManager.instance.PlayAudioEvent(LevelMusic, gameObject);
    }
    public void LoseGame()
    {
        EnemyWaveManager.instance.LoseWaveClear();
        if (MicrogameManager.instance.currentlyPlayingMinigame)
        {
            MicrogameManager.instance.MicrogameEnd();
        }
        loseUI.SetActive(true);
        AudioManager.instance.StopAudioEvent(LevelMusic, gameObject);
        AudioManager.instance.PlayAudioEvent(loseSFX, gameObject);
        Debug.Log("YOU LOST");
    }

    public void WinGame()
    {
        if (MicrogameManager.instance.currentlyPlayingMinigame)
        {
            MicrogameManager.instance.MicrogameEnd();
        }
        winUI.SetActive(true);
        AudioManager.instance.StopAudioEvent(LevelMusic, gameObject);
        AudioManager.instance.PlayAudioEvent(winSFX, gameObject);
        Debug.Log("YOU WIN");
    }
}
