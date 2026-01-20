using System.Collections;
using UnityEngine;

public class ScifiFlyingEnemy : BaseEnemyClass
{
    //boost - set random time to boost after starting
    public float initialSpeed;
    public float boostSpeed;
    public float boostDuration;

    public int boostGrp1;
    public int boostGrp2;
    public int boostGrp3;

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

    private IEnumerator BoostMode()
    {
        speed = boostSpeed;
        yield return new WaitForSeconds(boostDuration);
        speed = initialSpeed;
    }
}
