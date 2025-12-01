using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.EventSystems;

public class QuestLogUI : MonoBehaviour
{
    [Header("Components")]
    [SerializeField] private GameObject contentParent;

    [SerializeField] private QuestLogScrollingList scrollingList;

    //add more TMP if needed
    [SerializeField] private TextMeshProUGUI questDisplayNameText;
    [SerializeField] private TextMeshProUGUI questStatusText;
    [SerializeField] private TextMeshProUGUI towerRewardsText;
    [SerializeField] private TextMeshProUGUI questRequirementsText;

    private Button firstSelectedButton;

    private void OnEnable()
    {
        GameEventManager.instance.questEvents.onQuestStateChange += QuestStateChange;
    }
    private void OnDisable()
    {
        GameEventManager.instance.questEvents.onQuestStateChange -= QuestStateChange;
    }

    // ref this with the player input to toggle quest log on and off
    private void QuestLogTogglePressed()
    {
        if (contentParent.activeInHierarchy)
        {
            HideUI();
        }
        else
        {
            ShowUI();
        }
    }

    private void ShowUI()
    {
        contentParent.SetActive(true);
        //disable player movement here

        if(firstSelectedButton != null)
        {
            firstSelectedButton.Select();
        }
    }
    private void HideUI()
    {
        contentParent?.SetActive(false);
        //enable player movement here

        EventSystem.current.SetSelectedGameObject(null);
    }


    private void QuestStateChange(Quest quest)
    {
        //add the button to the scrolling list if not already added
        QuestLogButton questLogButton = scrollingList.CreateButtonIfNotExists(quest, () =>
        {
            SetQuestLogInfo(quest);
        });

        //initialize first selected button if not already so it is always the top button
        if(firstSelectedButton == null)
        {
            firstSelectedButton = questLogButton.button;
        }

        //set button colour based on quest state
        questLogButton.SetState(quest.state);
    }

    private void SetQuestLogInfo(Quest quest)
    {
        //display name
        questDisplayNameText.text = quest.info.displayName;

        //quest status
        questStatusText.text = quest.GetFullStatusText();

        //requirements (so add world requirements here if needed)
        questRequirementsText.text = "";
        foreach(QuestInfoSO prerequisiteQuestInfo in quest.info.questPrerequisites)
        {
            questRequirementsText.text += prerequisiteQuestInfo.displayName + "\n";
        }

        //rewards (HAVE TO CHANGE THIS LATER WHEN GET ACTUAL TOWER REWARD GAIN)
        towerRewardsText.text = quest.info.towerReward + "Yes";
    }
}
