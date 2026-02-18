using UnityEngine;

public class StartFurnace : QuestStep
{
    //numbers for quest functionality
    private int numberOfMushroomsKilled = 0;
    private int numberOfMushroomsToKill = 10;


    private void Start()
    {
        //IMPORTANT FOR QUEST LOG
        UpdateState();
    }

    private void OnEnable()
    {
        GameEventManager.instance.killEvents.onKillMushroom += FruitKillActionDone;
    }
    private void OnDisable()
    {
        GameEventManager.instance.killEvents.onKillMushroom -= FruitKillActionDone;
    }

    //record of the number of kanades collected to progress the quest
    private void FruitKillActionDone()
    {
        if (numberOfMushroomsKilled < numberOfMushroomsToKill)
        {
            numberOfMushroomsKilled++;
            UpdateState();
        }

        if (numberOfMushroomsKilled >= numberOfMushroomsToKill)
        {
            FinishedQuestStep();
        }
    }

    //call to update the state of the quest
    private void UpdateState()
    {
        //idk what this
        string state = numberOfMushroomsKilled.ToString();
        //shows text in the qLog UI
        string status = "Killed " + numberOfMushroomsKilled + " / " + numberOfMushroomsToKill + " Enemies.";
        ChangeState(state, status);
    }

    protected override void SetQuestStepState(string state)
    {
        this.numberOfMushroomsKilled = System.Int32.Parse(state);
        UpdateState();
    }
}
