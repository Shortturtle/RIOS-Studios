using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public abstract class QuestStep : MonoBehaviour
{
    private bool isFinished = false;

    private string questId;

    private int stepIndex;

    //so it knows what quest it is
    public void InitializeQuestStep(string questId, int stepIndex, string questStepState)
    {
        this.questId = questId;
        this.stepIndex = stepIndex;
        if(questStepState != null && questStepState != "")
        {
            SetQuestStepState(questStepState);
        }
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

    protected void ChangeState(string newState, string newStatus)
    {
        GameEventManager.instance.questEvents.QuestStepStateChange(questId, stepIndex, new QuestStepState(newState, newStatus));
    }

    protected abstract void SetQuestStepState(string state);
}
