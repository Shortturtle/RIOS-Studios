using UnityEngine;

public class QuestWhetstone : MonoBehaviour
{

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
        if (quest.info.displayNumber == 3)
        {
            if (quest.state.Equals(QuestState.IN_PROGRESS))
            {
                Destroy(gameObject);
            }
        }
    }
}
