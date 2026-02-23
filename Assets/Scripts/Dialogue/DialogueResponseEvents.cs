using UnityEngine;
using System;

public class DialogueResponseEvents : MonoBehaviour
{
    [SerializeField] public DialogueObject dialogueObject;
    [SerializeField] private ResponseEvent[] events;

    public DialogueObject DialogueObject => dialogueObject;
    public ResponseEvent[] Events => events;

    //Ensures that the events array matches the number of responses in the dialogue object
    public void OnValidate()
    {
        if (dialogueObject == null) return;                                                     //If no dialogue object is assigned, exit early
        if (dialogueObject.Responses == null) return;                                           //If the dialogue object has no responses, exit early
        if (events != null && events.Length == dialogueObject.Responses.Length) return;         //If the events array already exists & has the correct length, do nothing

        //Initialise || Resize the events array to match the number of responses
        if (events == null)
        {
            events = new ResponseEvent[dialogueObject.Responses.Length];
        }
        else
        {
            Array.Resize(ref events, dialogueObject.Responses.Length);
        }

        //Loop through all responses in the dialogue object
        for (int i = 0; i < dialogueObject.Responses.Length; i++)
        {
            Response response = dialogueObject.Responses[i];

            //Check: If event already exists then update the name
            if (events[i] != null)
            {
                events[i].name = response.ResponseText;
                continue;
            }

            events[i] = new ResponseEvent() {name = response.ResponseText};
        }
    }
}