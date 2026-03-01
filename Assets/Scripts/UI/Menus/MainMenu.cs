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
        switchScenes.FadeOutAndLoad("PreTutorialCutscene");
    }

    public void Options()
    {
        StopAllMusic.Post(gameObject);
        switchScenes.FadeOutAndLoad("Credits");
    }

    public void Quit()
    {
        Application.Quit();
    }

    public void BackToMenu()
    {
        StopAllMusic.Post(gameObject);
        switchScenes.FadeOutAndLoad("Main Menu");
    }
}
