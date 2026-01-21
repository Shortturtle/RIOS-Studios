using UnityEditor;
using UnityEngine;

public class JackBoxCrankManager : BaseMicrogameClass
{
    public float rotationsToComplete;
    public Crank crank;
    private float currentRotations;
    private Canvas canvas;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        StartMicrogame();
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
        
    }

    private void ProgressCheck()
    {
        if (currentRotations >= rotationsToComplete)
        {
            EndMicrogame();
        }
    }
}
