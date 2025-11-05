using System;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;

[RequireComponent (typeof(SphereCollider))]
public class OffenseTowerBase : BaseTowerClass
{
    // Stat Block
    public OffenseTowerStats stats;

    // Range variables
    protected SphereCollider rangeSphere;

    // Target variables
    protected List<BaseEnemyClass> targets = new List<BaseEnemyClass>();
    public GameObject currentTarget;

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
    protected float attackTimer;

    // Projectile variables
    public GameObject bulletExitPoint;
    protected GameObject projectile;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    protected virtual void Start()
    {
        Initialize();
    }

    private void Awake()
    {
        rangeSphere = GetComponent<SphereCollider>();
    }

    // Update is called once per frame
    protected virtual void Update()
    {
        GetTargetEnemy();
        TrackEnemy();
        AttackTimer();
    }

    protected virtual void FixedUpdate()
    {

    }

    protected virtual void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Enemy") || other.gameObject.GetComponent<BaseEnemyClass>())
        {
            Debug.Log(other.gameObject.name);
            targets.Add(other.GetComponent<BaseEnemyClass>());
        }
    }

    protected virtual void OnTriggerExit(Collider other)
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
            GameObject targetedEnemy = null;

            switch (targettingMode)
            {
                case TargettingModes.First:
                    float highestPercentage = 0;
                    foreach (var option in targets)
                    {
                        if (option == null)
                        {
                            targets.Remove(option);
                            return;
                        }

                        if (option.percentageDistance > highestPercentage)
                        {
                            targetedEnemy = option.gameObject;
                            highestPercentage = option.percentageDistance;
                        }
                    }
                    break;

                case TargettingModes.Last:
                    float lowestPercentage = 100;
                    foreach (var option in targets)
                    {
                        if (option == null)
                        {
                            targets.Remove(option);
                            return;
                        }

                        if (option.percentageDistance < lowestPercentage)
                        {
                            targetedEnemy = option.gameObject;
                            lowestPercentage = option.percentageDistance;
                        }
                    }
                    break;

                case TargettingModes.Close:
                    float nearestDistance = 0;
                    float currentDistance;
                    foreach (var option in targets)
                    {
                        if (option == null)
                        {
                            targets.Remove(option);
                            return;
                        }

                        currentDistance = Vector3.Distance(option.transform.position, this.transform.position);

                        if ((nearestDistance == 0) || (currentDistance < nearestDistance))
                        {
                            targetedEnemy = option.gameObject;
                            nearestDistance = currentDistance;
                        }
                    }
                    break;

                case TargettingModes.Strong:
                    float currentHP = 0;
                    foreach (var option in targets)
                    {
                        if (option == null)
                        {
                            targets.Remove(option);
                            return;
                        }

                        if (currentHP < option.currentHealth)
                        {
                            targetedEnemy = option.gameObject;
                            currentHP = option.currentHealth;
                        }
                    }
                    break;
            }

            currentTarget = targetedEnemy;
        }

        else
        {
            currentTarget = null;
        }
    }

    protected virtual void TrackEnemy()
    {
        if (currentTarget != null)
        {
            Vector3 lookAtDir = new Vector3(currentTarget.transform.position.x, transform.position.y, currentTarget.transform.position.z);
            transform.LookAt(lookAtDir);
        }
    }

    protected virtual void Initialize()
    {
        rangeSphere.isTrigger = true;
        rangeSphere.radius = stats.Range;

        damageValue = stats.Damage;

        timeBetweenAttacks = stats.TimeBetweenAttacks;

        projectile = stats.Projectile;
    }

    protected virtual void AttackTimer()
    {
        attackTimer -= Time.deltaTime;

        if (currentTarget != null && attackTimer <= 0)
            {
                Attack();
            attackTimer = timeBetweenAttacks;
            }
    }

    protected virtual void Attack()
    {
        GameObject projectileInstance = Instantiate(projectile, bulletExitPoint.transform.position, Quaternion.identity);
        projectileInstance.GetComponent<BaseProjectileClass>().InitializeProjectile(damageValue, currentTarget);
    }
}
