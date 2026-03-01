using System.Collections.Generic;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class PortalButtonManager : BaseMicrogameClass
{
    //colour img
    public GameObject theColourNeeded1;
    public GameObject theColourNeeded2;
    public GameObject theColourNeeded3;
    //colour spawn positions
    public Transform location1;
    public Transform location2;
    public Transform location3;
    public Transform location4;
    public Transform location5;

    //colour number
    private int colour1;
    private int colour2;
    private int colour3;
    private int colour4;
    private int colour5;

    private GameObject size1;
    private GameObject size2;
    private GameObject size3;
    private GameObject size4;
    private GameObject size5;

    //variables to track game state
    private int currentProgress = 0;
    public int currentButtonToPress = 0;
    public Animator animator;

    public GameObject tickImg;

    //audio
    public AK.Wwise.Event correct;
    public AK.Wwise.Event wrong;

    public override void StartMicrogame()
    {
        InitializePortalGame(); //starts game
    }

    public void InitializePortalGame()
    {
        Canvas canvas = transform.parent.GetComponent<Canvas>(); //gets current canvas for movement tracking
        SettingColours();
        SetCurrentButton();
    }

    public void SettingColours()
    {
        colour1 = Random.Range(1, 4);
        colour2 = Random.Range(1, 4);
        colour3 = Random.Range(1, 4);
        colour4 = Random.Range(1, 4);
        colour5 = Random.Range(1, 4);

        if (colour1 == 1) { size1 = Instantiate(theColourNeeded1, location1); }
        if (colour1 == 2) { size1 = Instantiate(theColourNeeded2, location1); }
        if (colour1 == 3) { size1 = Instantiate(theColourNeeded3, location1); }

        if (colour2 == 1) { size2 = Instantiate(theColourNeeded1, location2); }
        if (colour2 == 2) { size2 = Instantiate(theColourNeeded2, location2); }
        if (colour2 == 3) { size2 = Instantiate(theColourNeeded3, location2); }

        if (colour3 == 1) { size3 = Instantiate(theColourNeeded1, location3); }
        if (colour3 == 2) { size3 = Instantiate(theColourNeeded2, location3); }
        if (colour3 == 3) { size3 = Instantiate(theColourNeeded3, location3); }

        if (colour4 == 1) { size4 = Instantiate(theColourNeeded1, location4); }
        if (colour4 == 2) { size4 = Instantiate(theColourNeeded2, location4); }
        if (colour4 == 3) { size4 = Instantiate(theColourNeeded3, location4); }

        if (colour5 == 1) { size5 = Instantiate(theColourNeeded1, location5); }
        if (colour5 == 2) { size5 = Instantiate(theColourNeeded2, location5); }
        if (colour5 == 3) { size5 = Instantiate(theColourNeeded3, location5); }
    }

    public void ButtonPressWork(int buttonNumber)
    {
        if(buttonNumber == currentButtonToPress)
        {
            correct.Post(gameObject);
            SetCurrentButton();
        }
        else
        {
            wrong.Post(gameObject);
        }
    }

    private void SetCurrentButton()
    {
        currentProgress++;

        if(currentProgress == 1) { currentButtonToPress = colour1; }
        if(currentProgress == 2) { currentButtonToPress = colour2; size1.transform.localScale = new Vector3 (0.75f, 0.75f); }
        if(currentProgress == 3) { currentButtonToPress = colour3; size2.transform.localScale = new Vector3 (0.75f, 0.75f); }
        if(currentProgress == 4) { currentButtonToPress = colour4; size3.transform.localScale = new Vector3(0.75f, 0.75f); }
        if (currentProgress == 5) { currentButtonToPress = colour5; size4.transform.localScale = new Vector3(0.75f, 0.75f); }
        if (currentProgress == 6) { size5.transform.localScale = new Vector3(0.75f, 0.75f); EndMicrogame(tickImg); }
    }
}
