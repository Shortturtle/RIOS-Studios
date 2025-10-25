using UnityEngine;
using TMPro;

public class DialogueUI : MonoBehaviour
{
    [SerializeField] private TMP_Text textLabel;

    private void Start()
    {
        GetComponent<TypewriterEffect>().Run("This is a simple dialogue system. \nThis is the next line.", textLabel);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
