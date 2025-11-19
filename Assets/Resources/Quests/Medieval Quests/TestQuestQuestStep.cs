using UnityEngine;

public class TestQuestQuestStep : QuestStep
{
    private int numberOfThingDone;

    private int numberNeededToComplete;


    private void OnEnable()
    {
        GameEventManager.instance.miscEvents.testAction += TestActionDone;
    }
    private void OnDisable()
    {
        GameEventManager.instance.miscEvents.testAction -= TestActionDone;
    }

    private void TestActionDone()
    {
        if(numberOfThingDone < numberNeededToComplete)
        {
            numberOfThingDone++;
        }

        if(numberOfThingDone > numberNeededToComplete)
        {
            FinishedQuestStep();
        }
    }
}
