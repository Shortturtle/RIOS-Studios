using UnityEngine;
using UnityEngine.UI;

public class RailgunManager : BaseMicrogameClass
{
    public float barOneTarget;
    public float barTwoTarget;
    public float barThreeTarget;

    public float barOneInitial;
    public float barTwoInitial;
    public float barThreeInitial;

    public Material sliderBarBGMat;
    public Image barOneBGImage;
    public Image barTwoBGImage;
    public Image barThreeBGImage;

    private Material barOneMaterial;
    private Material barTwoMaterial;
    private Material barThreeMaterial;

    public Slider slider1;
    public Slider slider2;
    public Slider slider3;

    public float maxDifference;
    private bool isFinishedOne = false;
    private bool isFinishedTwo = false;
    private bool isFinishedThree = false;

    public int completion = 0;
    public int numberToCompleteMinigame;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        StartMicrogame();
    }

    public override void StartMicrogame()
    {
        InitializeRailgun();
    }

    private void InitializeRailgun()
    {
        //initializes variables
        Canvas canvas = transform.parent.GetComponent<Canvas>();

        CreateBarTargets();
        CreateBarInitial();

        CheckBarOneProgress();
        CheckBarTwoProgress();
        CheckBarThreeProgress();
    }

    //set bar target and green indicator
    private void CreateBarTargets()
    {
        barOneTarget = Random.Range(1, 101);
        barOneMaterial = new Material(sliderBarBGMat);
        barOneBGImage.material = barOneMaterial;
        barOneMaterial.SetFloat("_MinValue", Mathf.Clamp(((barOneTarget - maxDifference) / 100), 0f, 1f));
        barOneMaterial.SetFloat("_MaxValue", Mathf.Clamp(((barOneTarget + maxDifference) / 100), 0f, 1f));

        barTwoTarget = Random.Range(1, 101);
        barTwoMaterial = new Material(sliderBarBGMat);
        barTwoBGImage.material = barTwoMaterial;
        barTwoMaterial.SetFloat("_MinValue", Mathf.Clamp(((barTwoTarget - maxDifference) / 100), 0f, 1f));
        barTwoMaterial.SetFloat("_MaxValue", Mathf.Clamp(((barTwoTarget + maxDifference) / 100), 0f, 1f));

        barThreeTarget = Random.Range(1, 101);
        barThreeMaterial = new Material(sliderBarBGMat);
        barThreeBGImage.material = barThreeMaterial;
        barThreeMaterial.SetFloat("_MinValue", Mathf.Clamp(((barThreeTarget - maxDifference) / 100), 0f, 1f));
        barThreeMaterial.SetFloat("_MaxValue", Mathf.Clamp(((barThreeTarget + maxDifference) / 100), 0f, 1f));
    }

    //set sliders at random value
    private void CreateBarInitial()
    {
        barOneInitial = Random.Range(1, 101);
        barTwoInitial = Random.Range(1, 101);
        barThreeInitial = Random.Range(1, 101);

        slider1.value = barOneInitial;
        slider2.value = barTwoInitial;
        slider3.value = barThreeInitial;
    }

    //check bar progress, called on end drag in the inspector
    public void CheckBarOneProgress()
    {
        if(Mathf.Abs(slider1.value - barOneTarget) <= maxDifference && !isFinishedOne) 
        {
            isFinishedOne = true;
            slider1.interactable = false;
            completion++; 
        }
    }
    public void CheckBarTwoProgress()
    {
        if(Mathf.Abs(slider2.value - barTwoTarget) <= maxDifference && !isFinishedTwo) 
        {
            isFinishedTwo = true;
            slider2.interactable = false;
            completion++; 
        }
    }
    public void CheckBarThreeProgress()
    {
        if(Mathf.Abs(slider3.value - barThreeTarget) <= maxDifference && !isFinishedThree) 
        { 
            isFinishedThree = true;
            slider3.interactable = false;
            completion++; 
        }
    }

    //increase completion once bar target is reached
    public void BarTargetReached()
    {
        completion++;
        //if all sliders at target, minigame is complete and minigame close func is activated
        if (completion == numberToCompleteMinigame)
        {
            EndMicrogame();
        }
    }
}
