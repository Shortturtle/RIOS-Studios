using UnityEngine;

public class LobbyAudioPlayer : MonoBehaviour
{
    public AK.Wwise.Event LevelMusic;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        PlayLevelMusic();
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
