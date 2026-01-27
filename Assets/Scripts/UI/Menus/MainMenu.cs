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
        StopAllMusic.Post(gameObject);
        switchScenes.FadeOutAndLoad("World Menu");
    }

    public void Options()
    {
        switchScenes.FadeOutAndLoad("Options Menu");
    }

    public void Quit()
    {
        Application.Quit();
    }

    public void BackToMenu()
    {
        switchScenes.FadeOutAndLoad("Main Menu");
    }
}
