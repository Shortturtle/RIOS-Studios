using UnityEngine;

public class ScrewManager : BaseMicrogameClass
{
    public Transform screwLocation1;
    public Transform screwLocation2;
    public Transform screwLocation3;
    public Transform screwLocation4;
    public Transform screwLocation5;
    public Transform screwLocation6;

    public GameObject screwNormal;
    public GameObject screwRusty;

    private Canvas canvas;

    public override void StartMicrogame()
    {
        InitializeScrews();
    }

    public void Start()
    {
        StartMicrogame();
    }
    private void InitializeScrews()
    {
        //initializes wire variables
        Canvas canvas = transform.parent.GetComponent<Canvas>();


    }
}
