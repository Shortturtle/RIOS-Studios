using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class SharpenManager : BaseMicrogameClass
{
    //to decrease opacity of blade edge so blade becomes cleaner
    public GameObject bladeEdge;
    private Image image;
    private float currentAlpha = 1f;

    public int completion = 0;
    public int numberToCompleteMinigame;
    public AK.Wwise.Event AxeSharpen;

    public GameObject tickImg;

    public override void StartMicrogame()
    {
        InitializeAxe();
    }

    private void InitializeAxe()
    {
        //initializes variables
        Canvas canvas = transform.parent.GetComponent<Canvas>();

        //ref for image of blade edge
        image = bladeEdge.GetComponent<Image>();
    }



    //count to the numberToCompleteMinigame
    public void ProgressCheck()
    {
        completion++;
        AxeSharpen.Post(gameObject);
        //decrease opacity of image everytime sharpens calls this
        var tempColor = image.color;
        tempColor.a = currentAlpha - 0.125f;
        image.color = tempColor;
        currentAlpha = tempColor.a;

        if (completion == numberToCompleteMinigame)
        {
            EndMicrogame(tickImg);
        }
    }
}
