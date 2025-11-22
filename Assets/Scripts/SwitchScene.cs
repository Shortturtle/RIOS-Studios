using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SwitchScene : MonoBehaviour
{
    public Animator PanelAnim;
    public GameObject FadePanel;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        PanelAnim.SetTrigger("Fade");
    }

    // Update is called once per frame
    void Update()
    {
        //Literally any input to switch scenes
        if (Input.anyKeyDown || Input.GetMouseButtonDown(0) || Input.GetMouseButtonDown(1) || Input.GetMouseButtonDown(2))
        {
            FadePanel.SetActive(false);
        }
    }

    public void FadeOutAndLoad(string sceneName)
    {
        //FadePanel.SetActive(true);
        //PanelAnim.SetTrigger("Idle");

        StartCoroutine(FadeAndLoad(sceneName));
    }

    private IEnumerator FadeAndLoad(string sceneName)
    {
        yield return new WaitForSeconds(1f);
        SceneManager.LoadScene(sceneName);
    }
}