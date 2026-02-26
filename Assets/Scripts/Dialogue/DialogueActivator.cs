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

    public void UpdateDialogueObject(DialogueObject dialogueObject)
    {
        this.dialogueObject = dialogueObject;
    }

    //Essentially, when the player enters the trigger collider, set the player's Interactable to THIS SPECIFIC (game)OBJECT with the DialogueActivator script(e.g. a book, an NPC, etc)
    private void OnTriggerEnter(Collider other)
    {
        //Check: Does it have the Player tag and a PlayerMovement component?
        if (other.CompareTag("Player") && other.TryGetComponent(out PlayerMovement player))
        {
            player.Interactable = this;
            player.DialogueUI.ShowInteractPrompt();
            Debug.Log("Player entered dialogue trigger and can interact with " + gameObject.name);
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
                player.DialogueUI.HideInteractPrompt();
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
