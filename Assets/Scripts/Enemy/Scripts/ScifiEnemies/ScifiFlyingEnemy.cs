using System.Collections;
using UnityEngine;

public class ScifiFlyingEnemy : BaseEnemyClass
{
    //boost - set random time to boost after starting
    public float initialSpeed;
    public float boostSpeed;
    public float boostDuration;

    //choose which part of the track to boost at
    public int boostGrp1;
    public int boostGrp2;
    public int boostGrp3;

    protected override void Start()
    {
        //set a initial speed so they can return to original speed
        initialSpeed = speed;

        StartCoroutine("BoostStart");

        base.Start();
    }

    //coroutine for boosting
    private IEnumerator BoostStart()
    {
        //randomly choose which boost group it will be in
        int boostGroup = Random.Range(1, 4);

        //based on boost grp chosen, wait for a certain amt of time before boosting
        if (boostGroup == 1)
        {
            yield return new WaitForSeconds(boostGrp1);
            Debug.Log("bg1");
            StartCoroutine("BoostMode");
        }
        else if (boostGroup == 2)
        {
            yield return new WaitForSeconds(boostGrp2);
            Debug.Log("bg2");
            StartCoroutine("BoostMode");
        }
        else if (boostGroup == 3)
        {
            yield return new WaitForSeconds(boostGrp3);
            Debug.Log("bg3");
            StartCoroutine("BoostMode");
        }
    }

    //boost enemy speed for a while, then bring it back to initial speed
    private IEnumerator BoostMode()
    {
        speed = boostSpeed;
        yield return new WaitForSeconds(boostDuration);
        speed = initialSpeed;
    }
}
