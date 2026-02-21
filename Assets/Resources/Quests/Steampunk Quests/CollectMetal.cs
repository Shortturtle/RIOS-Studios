using UnityEngine;

[RequireComponent(typeof(BoxCollider))]
public class CollectMetal : QuestStep
{
    private bool done = false;
    private void Start()
    {
        string status = "Collect the metal from the furnace";
        ChangeState("", status);
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && other.TryGetComponent(out PlayerMovement player))
        {
            if (done == false) { player.DialogueUI.ShowInteractPrompt(); }

            if (Input.GetKeyDown("F"))
            {
                done = true;
                string status = "Obtained Metal";
                ChangeState("", status);
                player.DialogueUI.HideInteractPrompt();
                FinishedQuestStep();
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player") && other.TryGetComponent(out PlayerMovement player))
        {
            player.DialogueUI.HideInteractPrompt();
        }
    }

    protected override void SetQuestStepState(string state) { } //no state needed
}
