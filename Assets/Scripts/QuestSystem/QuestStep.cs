using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public abstract class QuestStep : MonoBehaviour
{
    private bool isFinished = false;

    //the function for finishing quests, should be available in all the quest scripts
    protected void FinishedQuestStep()
    {
        if (!isFinished)
        {
            isFinished = true;

            // todo - advance quest forward now that step is finished

            Destroy(this.gameObject);
        }
    }
}
