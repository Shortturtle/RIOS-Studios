using UnityEngine;

public class TestQuestQuestStep : QuestStep
{
    private int numberOfThingDone = 0;

    private int numberNeededToComplete = 5;


    private void OnEnable()
    {
        GameEventManager.instance.miscEvents.onCollectKanade += TestActionDone;
    }
    private void OnDisable()
    {
        GameEventManager.instance.miscEvents.onCollectKanade -= TestActionDone;
    }

    private void TestActionDone()
    {
        if(numberOfThingDone < numberNeededToComplete)
        {
            numberOfThingDone++;
        }

        if(numberOfThingDone >= numberNeededToComplete)
        {
            FinishedQuestStep();
        }
    }
}
