using UnityEngine;

public class GameEventManager : MonoBehaviour
{
    public static GameEventManager instance { get; private set; }

    public MiscEvents miscEvents;


    private void Awake()
    {
        if(instance != null)
        {
            Debug.LogError("More than one Game Event Manager in scene");
        }
        instance = this;

        //initialise all events
        miscEvents = new MiscEvents();
    }
}
