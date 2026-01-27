using UnityEngine;

public class WorldSelectManager : MonoBehaviour
{
    public SwitchScene switchScenes;
    public string sceneToLoad;
    //public AK.Wwise.Event MainMenuMusic;
    //public AK.Wwise.Event StopAllMusic;
    private void Start()
    {
        //MainMenuMusic.Post(gameObject);
    }
    public void sceneToEnter()
    {
        //StopAllMusic.Post(gameObject);
        switchScenes.FadeOutAndLoad(sceneToLoad);
    }

    public void BackToMenu()
    {
        switchScenes.FadeOutAndLoad("Main Menu");
    }

    public void BackToLobby()
    {
        //use playerprefs to check player lobby?
    }
}
