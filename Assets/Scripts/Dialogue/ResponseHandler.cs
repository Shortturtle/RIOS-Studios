using UnityEngine;
using TMPro;
using UnityEngine.UI;
using NUnit.Framework;
using System.Collections.Generic;

public class ResponseHandler : MonoBehaviour
{
    [SerializeField] private RectTransform responseBox;
    [SerializeField] private RectTransform responseButtonTemplate;
    [SerializeField] private RectTransform responseContainer;

    private DialogueUI dialogueUI;

    private List<GameObject> tempResponseButtons = new List<GameObject>();

    private void Start()
    {
        dialogueUI = GetComponent<DialogueUI>();
    }

    public void ShowResponses(Response[] responses)
    {
        float responseBoxHeight = 0f;

        foreach (Response response in responses)
        {
            GameObject responseButton = Instantiate(responseButtonTemplate.gameObject, responseContainer);  //create a game obj inside the container
            responseButton.SetActive(true);                                                                 //make it visible
            responseButton.GetComponent<TMP_Text>().text = response.ResponseText;                           //set the text
            responseButton.GetComponent<Button>().onClick.AddListener(() => OnPickedResponse(response));    //add event callback when button is clicked (basically hardcoded the inspector button event)

            tempResponseButtons.Add(responseButton);                                                        //keep track of created buttons to destroy them later if needed

            responseBoxHeight += responseButtonTemplate.sizeDelta.y;
        }

        responseBox.sizeDelta = new Vector2(responseBox.sizeDelta.x, responseBoxHeight);
        responseBox.gameObject.SetActive(true);
    }

    private void OnPickedResponse(Response response)
    {
        responseBox.gameObject.SetActive(false);

        foreach (GameObject button in tempResponseButtons)
        {
            Destroy(button);
        }

        tempResponseButtons.Clear(); //clear the list

        dialogueUI.ShowDialogue(response.DialogueObject);
    }
}
