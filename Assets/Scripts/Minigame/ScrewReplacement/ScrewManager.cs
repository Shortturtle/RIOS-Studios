using System;
using System.Collections.Generic;
using System.Linq;
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

    private int willSpawnRusted = 3;

    private Canvas canvas;

    public int completion = 0;
    public int numberToCompleteMinigame;

    public override void StartMicrogame()
    {
        InitializeScrews();
    }

    public void Start()
    {
    }
    private void InitializeScrews()
    {
        //initializes variables
        Canvas canvas = transform.parent.GetComponent<Canvas>();

        List<int> possibleNumbers = Enumerable.Range(1, 7 - 1).ToList();
        List<int> shuffledNumbers = possibleNumbers.OrderBy(x => UnityEngine.Random.value).ToList();

        foreach (int number in shuffledNumbers)
        {
            if(willSpawnRusted > 0)
            {
                SpawnRustyScrews(number);
                willSpawnRusted--;
            }
            else
            {
                SpawnNormalScrews(number);
            }
        }
    }

    //count to the numberToCompleteMinigame
    public void ProgressCheck()
    {
        completion++;
        //if enough screws are replaced, minigame is cmplete and minigame close func is activated
        if (completion == numberToCompleteMinigame)
        {
            EndMicrogame();
            Destroy(gameObject);
        }
    }

    private void SpawnRustyScrews(int number)
    {
        if(number == 1) { Instantiate(screwRusty, screwLocation1); }
        else if(number == 2) { Instantiate(screwRusty, screwLocation2); }
        else if(number == 3) { Instantiate(screwRusty, screwLocation3); }
        else if(number == 4) { Instantiate(screwRusty, screwLocation4); }
        else if(number == 5) { Instantiate(screwRusty, screwLocation5); }
        else if(number == 6) { Instantiate(screwRusty, screwLocation6); }
    }
    private void SpawnNormalScrews(int number)
    {
        if (number == 1) { Instantiate(screwNormal, screwLocation1); }
        else if (number == 2) { Instantiate(screwNormal, screwLocation2); }
        else if (number == 3) { Instantiate(screwNormal, screwLocation3); }
        else if (number == 4) { Instantiate(screwNormal, screwLocation4); }
        else if (number == 5) { Instantiate(screwNormal, screwLocation5); }
        else if (number == 6) { Instantiate(screwNormal, screwLocation6); }
    }
}
