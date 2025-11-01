using UnityEngine;

public class DialogueActivator : MonoBehaviour, IInteractable
{
    [SerializeField] private DialogueObject dialogueObject;

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
        player.DialogueUI.ShowDialogue(dialogueObject);
    }
}
