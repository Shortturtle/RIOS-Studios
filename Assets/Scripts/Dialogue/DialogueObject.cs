using UnityEngine;

[CreateAssetMenu(menuName = "Dialogue/Dialogue Object")]

public class DialogueObject : ScriptableObject
{
    [SerializeField] [TextArea] private string[] dialogue;

    public string[] Dialogue => dialogue; //prevents outside modification of the dialogue array

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
