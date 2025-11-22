using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public void Play()
    {
        SceneManager.LoadScene("Medieval Lobby");
    }

    public void Quit()
    {
        Application.Quit();
    }
}
