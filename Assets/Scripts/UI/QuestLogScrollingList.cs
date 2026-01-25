using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class QuestLogScrollingList : MonoBehaviour
{
    [Header("Components")]
    [SerializeField] private GameObject contentParent;

    [Header("Rect Transforms")]
    [SerializeField] private RectTransform scrollRectTransform;
    [SerializeField] private RectTransform contentRectTransform;

    [Header("Quest Log Button")]
    [SerializeField] private GameObject questLogButtonPrefab;

    private Dictionary<string, QuestLogButton> idToButtonMap = new Dictionary<string, QuestLogButton>();

    //to check quest state and choose to initialize the button
    private QuestState currentQuestState;

    public QuestLogButton CreateButtonIfNotExists(Quest quest, UnityAction selectAction)
    {
        QuestLogButton questLogButton = null;

        //only create button if quest id has not been seen before
        if (!idToButtonMap.ContainsKey(quest.info.id))
        {
            questLogButton = InstantiateQuestLogButton(quest, selectAction);
        }
        else
        {
            questLogButton = idToButtonMap[quest.info.id];
        }

        //remove quest from ui if its state isnt started or can finish
        if (currentQuestState.Equals(QuestState.REQUIREMENTS_NOT_MET) || currentQuestState.Equals(QuestState.CAN_START) || currentQuestState.Equals(QuestState.FINISHED))
        {
            questLogButton = null;
        }

        return questLogButton;
    }

    private QuestLogButton InstantiateQuestLogButton(Quest quest, UnityAction selectAction)
    {
        //create button
        QuestLogButton questLogButton = Instantiate(questLogButtonPrefab, contentParent.transform).GetComponent<QuestLogButton>();

        //game object name in scene
        questLogButton.gameObject.name = quest.info.id + "_button";

        //initialize and set up function when button is selected
        RectTransform buttonRectTransform = questLogButton.GetComponent<RectTransform>();
        questLogButton.Initialize(quest.info.displayName, () => { selectAction(); UpdateScrolling(buttonRectTransform); });
        
        //add to map to keep track of button
        idToButtonMap[quest.info.id] = questLogButton;
        return questLogButton;
    }

    //for scrolling with non mouse
    private void UpdateScrolling(RectTransform buttonRectTransform)
    {
        //calc min & max for button
        float buttonYMin = Mathf.Abs(buttonRectTransform.anchoredPosition.y);
        float buttonYMax = buttonYMin + buttonRectTransform.rect.height;

        //calc min & max for content area
        float contentYMin = contentRectTransform.anchoredPosition.y;
        float contentYMax = buttonYMin + scrollRectTransform.rect.height;

        //handle scrolling down
        if(buttonYMax > contentYMax)
        {
            contentRectTransform.anchoredPosition = new Vector2(contentRectTransform.anchoredPosition.x, buttonYMax - scrollRectTransform.rect.height);
        }
        //scroll up
        else if(buttonYMin < contentYMin)
        {
            contentRectTransform.anchoredPosition = new Vector2(contentRectTransform.anchoredPosition.x,buttonYMin);
        }
    }
}
