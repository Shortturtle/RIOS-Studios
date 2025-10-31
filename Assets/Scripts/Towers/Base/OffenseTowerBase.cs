using System;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;

[RequireComponent (typeof(SphereCollider))]
public class OffenseTowerBase : BaseTowerClass
{
    // Range variables
    protected float rangeValue;
    protected SphereCollider rangeSphere;

    // Target variables
    protected List<BaseEnemy> targets = new List<BaseEnemy>();
    protected GameObject currentTarget;

    protected enum TargettingModes
    {
        First, Last, Close, Strong
    }
    protected TargettingModes targettingMode = TargettingModes.First;
    protected int targetModeNum = 0;

    protected enum ChangeDirection
    {
        Next, Prev
    }

    // Damage variables
    protected float damageValue;
    protected float timeBetweenAttacks;

    // Projectile variables
    protected GameObject projectile;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Debug.Log((TargettingModes)Enum.ToObject(typeof(TargettingModes), targetModeNum));
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    protected void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Enemy") || other.gameObject.GetComponent<BaseEnemy>() != null)
        {
            targets.Add(other.GetComponent<BaseEnemy>());
        }
    }

    protected void OnTriggerExit(Collider other)
    {
        if (targets.Contains(other.GetComponent<BaseEnemy>()))
        {
            targets.Remove(other.GetComponent<BaseEnemy>());
        }
    }

    protected void ChangeTargettingMode(ChangeDirection direction)
    {
        targetModeNum = direction == ChangeDirection.Next ? targetModeNum++ : targetModeNum--;

        if (targetModeNum < 0)
        {
            targetModeNum = Enum.GetNames(typeof(TargettingModes)).Length - 1;
        }

        else if (targetModeNum > (Enum.GetNames(typeof(TargettingModes)).Length - 1))
        {
            targetModeNum = 0;
        }

        targettingMode = (TargettingModes)Enum.ToObject(typeof(TargettingModes), targetModeNum);

    }

    protected GameObject GetTargetEnemy()
    {
        switch (targettingMode)
        {
            case TargettingModes.First:
                BaseEnemy FirstInLine = null;
                float highestPercentage = 0;
                foreach(BaseEnemy option in targets)
                {
                    if (option.percentageDistance > highestPercentage)
                    {
                        FirstInLine = option;
                        highestPercentage = option.percentageDistance;
                    }
                }
                return FirstInLine.gameObject;

            case TargettingModes.Last:

                break;

            case TargettingModes.Close:

                break;

            case TargettingModes.Strong:

                break;
        }
    }
}
