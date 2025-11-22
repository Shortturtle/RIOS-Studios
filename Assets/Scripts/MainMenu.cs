using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public SwitchScene switchScenes;

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
