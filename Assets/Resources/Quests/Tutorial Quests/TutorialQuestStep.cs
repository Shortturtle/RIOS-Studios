using UnityEngine;

public class TutorialQuestStep : QuestStep
{
    //numbers for quest functionality
    private int numberOfItemsCollected = 0;
    private int numberOfItemsToComplete = 3;


    private void Start()
    {
        //IMPORTANT FOR QUEST LOG
        UpdateState();
    }

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
        if (numberOfItemsCollected < numberOfItemsToComplete)
        {
            numberOfItemsCollected++;
            UpdateState();
        }

        if (numberOfItemsCollected >= numberOfItemsToComplete)
        {
            FinishedQuestStep();
        }
    }

    //call to update the state of the quest
    private void UpdateState()
    {
        //idk what this
        string state = numberOfItemsCollected.ToString();
        //shows text in the qLog UI
        string status = "Collected " + numberOfItemsCollected + " / " + numberOfItemsToComplete + " Items.";
        ChangeState(state, status);
    }

    //i have no clue what this is either fuccc
    protected override void SetQuestStepState(string state)
    {
        this.numberOfItemsCollected = System.Int32.Parse(state);
        UpdateState();
    }
}
