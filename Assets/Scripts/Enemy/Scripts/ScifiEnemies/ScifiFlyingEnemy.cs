using System.Collections;
using UnityEngine;

public class ScifiFlyingEnemy : BaseEnemyClass
{
    //boost - set random time to boost after starting
    public float initialSpeed;
    public float boostSpeed;
    public float boostDuration;

    public int minBoostTime;
    public int maxBoostTime1;
    public int maxBoostTime2;
    public int maxBoostTime3;


    protected override void Start()
    {
        initialSpeed = speed;

        StartCoroutine("BoostStart");

        base.Start();
    }

    private IEnumerator BoostStart()
    {
        int boostGroup = Random.Range(1, 4);
        if (boostGroup == 1)
        {
            int beforeBoost = Random.Range(minBoostTime, maxBoostTime1);
            yield return new WaitForSeconds(beforeBoost);
            StartCoroutine("BoostMode");
        }
        else if (boostGroup == 2)
        {
            int beforeBoost = Random.Range(maxBoostTime1, maxBoostTime2);
            yield return new WaitForSeconds(beforeBoost);
            StartCoroutine("BoostMode");
        }
        else if (boostGroup == 3)
        {
            int beforeBoost = Random.Range(maxBoostTime2, maxBoostTime3);
            yield return new WaitForSeconds(beforeBoost);
            StartCoroutine("BoostMode");
        }
    }

    private IEnumerator BoostMode()
    {
        speed = boostSpeed;
        yield return new WaitForSeconds(boostDuration);
        speed = initialSpeed;
    }
}
