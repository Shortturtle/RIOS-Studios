using System.Collections.Generic;
using UnityEngine;

public class QuestManager : MonoBehaviour
{
    private Dictionary<string, Quest> questMap;

    private void Awake()
    {
        questMap = CreateQuestMap();
    }

    private void OnEnable()
    {
        GameEventManager.instance.questEvents.onStartQuest += StartQuest;
        GameEventManager.instance.questEvents.onAdvanceQuest += AdvanceQuest;
        GameEventManager.instance.questEvents.onFinishQuest += FinishQuest;

        GameEventManager.instance.questEvents.onQuestStepStateChange += QuestStepStateChange;
    }

    private void OnDisable()
    {
        GameEventManager.instance.questEvents.onStartQuest -= StartQuest;
        GameEventManager.instance.questEvents.onAdvanceQuest -= AdvanceQuest;
        GameEventManager.instance.questEvents.onFinishQuest -= FinishQuest;

        GameEventManager.instance.questEvents.onQuestStepStateChange -= QuestStepStateChange;
    }

    private void Start()
    {
        foreach(Quest quest in questMap.Values)
        {
            //initialize any loaded quest steps
            if (quest.state == QuestState.IN_PROGRESS)
            {
                quest.InstantiateCurrentQuestStep(this.transform);
            }

            //broadcast initial state of all quests on startup
            GameEventManager.instance.questEvents.QuestStateChange(quest);
        }
    }

    //call this func to update a quest state
    private void ChangeQuestState(string id, QuestState state)
    {
        Quest quest = GetQuestByID(id);
        quest.state = state;
        GameEventManager.instance.questEvents.QuestStateChange(quest);
    }


    private bool CheckRequirementsMet(Quest quest)
    {
        bool meetsRequirements = true;
        //we dont have lvl requirements so i skipped a part

        //check quest prerequisites for completion
        foreach (QuestInfoSO prerequisiteQuestInfo in quest.info.questPrerequisites)
        {
            if(GetQuestByID(prerequisiteQuestInfo.id).state != QuestState.FINISHED)  //i think is to check if quest is finished or not, then disallow quest to be restarted
            {
                meetsRequirements = false;
            }
        }

        return meetsRequirements;
    }

    private void Update()
    {
        //check through all the quests
        foreach(Quest quest in questMap.Values)
        {
            //if player meets all the requirements & quest is not started, switch quest over to the CAN_START state
            if(quest.state == QuestState.REQUIREMENTS_NOT_MET && CheckRequirementsMet(quest))
            {
                ChangeQuestState(quest.info.id, QuestState.CAN_START);
            }
                
            
        }
    }

    private void StartQuest(string id)
    {
        //get quest, instantiate current quest step under this object, and set quest to be in progress
        Quest quest = GetQuestByID(id);
        quest.InstantiateCurrentQuestStep(this.transform);
        ChangeQuestState(quest.info.id, QuestState.IN_PROGRESS);
    }
    private void AdvanceQuest(string id)
    {
        Quest quest = GetQuestByID(id);

        //move on to the next step
        quest.MoveToNextStep();

        //if there are more steps, instantiate next one
        if (quest.CurrentStepExists())
        {
            quest.InstantiateCurrentQuestStep(this.transform);
        }
        //if there are no more steps, then finished all of them for this quest
        else
        {
            ChangeQuestState(quest.info.id, QuestState.CAN_FINISH);
        }
    }
    private void FinishQuest(string id)
    {
        Quest quest = GetQuestByID(id);
        ClaimRewards(quest);
        ChangeQuestState(quest.info.id, QuestState.FINISHED);
    }

    private void ClaimRewards(Quest quest)
    {
        // imma fix this
        //fuck this shit got hands (translation -- i suck at this help)

        GameEventManager.instance.towerRewardEvents.TowerRewards(quest.info.towerReward);
    }

    private void QuestStepStateChange(string id, int stepIndex, QuestStepState questStepState)
    {
        Quest quest = GetQuestByID(id);
        quest.StoreQuestStepState(questStepState, stepIndex);
        ChangeQuestState(id, quest.state);
    }

    private Dictionary<string, Quest> CreateQuestMap()
    {
        //Loads all QuestInfoSO Scriptable Objects under Assts/Resources/Quests folder
        QuestInfoSO[] allQuests = Resources.LoadAll<QuestInfoSO>("Quests");

        Dictionary<string, Quest> idToQuestMap = new Dictionary<string, Quest>();
        foreach (QuestInfoSO questInfo in allQuests)
        {
            if (idToQuestMap.ContainsKey(questInfo.id))
            {
                Debug.LogWarning("Duplicate ID found when creating quest map" + questInfo.id);
            }
            idToQuestMap.Add(questInfo.id, LoadQuest(questInfo));
        }
        return idToQuestMap;
    }

    //to get errors when accessing quests that dont exist
    private Quest GetQuestByID(string id)
    {
        Quest quest = questMap[id];
        if(quest == null)
        {
            Debug.LogError("ID not found in the Quest Map" + id);
        }
        return quest;
    }

    private void OnApplicationQuit()
    {
        foreach (Quest quest in questMap.Values)
        {
            SaveQuest(quest);
        }
    }

    private void SaveQuest(Quest quest)
    {
        try
        {
            QuestData questData = quest.GetQuestData();
            //serialize using JsonUtility
            string serializedData = JsonUtility.ToJson(questData);
            PlayerPrefs.SetString(quest.info.id, serializedData);

            //test
            //Debug.Log(serializedData);
        }
        catch(System.Exception e)
        {
            Debug.LogError("Failed to save quest with id " + quest.info.id + ": " + e);
        }
    }

    private Quest LoadQuest(QuestInfoSO questInfo)
    {
        Quest quest = null;
        try
        {
            //load quest from saved data
            if (PlayerPrefs.HasKey(questInfo.id))
            {
                string serializedData = PlayerPrefs.GetString(questInfo.id);
                QuestData questData = JsonUtility.FromJson<QuestData>(serializedData);
                quest = new Quest(questInfo, questData.state, questData.questStepIndex, questData.questStepStates);
            }
            //else, initialize new quest
            else
            {
                quest = new Quest(questInfo);
            }
        }
        catch(System.Exception e)
        {
            Debug.LogError("Failed to load quest with id " + quest.info.id + ": " + e);
        }
        return quest;
    }
}
