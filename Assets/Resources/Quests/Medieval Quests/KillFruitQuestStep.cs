using UnityEngine;

public class KillFruitQuestStep : QuestStep
{
    //numbers for quest functionality
    private int numberOfFruitsKilled = 0;
    private int numberOfFruitsToKill = 10;


    private void Start()
    {
        //IMPORTANT FOR QUEST LOG
        UpdateState();
    }

    private void OnEnable()
    {
        GameEventManager.instance.killEvents.onKillFruit += FruitKillActionDone;
    }
    private void OnDisable()
    {
        GameEventManager.instance.killEvents.onKillFruit -= FruitKillActionDone;
    }

    //record of the number of kanades collected to progress the quest
    private void FruitKillActionDone()
    {
        if (numberOfFruitsKilled < numberOfFruitsToKill)
        {
            numberOfFruitsKilled++;
            UpdateState();
        }

        if (numberOfFruitsKilled >= numberOfFruitsToKill)
        {
            FinishedQuestStep();
        }
    }

    //call to update the state of the quest
    private void UpdateState()
    {
        //idk what this
        string state = numberOfFruitsKilled.ToString();
        //shows text in the qLog UI
        string status = "Killed " + numberOfFruitsKilled + " / " + numberOfFruitsToKill + " Enemies.";
        ChangeState(state, status);
    }

    protected override void SetQuestStepState(string state)
    {
        this.numberOfFruitsKilled = System.Int32.Parse(state);
        UpdateState();
    }
}
