using UnityEngine;
using System;
using UnityEngine.SceneManagement;

public class EnterLevel : MonoBehaviour
{
    public float waitTime = 1.5f;
    public string levelToLoad;
    public PlayerMovement player;

    public void enterLevel(PlayerMovement player)
    {
        StartCoroutine(loadLevel());
    }

    private System.Collections.IEnumerator loadLevel()
    {
        //Wait for the animation to finish
        yield return new WaitForSeconds(waitTime);
        //Load the new level
        SceneManager.LoadScene(levelToLoad);
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
