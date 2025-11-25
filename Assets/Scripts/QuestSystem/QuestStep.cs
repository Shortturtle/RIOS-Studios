using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public abstract class QuestStep : MonoBehaviour
{
    private bool isFinished = false;

    private string questId;

    //so it knows what quest it is
    public void InitializeQuestStep(string questId)
    {
        this.questId = questId;
    }

    //the function for finishing quests, should be available in all the quest scripts
    protected void FinishedQuestStep()
    {
        if (!isFinished)
        {
            isFinished = true;

            //next step of quest
            GameEventManager.instance.questEvents.AdvanceQuest(questId);

            Destroy(this.gameObject);
        }
    }
}
