using System.Collections;
using TMPro;
using UnityEngine;

public class TypewriterEffect : MonoBehaviour
{
    [SerializeField] private float typeSpeed = 50f;

    //Responsible for running the code
    public void Run(string textToType, TMP_Text textLabel)                      //(the string we wanna type, the text label we wanna type it into)
    {
        StartCoroutine(TypeText(textToType, textLabel));
    }

    private IEnumerator TypeText(string textToType, TMP_Text textLabel)         //Responsible for the typewriter effect
    {
        textLabel.text = string.Empty;                                          //clear the text label at the start
        yield return new WaitForSeconds(1);                                     //wait for 1 seconds before starting to type

        //measures how many characters we type on screen at the given frame
        float t = 0; 
        int charIndex = 0;

        while (charIndex < textToType.Length)                                   //while typing is in progress
        {
            t += Time.deltaTime * typeSpeed;                                    //shows each letter based on the typing speed (and time passsed)
            charIndex = Mathf.FloorToInt(t);                                    //get the integer value of t (e.g. 5.9 = 5, 2.3 = 2, etc)
            charIndex = Mathf.Clamp(charIndex, 0, textToType.Length);           //clamp charIndex to be within the bounds of the string length
            
            textLabel.text = textToType.Substring(0, charIndex);                //set the text label to the substring of the text we want to type
            
            yield return null;                                                  //wait for the next frame
        }

        textLabel.text = textToType;                                            //ensure the full text is displayed at the end
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
