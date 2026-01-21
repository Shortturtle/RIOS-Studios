using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class SharpenManager : BaseMicrogameClass
{
    public int completion = 0;
    public int numberToCompleteMinigame;

    public override void StartMicrogame()
    {
        InitializeAxe();
    }

    private void Start()
    {
        StartMicrogame();
    }

    private void InitializeAxe()
    {
        //initializes variables
        Canvas canvas = transform.parent.GetComponent<Canvas>();

        
    }



    //count to the numberToCompleteMinigame
    public void ProgressCheck()
    {
        completion++;

        if (completion == numberToCompleteMinigame)
        {
            EndMicrogame();
        }
    }
}
