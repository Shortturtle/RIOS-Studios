using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class QuestPoint : MonoBehaviour
{
    [Header("Quest")]

    //put the quest SO into this part in inspector
    [SerializeField] private QuestInfoSO questInfoForPoint;

    [Header("Config")]
    [SerializeField] private bool questStartPoint = true;
    [SerializeField] private bool questFinishPoint = true;

    private string questId;

    private QuestState currentQuestState;

    //put quest icon script as a child of this script
    private QuestIcon questIcon;

    private void Awake()
    {
        questId = questInfoForPoint.id;
        questIcon = GetComponentInChildren<QuestIcon>();
    }

    private void OnEnable()
    {
        GameEventManager.instance.questEvents.onQuestStateChange += QuestStateChange;
    }
    private void OnDisable()
    {
        GameEventManager.instance.questEvents.onQuestStateChange -= QuestStateChange;
    }

    //call this one to start the quest in the inspector
    public void ActivateQuest()
    {
        //start or finish quest
        if(currentQuestState.Equals(QuestState.CAN_START) && questStartPoint)
        {
            GameEventManager.instance.questEvents.StartQuest(questId);
        }
        else if(currentQuestState.Equals(QuestState.CAN_FINISH) && questFinishPoint)
        {
            GameEventManager.instance.questEvents.FinishQuest(questId);
        }
    }

    private void QuestStateChange(Quest quest)
    {
        if (quest.info.id.Equals(questId))
        {
            currentQuestState = quest.state;
            questIcon.SetState(currentQuestState, questStartPoint, questFinishPoint);
        }
    }
}
