using UnityEngine;

public class UnitSwitch : MonoBehaviour
{
    public GameObject unitBeforeQuest;
    public GameObject unitAfterQuest;

    public int questDisplayNo;

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
        if(quest.info.displayNumber == questDisplayNo)
        {
            if (quest.state.Equals(QuestState.CAN_START) || quest.state.Equals(QuestState.IN_PROGRESS))
            {
                unitBeforeQuest.SetActive(true);
                unitAfterQuest.SetActive(false);
            }
            else if (quest.state.Equals(QuestState.CAN_FINISH))
            {
                unitBeforeQuest.SetActive(false);
                unitAfterQuest.SetActive(true);
            }
        }
    }
}
