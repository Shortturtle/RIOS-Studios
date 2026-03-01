using UnityEngine;
using UnityEngine.Video;
using UnityEngine.SceneManagement;

public class AftCutscene : MonoBehaviour
{
    public string sceneToLoad;
    public VideoPlayer VideoPlayer;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        StartCoroutine(cutsceneFinish());
    }

    private System.Collections.IEnumerator cutsceneFinish()
    {
        yield return new WaitForSeconds((float)VideoPlayer.clip.length);
        SceneManager.LoadScene(sceneToLoad);
    }
}
