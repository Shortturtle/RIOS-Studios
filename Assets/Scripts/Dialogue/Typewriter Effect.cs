using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TypewriterEffect : MonoBehaviour
{
    [SerializeField] private float typeSpeed = 50f;

    public bool IsRunning { get; private set; }

    private readonly List<Punctuation> punctuations = new List<Punctuation>()
    {
        new Punctuation(new HashSet<char> {'.', '!', '?'}, 0.6f),
        new Punctuation(new HashSet<char> { ',', ';', ':' }, 0.3f),
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
            //New Typewriter Effect Code
            t += Time.deltaTime * typeSpeed;
            int targetIndex = Mathf.FloorToInt(t);

            while (charIndex < targetIndex && charIndex < textToType.Length)
            {
                //Ignore the stuff inbetween "<" and ">" (this is for the rich text tags/when u wanna change the style mid sentence)
                if (textToType[charIndex] == '<')
                {
                    int tagEnd = textToType.IndexOf('>', charIndex);
                    if (tagEnd != -1)
                    {
                        textLabel.text += textToType.Substring(charIndex, tagEnd - charIndex + 1);
                        charIndex = tagEnd + 1;
                        continue;
                    }
                }

                char currentChar = textToType[charIndex];
                textLabel.text += currentChar;

                bool isLast = charIndex >= textToType.Length - 1;

                if (IsPunctuation(currentChar, out float waitTime)
                    && !isLast
                    && !IsPunctuation(textToType[charIndex + 1], out _))
                {
                    yield return new WaitForSeconds(waitTime);
                }
                
                charIndex++;
            }

            yield return null;                                                  //wait for the next frame
        }

        IsRunning = false;
    }

    //Check: if the character is a punctuation then return the wait time
    private bool IsPunctuation(char character, out float waitTime)
    {
        foreach (Punctuation punctuationCategory in punctuations)
        {
            if (punctuationCategory.Punctuations.Contains(character))
            {
                waitTime = punctuationCategory.WaitTime;
                return true;
            }
        }

        waitTime = default;
        return false;
    }

    private readonly struct Punctuation
    {
        public readonly HashSet<char> Punctuations;
        public readonly float WaitTime;
        public Punctuation(HashSet<char> punctuations, float waitTime)
        {
            Punctuations = punctuations;
            WaitTime = waitTime;
        }
    }
}
