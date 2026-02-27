using UnityEngine;

[RequireComponent(typeof(BoxCollider))]
public class CollectRoofBoxes : QuestStep
{
    private bool done = false;
    private void Start()
    {
        string status = "Grab Neiro's boxes from the roof";
        ChangeState("", status);
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && other.TryGetComponent(out PlayerMovement player))
        {
            if (done == false) { player.DialogueUI.ShowInteractPrompt(); }
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player") && other.TryGetComponent(out PlayerMovement player))
        {
            if (Input.GetKeyDown(KeyCode.F) && done == false)
            {
                done = true;
                string status = "Boxes grabbed";
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
