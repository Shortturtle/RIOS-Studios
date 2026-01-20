using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;

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

    public enum TargettingModes
    {
        First, Last, Close, Strong
    }
    public TargettingModes targettingMode = TargettingModes.First;
    protected int targetModeNum = 0;

    public GameObject MoveableTarget;

    protected enum ChangeDirection
    {
        Next, Prev
    }

    // Damage variables
    [HideInInspector] public float damageBase;
    [HideInInspector] public float timeBetweenAttacksBase;
    [HideInInspector] public float rangeBase;
    [HideInInspector] public float damageValue;
    [HideInInspector] public float timeBetweenAttackValue;
    [HideInInspector] public float rangeValue;
    [HideInInspector] public bool canAttackFlying;
    protected float attackTimer;

    // Projectile variables
    public GameObject bulletExitPoint;
    protected GameObject projectile;

    //UI Variables
    public GameObject hoverUI;
    protected GameObject hoverUIInstance;
    public GameObject hoverUIPosition;
    public GameObject statsUI;
    protected bool clickable;

    //Audio events
    protected AK.Wwise.Event attackEvent;
    protected AK.Wwise.Event degradeEvent;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    protected virtual void Start()
    {
        InitializeTower();
    }

    private void Awake()
    {
        rangeSphere = GetComponent<SphereCollider>();
    }

    // Update is called once per frame
    protected virtual void Update()
    {
        if (isStunned)
        {
            return;
        }
        GetTargetEnemy();
        TrackEnemy();
        AttackTimer();
        GeneralDegradeTracker();
        HoverUIHandler();
    }
    public void Stun(float duration)
    {
        StartCoroutine(StunRoutine(duration));
    }

    private IEnumerator StunRoutine(float duration)
    {
        isStunned = true;
        yield return new WaitForSeconds(duration);
        isStunned = false;
    }

    protected virtual void FixedUpdate()
    {
    }

    protected virtual void OnTriggerStay(Collider other)
    {
        if (other.gameObject.CompareTag("Enemy") || other.gameObject.GetComponent<BaseEnemyClass>())
        {
            BaseEnemyClass tempEnemy = other.gameObject.GetComponent<BaseEnemyClass>();

            if(tempEnemy.isFlying && !canAttackFlying)
            {
                return;
            }

            if (!targets.Contains(other.GetComponent<BaseEnemyClass>()))
            {
                targets.Add(other.GetComponent<BaseEnemyClass>());
            }
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
                    float lowestPercentage = 999;
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
            Vector3 lookAtDir = new Vector3(currentTarget.transform.position.x, MoveableTarget.transform.position.y, currentTarget.transform.position.z);
            MoveableTarget.transform.LookAt(lookAtDir);
        }
    }

    public override void InitializeTower()
    {

        damageBase = stats.Damage;
        damageValue = damageBase;

        timeBetweenAttacksBase = stats.TimeBetweenAttacks;
        timeBetweenAttackValue = timeBetweenAttacksBase;

        rangeBase = stats.Range;
        rangeValue = rangeBase;
        rangeSphere.isTrigger = true;
        rangeSphere.radius = rangeValue;

        projectile = stats.Projectile;

        canAttackFlying = stats.canAttackFlying;

        cost = stats.Cost;

        degradeTimerDuration = stats.DegradeTimerDuration;
        degradeCountdownTimer = degradeTimerDuration;
        overdriveTimerDuration = stats.OverdriveTimerDuration;
        bufferTimerDuration = stats.BufferTimerDuration;
        degradeRank = 0;
        maxDegradeRank = stats.MaxDegradeRank;

        microgame = stats.Microgame;

        attackEvent = stats.AttackEvent;
        degradeEvent = stats.DegradeEvent;
    }

    protected virtual void AttackTimer()
    {
        attackTimer -= Time.deltaTime;

        if (currentTarget != null && attackTimer <= 0)
            {
                Attack();
            attackTimer = timeBetweenAttackValue;
            }
    }

    protected virtual void Attack()
    {
        attackEvent.Post(this.gameObject);
        GameObject projectileInstance = Instantiate(projectile, bulletExitPoint.transform.position, Quaternion.identity);
        projectileInstance.GetComponent<BaseProjectileClass>().InitializeProjectile(damageValue, currentTarget, currentTarget.transform.position);
    }

    protected override void MaxDegradeTracker()
    {
        if (degradeRank == maxDegradeRank && !isMaxDegraded)
        {
            isMaxDegraded = true;
            degradeEvent.Post(this.gameObject);
            degradeSign.SetActive(true);
        }

        else if (!isMaxDegraded)
        {
            degradeSign.SetActive(false);
        }
    }

    protected override void Degrade()
    {
        timeBetweenAttackValue = timeBetweenAttacksBase * (1 + ((float)(degradeRank + 1f)/(float)maxDegradeRank));
        degradeRank++;
        ResetDegradeTimer();
    }

    public override void RepairTower()
    {
        OverDrive();
        degradeRank = 0;
        isMaxDegraded = false;
        isOverdrive = true;
    }

    protected override void OverDrive()
    {
        timeBetweenAttackValue = timeBetweenAttacksBase / 4;
        attackTimer = 0;
        overdriveCountdownTimer = overdriveTimerDuration;
    }

    protected override void OverDriveEnd()
    {
        base.OverDriveEnd();
        timeBetweenAttackValue = timeBetweenAttacksBase;
    }

    public override void HoverUIHandler()
    {
        if (hoverUIInstance  != null)
        {
            FixHoverUIPosition();
        }

        if (isHovered && hoverUIInstance == null)
        {
            InitializeHoverUI();
        }

        else if (!isHovered && hoverUIInstance != null)
        {
            DeleteHoverUI();
        }
    }

    public override void InitializeHoverUI()
    {
        hoverUIInstance = Instantiate(hoverUI, GameObject.FindGameObjectWithTag("HoverCanvas"). transform);
        hoverUIInstance.transform.position = Camera.main.WorldToScreenPoint(hoverUIPosition.transform.position);
        var offenseHoverUI = hoverUIInstance.GetComponent<OffenseTowerHoverUI>();

        if (offenseHoverUI != null)
        {
            offenseHoverUI.SetValues(this);
        }
    }

    public override void DeleteHoverUI()
    {
        Destroy(hoverUIInstance);
    }

    protected void FixHoverUIPosition()
    {
        hoverUIInstance.transform.position = Camera.main.WorldToScreenPoint(hoverUIPosition.transform.position);
    }
}
