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

    public Slider slider1;
    public Slider slider2;
    public Slider slider3;

    public float maxDifference;
    private bool isFinished = false;

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

    private void CreateBarTargets()
    {
        barOneTarget = Random.Range(0, 101);
        barTwoTarget = Random.Range(0, 101);
        barThreeTarget = Random.Range(0, 101);
    }

    private void CreateBarInitial()
    {
        barOneInitial = Random.Range(0, 101);
        barTwoInitial = Random.Range(0, 101);
        barThreeInitial = Random.Range(0, 101);

        slider1.value = barOneInitial;
        slider2.value = barTwoInitial;
        slider3.value = barThreeInitial;
    }

    public void CheckBarOneProgress()
    {
        if(Mathf.Abs(slider1.value - barOneTarget) <= maxDifference && !isFinished) 
        {
            isFinished = true;
            slider1.interactable = false;
            completion++; 
        }
    }
    public void CheckBarTwoProgress()
    {
        if(Mathf.Abs(slider2.value - barTwoTarget) <= maxDifference && !isFinished) 
        {
            isFinished = true;
            slider2.interactable = false;
            completion++; 
        }
    }
    public void CheckBarThreeProgress()
    {
        if(Mathf.Abs(slider3.value - barThreeTarget) <= maxDifference && !isFinished) 
        { 
            isFinished = true;
            slider3.interactable = false;
            completion++; 
        }
    }

    public void BarTargetReached()
    {
        completion++;
        //if all sliders at target, minigame is complete and minigame close func is activated
        if (completion == numberToCompleteMinigame)
        {
            EndMicrogame();
            Destroy(gameObject);
        }
    }
}
