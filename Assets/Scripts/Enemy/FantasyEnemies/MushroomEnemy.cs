using UnityEngine;

public class MushroomEnemy : BaseEnemyClass
{
    //cap shield gameobject to spawn
    public GameObject capShield;


    [Header("Quest")]
    //put the quest SO into this part in inspector
    [SerializeField] private QuestInfoSO questInfoForKill;

    private string questId;

    private QuestState currentQuestState;

    protected override void Start()
    {
        questId = questInfoForKill.id;
        Debug.Log(currentQuestState);
        Debug.Log(questInfoForKill.displayName);
        //Debug.Log(questInfoForKill.);
        base.Start();
    }

    private void OnEnable()
    {
        GameEventManager.instance.questEvents.onQuestStateChange += QuestStateChange;
    }
    private void OnDisable()
    {
        GameEventManager.instance.questEvents.onQuestStateChange -= QuestStateChange;
    }

    private void QuestStateChange(Quest quest)
    {
        if (quest.info.id.Equals(questId))
        {
            currentQuestState = quest.state;
        }
    }

    public override void Die()
    {
        //increase mushroom killed
        if (currentQuestState.Equals(QuestState.IN_PROGRESS))
        {
            GameEventManager.instance.killEvents.MushroomsKilled();
        }

        //spawn cap shield
        Instantiate(capShield, transform.position, Quaternion.identity);

        base.Die();
    }
}
