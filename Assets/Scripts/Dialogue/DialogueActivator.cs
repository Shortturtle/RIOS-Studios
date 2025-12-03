using UnityEngine;

public class DialogueActivator : MonoBehaviour, IInteractable
{
    [SerializeField] private DialogueObject dialogueObject;

    //test for quest
    public QuestPoint questPoint;
    private void Start()
    {
        questPoint = GetComponentInChildren<QuestPoint>();
    }
    //test for quest

    public void UpdateDialogueObject(DialogueObject dialogueObject)
    {
        this.dialogueObject = dialogueObject;
    }

    private void OnTriggerEnter(Collider other)
    {
        //Check: Does it have the Player tag and a PlayerMovement component?
        if (other.CompareTag("Player") && other.TryGetComponent(out PlayerMovement player))
        {
            player.Interactable = this;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        //Check: Does it have the Player tag and a PlayerMovement component?
        if (other.CompareTag("Player") && other.TryGetComponent(out PlayerMovement player))
        {
            //Remove reference to THIS interactable
            if (player.Interactable is DialogueActivator dialogueActivator && dialogueActivator == this)
            {
                player.Interactable = null;
            }
        }
    }

    public void Interact(PlayerMovement player)
    {
        foreach(DialogueResponseEvents responseEvents in GetComponents<DialogueResponseEvents>())
        {
           if (responseEvents.DialogueObject == dialogueObject)
            {
                player.DialogueUI.AddResponseEvents(responseEvents.Events);
                break;
            }
        }
        player.DialogueUI.ShowDialogue(dialogueObject);

        if(questPoint != null)
        {
            questPoint.ActivateQuest();
        }
    }
}
