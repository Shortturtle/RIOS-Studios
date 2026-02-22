using UnityEngine;

public class BartenderSwitch : MonoBehaviour
{
    public GameObject bartenderNoQuest;
    public GameObject bartenderYesQuest;

    public Quest talkQuest;

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
        if(quest.info.displayNumber == 1)
        {
            if (quest.state.Equals(QuestState.IN_PROGRESS) || quest.state.Equals(QuestState.CAN_FINISH))
            {
                bartenderYesQuest.SetActive(true);
                bartenderNoQuest.SetActive(false);
            }
            else
            {
                bartenderYesQuest.SetActive(false);
                bartenderNoQuest.SetActive(true);
            }
        }
    }
}
