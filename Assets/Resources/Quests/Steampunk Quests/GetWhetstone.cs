using UnityEngine;

[RequireComponent(typeof(BoxCollider))]
public class GetWhetstone : QuestStep
{
    private bool done = false;
    private void Start()
    {
        string status = "Find the whetstone";
        ChangeState("", status);
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && other.TryGetComponent(out PlayerMovement player))
        {
            if(done == false) { player.DialogueUI.ShowInteractPrompt(); }
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player") && other.TryGetComponent(out PlayerMovement player))
        {
            Debug.Log("range to collect whetstone");
            if (Input.GetKeyDown(KeyCode.F) && done == false)
            {
                done = true;
                string status = "Got the whetstone!";
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
