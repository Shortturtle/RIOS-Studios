using UnityEngine;

[CreateAssetMenu(menuName = "Dialogue/Dialogue Object")]

public class DialogueObject : ScriptableObject
{
    //[SerializeField] public string characterName;
    [SerializeField] private DialogueLine[] dialogue;
    [SerializeField] private Response[] responses;

    public DialogueLine[] Dialogue => dialogue; //prevents outside modification of the dialogue array

    public bool HasResponses => Responses != null && Responses.Length > 0; //Determines if there are any responses available and if they are more than 0 (essentially idiot proofs it)

    public Response[] Responses => responses; //prevents outside modification of the responses array
}
