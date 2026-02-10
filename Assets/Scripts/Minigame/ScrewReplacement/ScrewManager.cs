using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ScrewManager : BaseMicrogameClass
{
    //all locations on canvas for screws to spawn
    public Transform screwLocation1;
    public Transform screwLocation2;
    public Transform screwLocation3;
    public Transform screwLocation4;
    public Transform screwLocation5;
    public Transform screwLocation6;

    //types of screws
    public GameObject screwNormal;
    public GameObject screwRusty;

    //how many rusted screws will spawn
    private int willSpawnRusted = 3;

    private Canvas canvas;

    public int completion = 0;
    public int numberToCompleteMinigame;

    public GameObject tickImg;

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
        //initializes variables
        Canvas canvas = transform.parent.GetComponent<Canvas>();

        //creates list from 1-6 and shuffles it
        List<int> possibleNumbers = Enumerable.Range(1, 7 - 1).ToList();
        List<int> shuffledNumbers = possibleNumbers.OrderBy(x => UnityEngine.Random.value).ToList();

        //for each number first spawn rusty screws, then once 3 are spawned it spawns normal screws for the rest
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
            EndMicrogame(tickImg);
        }
    }

    //spawns screws at location based on the number randomised
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
