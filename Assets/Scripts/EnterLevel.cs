using UnityEngine;

public class EnterLevel : MonoBehaviour
{
    public string levelToLoad;
    void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player") && Input.GetKeyDown(KeyCode.F))
        {
            UnityEngine.SceneManagement.SceneManager.LoadScene(levelToLoad);
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
}
