using UnityEditor;
using UnityEngine;

public class JackBoxCrankManager : BaseMicrogameClass
{
    public float rotationsToComplete;
    public Crank crank;
    public float currentRotations;
    private Canvas canvas;

    public GameObject tickImg;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
    }

    public override void StartMicrogame()
    {
        InitializeCrank();
    }

    private void InitializeCrank()
    {
        Canvas canvas = transform.parent.GetComponent<Canvas>();

        crank.canvas = canvas;
        crank.jackBoxCrankManager = this;
    }

    // Update is called once per frame
    void Update()
    {
        ProgressCheck();
    }

    private void ProgressCheck()
    {
        if (Mathf.Abs(currentRotations) >= rotationsToComplete)
        {
            EndMicrogame(tickImg);
        }
    }
}
