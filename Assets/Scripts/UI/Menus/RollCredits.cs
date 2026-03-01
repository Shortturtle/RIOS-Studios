using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class RollCredits : MonoBehaviour
{
    public GameObject ExitButton;
    public float scrollSpeed = 100f;
    public float endPosition;

    private RectTransform rect;
    private VerticalLayoutGroup layoutGroup;

    void Start()
    {
        rect = GetComponent<RectTransform>();
        layoutGroup = GetComponent<VerticalLayoutGroup>();

        ExitButton.SetActive(false);
        StartCoroutine(EnableExitButton());

        // Force layout rebuild once
        LayoutRebuilder.ForceRebuildLayoutImmediate(rect);

        // Disable layout so it stops recalculating
        layoutGroup.enabled = false;
    }

    System.Collections.IEnumerator EnableExitButton()
    {
        yield return new WaitForSeconds(5f);
        ExitButton.SetActive(true);
    }

    void Update()
    {
        rect.anchoredPosition += Vector2.up * scrollSpeed * Time.deltaTime;

        if (rect.anchoredPosition.y > endPosition)
        {
            SceneManager.LoadScene("Main Menu");
        }
    }
}