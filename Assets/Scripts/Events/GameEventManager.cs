using UnityEngine;

public class GameEventManager : MonoBehaviour
{
    public static GameEventManager instance { get; private set; }

    public MiscEvents miscEvents;

    public QuestEvents questEvents;

    public TowerRewardEvents towerRewardEvents;

    private void Awake()
    {
        if(instance != null)
        {
            Debug.LogError("More than one Game Event Manager in scene");
        }
        instance = this;

        //initialise all events (for scripts relating to events n actions)
        miscEvents = new MiscEvents();
        questEvents = new QuestEvents();
        towerRewardEvents = new TowerRewardEvents();
    }
}
