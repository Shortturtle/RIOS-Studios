using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public struct DialogueLine
{
    public string speakerName;
    public Sprite speakerSprite;
    [TextArea] public string text;
}
