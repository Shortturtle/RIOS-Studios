using UnityEngine;
using System;
using UnityEngine.SceneManagement;
using UnityEditor.Experimental.GraphView;

public class EnterLevel : MonoBehaviour
{
    public string levelToLoad;
    private PlayerMovement player;

    public void enterLevel()
    {
        if (player != null)
        {
            StartCoroutine(loadLevel());
        }
    }

    private System.Collections.IEnumerator loadLevel()
    {
        if (player == null)
            yield break;

        //Load the new level once dialogue is finished
        while (player.dialogueUI.IsOpen)
        {
            yield return null;
        }

        Debug.Log("Loading level: " + levelToLoad);
        yield return new WaitForSeconds(0.2f);
        SceneManager.LoadScene(levelToLoad);
    }

    private void OnTriggerEnter(Collider other)
    {
        //Check: Does it have the Player tag and a PlayerMovement component?
        if (other.CompareTag("Player") && other.TryGetComponent(out PlayerMovement p))
        {
            player = p;
            player.DialogueUI.ShowInteractPrompt();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        //Check: Does it have the Player tag and a PlayerMovement component?
        if (other.CompareTag("Player") && other.TryGetComponent(out PlayerMovement p))
        {
            p.DialogueUI.HideInteractPrompt();
            player = null;
        }
    }
}
