using UnityEngine;

public class EnterLevel : MonoBehaviour, IInteractable
{
    public string levelToLoad;

    void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player") && other.TryGetComponent(out PlayerMovement player))
        {
            if (CompareTag("Player") && Input.GetKeyDown(KeyCode.F))
            {
                player.Interactable = this;
                UnityEngine.SceneManagement.SceneManager.LoadScene(levelToLoad);
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        //Check: Does it have the Player tag and a PlayerMovement component?
        if (other.CompareTag("Player") && other.TryGetComponent(out PlayerMovement player))
        {
            player.DialogueUI.ShowInteractPrompt();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        //Check: Does it have the Player tag and a PlayerMovement component?
        if (other.CompareTag("Player") && other.TryGetComponent(out PlayerMovement player))
        {
            player.DialogueUI.HideInteractPrompt();
        }
    }

    void IInteractable.Interact(PlayerMovement player)
    {
        throw new System.NotImplementedException();
    }
}
