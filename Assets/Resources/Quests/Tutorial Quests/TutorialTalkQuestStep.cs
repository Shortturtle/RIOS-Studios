using UnityEngine;

public class TutorialTalkQuestStep : QuestStep
{
    //numbers for quest functionality
    private int numberOfThingDone = 0;
    private int numberNeededToComplete = 1;


    private void Start()
    {
        //IMPORTANT FOR QUEST LOG
        UpdateState();
    }

    private void OnEnable()
    {
        GameEventManager.instance.miscEvents.talkedToPeople += Interacted;
    }
    private void OnDisable()
    {
        GameEventManager.instance.miscEvents.talkedToPeople -= Interacted;
    }

    //record of the number of kanades collected to progress the quest
    private void Interacted()
    {
        if (numberOfThingDone < numberNeededToComplete)
        {
            numberOfThingDone++;
            UpdateState();
        }

        if (numberOfThingDone >= numberNeededToComplete)
        {
            FinishedQuestStep();
        }
    }

    //call to update the state of the quest
    private void UpdateState()
    {
        //idk what this
        string state = numberOfThingDone.ToString();
        string status = "Talked to " + numberOfThingDone + " / " + numberNeededToComplete + " people in tavern.";
        ChangeState(state, status);
    }

    //i have no clue what this is either fuccc
    protected override void SetQuestStepState(string state)
    {
        this.numberOfThingDone = System.Int32.Parse(state);
        UpdateState();
    }
}
