using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(DialogueResponseEvents))]
public class DialogueResponseEventsEditor : Editor
{
    //This script basically allows us to add a "Refresh" button in the inspector for DialogueResponseEvents so its more convenient to edit.
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        DialogueResponseEvents responseEvents = (DialogueResponseEvents)target;

        if (GUILayout.Button("Refresh"))
        {
            responseEvents.OnValidate();
        }
    }
}
