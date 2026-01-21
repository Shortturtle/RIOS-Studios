using UnityEngine;

[System.Serializable]
public struct DialogueLine
{
    public string speakerName;
    [TextArea] public string text;
}
