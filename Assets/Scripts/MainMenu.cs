using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public SwitchScene switchScenes;
    public AK.Wwise.Event MainMenuMusic;
    public AK.Wwise.Event StopAllMusic;
    private void Start()
    {
        MainMenuMusic.Post(gameObject);
    }
    public void Play()
    {
        switchScenes.FadeOutAndLoad("Medieval Lobby");
    }

    public void Options()
    {
        switchScenes.FadeOutAndLoad("");
    }

    public void Quit()
    {
        Application.Quit();
    }
}
