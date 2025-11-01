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
    protected List<BaseEnemyClass> targets = new List<BaseEnemyClass>();
    protected GameObject currentTarget;

    public enum TargettingModes
    {
        First, Last, Close, Strong
    }
    public TargettingModes targettingMode = TargettingModes.First;
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
        rangeValue = 7;
        SetStats();
    }

    private void Awake()
    {
        rangeSphere = GetComponent<SphereCollider>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void FixedUpdate()
    {
        GetTargetEnemy();
        TrackEnemy();
    }

    protected void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Enemy") || other.gameObject.GetComponent<BaseEnemyClass>())
        {
            Debug.Log(other.gameObject.name);
            targets.Add(other.GetComponent<BaseEnemyClass>());
        }
    }

    protected void OnTriggerExit(Collider other)
    {
        if (targets.Contains(other.GetComponent<BaseEnemyClass>()))
        {
            targets.Remove(other.GetComponent<BaseEnemyClass>());
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

    protected void GetTargetEnemy()
    {
        if (targets.Count != 0)
        {
            BaseEnemyClass targetedEnemy = null;

            switch (targettingMode)
            {
                case TargettingModes.First:
                    float highestPercentage = 0;
                    foreach (BaseEnemyClass option in targets)
                    {
                        if (option.percentageDistance > highestPercentage)
                        {
                            targetedEnemy = option;
                            highestPercentage = option.percentageDistance;
                        }
                    }
                    break;

                case TargettingModes.Last:
                    float lowestPercentage = 100;
                    foreach (BaseEnemyClass option in targets)
                    {
                        if (option.percentageDistance < lowestPercentage)
                        {
                            targetedEnemy = option;
                            lowestPercentage = option.percentageDistance;
                        }
                    }
                    break;

                case TargettingModes.Close:
                    float nearestDistance = 0;
                    float currentDistance;
                    foreach (BaseEnemyClass option in targets)
                    {
                        currentDistance = Vector3.Distance(option.transform.position, this.transform.position);

                        if ((nearestDistance == 0) || (currentDistance < nearestDistance))
                        {
                            targetedEnemy = option;
                            nearestDistance = currentDistance;
                        }
                    }
                    break;

                case TargettingModes.Strong:
                    float currentHP = 0;
                    foreach (BaseEnemyClass option in targets)
                    {
                        if (currentHP < option.currentHealth)
                        {
                            targetedEnemy = option;
                            currentHP = option.currentHealth;
                        }
                    }
                    break;
            }

            currentTarget = targetedEnemy.gameObject;
        }

        else
        {
            currentTarget = null;
        }
    }

    protected void TrackEnemy()
    {
        if (currentTarget != null)
        {
            Vector3 lookAtDir = new Vector3(currentTarget.transform.position.x, transform.position.y, currentTarget.transform.position.z);
            transform.LookAt(lookAtDir);
        }
    }

    protected void SetStats()
    {
        rangeSphere.isTrigger = true;
        rangeSphere.radius = rangeValue;
    }
}
