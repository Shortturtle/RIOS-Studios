using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SwitchScene : MonoBehaviour
{
    public Animator PanelAnim;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        PanelAnim.SetTrigger("Fade");
    }

    public void FadeIntoNewScene(string sceneName)
    {
        PanelAnim.SetTrigger("Idle");
        StartCoroutine(switchScene(sceneName));
    }

    IEnumerator switchScene(string sceneName)
    {
        yield return new WaitForSeconds(1.5f);
        SceneManager.LoadScene(sceneName);
    }

    // Update is called once per frame
    void Update()
    {
        //For testing purposes: Switch scenes with arrow keys
        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            FadeIntoNewScene("Danish Scene"); //this is the main line of code to switch scenes
        }
    }
}