using UnityEngine;

public class KillMushroomQuestStep : QuestStep
{
    //numbers for quest functionality
    private int numberOfMushroomsKilled = 0;
    private int numberOfMushroomsToKill = 5;

    private QuestState currentQuestState;

    private void Start()
    {
        //IMPORTANT FOR QUEST LOG
        UpdateState();
    }

    private void OnEnable()
    {
        GameEventManager.instance.killEvents.onKillMushroom += FruitKillActionDone;

        GameEventManager.instance.questEvents.onQuestStateChange += QuestStateChange;
    }
    private void OnDisable()
    {
        GameEventManager.instance.killEvents.onKillMushroom -= FruitKillActionDone;

        GameEventManager.instance.questEvents.onQuestStateChange -= QuestStateChange;
    }

    private void QuestStateChange(Quest quest)
    {
        if (quest.info.displayNumber == 2)
        {
            Debug.Log("queststatechange");
            currentQuestState = quest.state;
        }
    }

    private void FruitKillActionDone()
    {
        if (currentQuestState.Equals(QuestState.IN_PROGRESS) == false)
        {
            Debug.Log("returned");
            return;
        }
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
