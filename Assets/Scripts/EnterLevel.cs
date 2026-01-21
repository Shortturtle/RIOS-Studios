using UnityEngine;

public class EnterLevel : MonoBehaviour
{
    void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player") && Input.GetKeyDown(KeyCode.F))
        {
            UnityEngine.SceneManagement.SceneManager.LoadScene("TutorialLevel");
        }
    }
}
