using UnityEngine;
using TMPro;
using System.Collections;

public class DialogueUI : MonoBehaviour
{
    [SerializeField] private GameObject dialogueBox;
    [SerializeField] private TMP_Text textLabel;
    [SerializeField] private TMP_Text textName;
    [SerializeField] public GameObject interactPrompt;

    public bool IsOpen { get; private set; }
    private Coroutine dialogueCoroutine;

    private ResponseHandler responseHandler;
    private TypewriterEffect typewriterEffect;

    private void Start()
    {
        typewriterEffect =GetComponent<TypewriterEffect>();
        responseHandler=GetComponent<ResponseHandler>();

        CloseDialogueBox();
    }

    public void ShowInteractPrompt()
    {
        Debug.Log("Showing interact prompt");
        interactPrompt.SetActive(true);
    }

    public void HideInteractPrompt()
    {
        Debug.Log("Hiding interact prompt");
        interactPrompt.SetActive(false);
    }

    public void ShowDialogue(DialogueObject dialogueObject)
    {
        interactPrompt.SetActive(false);
        IsOpen = true;
        dialogueBox.SetActive(true);
        dialogueCoroutine = StartCoroutine(RunDialogue(dialogueObject));
    }

    public void AddResponseEvents(ResponseEvent[] responseEvents)
    {
        responseHandler.AddResponseEvents(responseEvents);
    }

    private IEnumerator RunDialogue(DialogueObject dialogueObject)
    {
        //string characterName = dialogueObject.characterName;
        //textName.text = characterName;

        for (int i = 0; i < dialogueObject.Dialogue.Length; i++)
        {
            DialogueLine dialogue = dialogueObject.Dialogue[i];

            //Change speaker name per line
            textName.text = dialogue.speakerName;

            yield return RunTypingEffect(dialogue.text);
            textLabel.text = dialogue.text;

            //Wait for input release so it doesn't instantly advance
            yield return new WaitUntil(() => !Input.GetKey(KeyCode.F) || !Input.GetMouseButton(0));

            //Check: Are we at the end of the dialogue?
            if (i == dialogueObject.Dialogue.Length - 1 && dialogueObject.HasResponses) break;
            
            yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.F) || Input.GetMouseButtonDown(0)); //"||" means "or"
        }

        //If there are responses, show them
        if (dialogueObject.HasResponses)
        {
            responseHandler.ShowResponses(dialogueObject.Responses);
        }
        else
        {
            CloseDialogueBox();
        }
    }

    private IEnumerator RunTypingEffect(string dialogue)
    {
        typewriterEffect.Run(dialogue, textLabel);
        
        while (typewriterEffect.IsRunning)
        {
            yield return null;

            if (Input.GetKeyDown(KeyCode.Space) || Input.GetMouseButtonDown(0))
            {
                typewriterEffect.Stop();
            }
        }
    }

    public void CloseDialogueBox()
    {
        IsOpen = false;
        dialogueBox.SetActive(false);
        textLabel.text = string.Empty;
    }
}
