using UnityEngine;

public class TestQuestQuestStep : QuestStep
{
    //numbers for quest functionality
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

    //record of the number of kanades collected to progress the quest
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
