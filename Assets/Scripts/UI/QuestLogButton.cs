using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using TMPro;
using UnityEditor.ShaderGraph;

public class QuestLogButton : MonoBehaviour, ISelectHandler
{
    private TextMeshProUGUI buttonText;

    private UnityAction onSelectAction;

    public Button button;

    private void Awake()
    {
        button = GetComponent<Button>();
    }

    //because button is being instantiated and may be disabled, need to manually initialise things here
    public void Initialize(string displayName, UnityAction selectAction)
    {
        this.buttonText = this.GetComponentInChildren<TextMeshProUGUI>();

        this.buttonText.text = displayName;
        this.onSelectAction = selectAction;
    }


    public void OnSelect(BaseEventData eventData)
    {
        onSelectAction();
    }

    public void SetState(QuestState state)
    {
        switch(state)
        {
            case QuestState.REQUIREMENTS_NOT_MET:
                buttonText.color = Color.grey;
                break;
            case QuestState.CAN_START:
                buttonText.color = Color.red;
                break;
            case QuestState.IN_PROGRESS:
                buttonText.color = Color.red;
                break;
            case QuestState.CAN_FINISH:
                buttonText.color = new Color(0,0,255,1);
                break;
            case QuestState.FINISHED:
                buttonText.color = new Color(50f, 180f, 0f, 1f);
                break;
            default:
                Debug.LogWarning("Quest State not recognised by switch statement: " + state);
                break;
        }
    }
}
