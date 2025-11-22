using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TypewriterEffect : MonoBehaviour
{
    [SerializeField] private float typeSpeed = 50f;

    public bool IsRunning { get; private set; }

    private readonly Dictionary<HashSet<char>, float> punctuations = new Dictionary<HashSet<char>, float>()
    {
        {new HashSet<char> {'.', '!', '?'}, 0.6f},
        {new HashSet<char> {',', ';', ':'}, 0.3f}
    };

    private Coroutine typingCoroutine;

    //Responsible for running the code
    public void Run(string textToType, TMP_Text textLabel)                      //(the string we wanna type, the text label we wanna type it into)
    {
        typingCoroutine = StartCoroutine(TypeText(textToType, textLabel));
    }

    public void Stop()                                                        
    {
        StopCoroutine(typingCoroutine);
        IsRunning = false;
    }

    private IEnumerator TypeText(string textToType, TMP_Text textLabel)         //Responsible for the typewriter effect
    {
        IsRunning = true;
        textLabel.text = string.Empty;                                          //clear the text label at the start

        //measures how many characters we type on screen at the given frame
        float t = 0; 
        int charIndex = 0;

        while (charIndex < textToType.Length)                                   //while typing is in progress
        {
            int lastCharIndex = charIndex - 1;                                  //get the last character index we typed

            t += Time.deltaTime * typeSpeed;                                    //shows each letter based on the typing speed (and time passsed)
            charIndex = Mathf.FloorToInt(t);                                    //get the integer value of t (e.g. 5.9 = 5, 2.3 = 2, etc)
            charIndex = Mathf.Clamp(charIndex, 0, textToType.Length);           //clamp charIndex to be within the bounds of the string length

            //Check: if we have typed any punctuation since the last frame, if the character types is the last, and if the next chara is a punctuation. If yes, wait for the specified time (essentially keep a consistent frame rate)
            for (int i = lastCharIndex + 1; i < charIndex; i++)                 
            {
                bool isLast = i >= textToType.Length - 1;

                textLabel.text = textToType.Substring(0, i + 1);

                if (IsPunctuation(textToType[i], out float waitTime) && !isLast && !IsPunctuation(textToType[i + 1], out _))
                {
                    yield return new WaitForSeconds(waitTime);
                }
            }

            textLabel.text = textToType.Substring(0, charIndex);                //set the text label to the substring of the text we want to type
            
            yield return null;                                                  //wait for the next frame
        }

        IsRunning = false;
    }

    //Check: if the character is a punctuation then return the wait time
    private bool IsPunctuation(char character, out float waitTime)
    {
        foreach (KeyValuePair<HashSet<char>, float> punctuationCategory in punctuations)
        {
            if (punctuationCategory.Key.Contains(character))
            {
                waitTime = punctuationCategory.Value;
                return true;
            }
        }

        waitTime = default;
        return false;
    }
}
