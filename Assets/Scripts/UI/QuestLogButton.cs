using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using TMPro;

public class QuestLogButton : MonoBehaviour, ISelectHandler
{
    private TextMeshProUGUI buttonText;

    private UnityAction onSelectAction;

    //because button is being instantiated and may be disabled, need to manually initialise things here
    public void Initialize(string displayName, UnityAction selectAction)
    {
        this.buttonText = this.GetComponentInChildren<TextMeshProUGUI>();

        this.onSelectAction = selectAction;
    }


    public void OnSelect(BaseEventData eventData)
    {
        onSelectAction();
    }
}
