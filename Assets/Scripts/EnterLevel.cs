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
}
