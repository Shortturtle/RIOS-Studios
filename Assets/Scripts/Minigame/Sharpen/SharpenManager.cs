using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class SharpenManager : BaseMicrogameClass
{
    public GameObject bladeEdge;
    private Image image;
    private float currentAlpha = 1f;

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

        image = bladeEdge.GetComponent<Image>();
    }



    //count to the numberToCompleteMinigame
    public void ProgressCheck()
    {
        completion++;

        var tempColor = image.color;
        tempColor.a = currentAlpha - 0.125f;
        image.color = tempColor;
        currentAlpha = tempColor.a;

        if (completion == numberToCompleteMinigame)
        {
            EndMicrogame();
        }
    }
}
