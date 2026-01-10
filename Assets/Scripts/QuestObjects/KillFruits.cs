using UnityEngine;

public class KillFruits : MonoBehaviour
{
    [Header("Quest")]
    //put the quest SO into this part in inspector
    [SerializeField] private QuestInfoSO questInfoForKill;

    private string questId;

    private QuestState currentQuestState;

    void Start()
    {
        questId = questInfoForKill.id;
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

    public void KilledFruit()
    {
        if (currentQuestState.Equals(QuestState.IN_PROGRESS))
        {
            //ref action in killevent script through GameEventManager
            GameEventManager.instance.killEvents.FruitKilled();
        }
    }
}
