using System.Collections.Generic;
using UnityEngine;

public class QuestManager : MonoBehaviour
{
    //untick from inspector if u dont want quests to be loaded
    [Header("Config")]
    [SerializeField] private bool loadQuestState = true;

    //world unlocked requirements
    private string worldPlayerUnlocked;

    public static QuestManager instance;

    private Dictionary<string, Quest> questMap;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }

        

        questMap = CreateQuestMap();

        PlayerPrefs.SetInt(worldPlayerUnlocked, 0);
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
        foreach (Quest quest in questMap.Values)
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
        if(PlayerPrefs.GetInt(worldPlayerUnlocked) < quest.info.worldUnlockRequirement)
        {
            meetsRequirements = false;
        }

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
            if (quest.state == QuestState.REQUIREMENTS_NOT_MET && CheckRequirementsMet(quest))
            {
                Debug.Log("change");
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

        SaveQuest(quest);
        Debug.Log(quest.state, quest.info);
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

        SaveQuest(quest);
        Debug.Log(quest.state, quest.info);
    }
    private void FinishQuest(string id)
    {
        Quest quest = GetQuestByID(id);
        ClaimRewards(quest);
        ChangeQuestState(quest.info.id, QuestState.FINISHED);

        SaveQuest(quest);
        Debug.Log(quest.state, quest.info);
    }

    private void ClaimRewards(Quest quest)
    {
        // this will change after finalising how to store towerRewards

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
            Debug.Log("before load quest, trying to add");
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
        if(questMap.Values == null) { Debug.Log("no quest values"); return; }
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
            //PlayerPrefs.SetString(quest.info.id, serializedData);
            currentQuestId = quest.info.displayNumber;
            Debug.Log("serialzised data = " + serializedData + "quest id = " + currentQuestId);
            questDataToLoad = serializedData;
            SaveSystem.Save();

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
            currentQuestId = questInfo.displayNumber;
            SaveSystem.Load();
            //load quest from saved data
            if (/*PlayerPrefs.HasKey(questInfo.id) && */questDataToLoad != null && loadQuestState)
            {
                string serializedData = questDataToLoad;
                QuestData questData = JsonUtility.FromJson<QuestData>(serializedData);

                if (questData == null)
                {
                    Debug.LogWarning($"Failed to parse saved quest data for {questInfo.id}, raw data: {serializedData}");
                    quest = new Quest(questInfo);
                }
                else
                {
                    quest = new Quest(questInfo, questData.state, questData.questStepIndex, questData.questStepStates);
                }

                ////string serializedData = PlayerPrefs.GetString(questInfo.id);
                //string serializedData = questDataToLoad;
                //Debug.Log(serializedData);
                //QuestData questData = JsonUtility.FromJson<QuestData>(serializedData);
                //quest = new Quest(questInfo, questData.state, questData.questStepIndex, questData.questStepStates);
            }
            //else, initialize new quest
            else
            {
                quest = new Quest(questInfo);
                Debug.Log("new quest questInfo");
            }
        }
        catch(System.Exception e)
        {
            Debug.LogError("Failed to load quest with id " + questInfo.id + ": " + e);
        }
        return quest;
    }

    public int currentQuestId;
    public string questDataToLoad;

    #region Save and Load
    public void Save(ref QuestSaveData data)
    {
        Debug.Log("Save");
        Debug.Log(data.questSaveDataList);
        if (data.questSaveDataList.Count == 0) { data.questSaveDataList = new List<string> { "", "", "", "", "", "", "", ""}; }  //if list created but no numbers, create list with all 9 ints
        data.questSaveDataList[currentQuestId] = questDataToLoad;  //save reward gained to specific number based on scene number in this(reward manager)
    }

    public void Load(QuestSaveData data)
    {
        Debug.Log("Load");
        if (data.questSaveDataList.Count == 0) { data.questSaveDataList = new List<string> { "", "", "", "", "", "", "", "" }; }  //if list created but no numbers, create list with all 9 ints
        questDataToLoad = data.questSaveDataList[currentQuestId];
        if(questDataToLoad == "")
        {
            questDataToLoad = null;
            Debug.Log("set to null");
        }
    }

    #endregion


    public void ResetQuestSaveData(ref QuestSaveData data)
    {
        data.questSaveDataList = new List<string> { "", "", "", "", "", "", "", ""};
    }
}

//save system part
[System.Serializable]

public struct QuestSaveData
{
    public List<string> questSaveDataList;
}