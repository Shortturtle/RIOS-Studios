using UnityEngine;

public class WorldSelectManager : MonoBehaviour
{
    public SwitchScene switchScenes;
    //public AK.Wwise.Event MainMenuMusic;
    //public AK.Wwise.Event StopAllMusic;
    private void Start()
    {
        //MainMenuMusic.Post(gameObject);
    }
    public void ToWorldOne()
    {
        //StopAllMusic.Post(gameObject);
        switchScenes.FadeOutAndLoad("FantasyLobby");
    }
    public void ToWorldTwo()
    {
        //StopAllMusic.Post(gameObject);
        switchScenes.FadeOutAndLoad("Steampunk Lobby");
    }
    public void ToWorldThree()
    {
        //StopAllMusic.Post(gameObject);
        switchScenes.FadeOutAndLoad("Scifi Lobby");
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
