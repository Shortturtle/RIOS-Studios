using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent (typeof(SphereCollider))]
public class OffenseTowerBase : BaseTowerClass
{
    // Stat Block
    public OffenseTowerStats stats;

    // Range variables
    protected SphereCollider rangeSphere;
    public GameObject rangeIndicator;

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
    [HideInInspector] public float damageMod =1f;
    [HideInInspector] public float timeBetweenAttackMod = 1f;
    [HideInInspector] public float rangeMod =1f;
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

    //VFX Variables
    public GameObject degradeVFX;
    public GameObject overdriveVFX;

    //Audio events
    protected AK.Wwise.Event attackEvent;
    protected AK.Wwise.Event degradeEvent;

    private int frameCount;

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
        frameCount++;

        //If the tower is stunned, skip the rest of the update loop to prevent it from attacking or doing anything else while stunned
        if (isStunned)
        {
            return;
        }
        if (isOverdrive && overdriveVFX != null)
        {
            overdriveVFX.SetActive(true);
        }
        else if (overdriveVFX != null)
        {
            overdriveVFX.SetActive(false);
        }
        
        //Update the target enemy every 6 frames (performance optimisation)
        if(frameCount % 6 == 0)
        {
            GetTargetEnemy();
            frameCount = 0;
        }
        
        TrackEnemy();
        AttackTimer();
        GeneralDegradeTracker();
        HoverUIHandler();
    }

    protected void OnDestroy()
    {
        if (hoverUIInstance != null)
        {
            Destroy(hoverUIInstance);
        }
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
        //Check if the object is an enemy by tag or if it has a BaseEnemyClass component, then add it to the list of targets if it's not already in there
        if (other.gameObject.CompareTag("Enemy") || other.gameObject.GetComponent<BaseEnemyClass>())
        {
            BaseEnemyClass tempEnemy = other.gameObject.GetComponent<BaseEnemyClass>();

            //If the enemy is flying and the tower can't attack flying, don't add it to the list of targets
            if (tempEnemy.isFlying && !canAttackFlying)
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
        //Remove the enemy from the list of targets if it leaves the range of the tower
        if (targets.Contains(other.GetComponent<BaseEnemyClass>()))
        {
            targets.Remove(other.GetComponent<BaseEnemyClass>());
        }
    }

    protected void ChangeTargettingMode(ChangeDirection direction)
    {
        targetModeNum = direction == ChangeDirection.Next ? targetModeNum++ : targetModeNum--;

        //Loop the target mode number if it goes out of bounds
        if (targetModeNum < 0)
        {
            targetModeNum = Enum.GetNames(typeof(TargettingModes)).Length - 1;
        }

        //If the target mode number is greater than the number of targetting modes, loop it back to 0
        else if (targetModeNum > (Enum.GetNames(typeof(TargettingModes)).Length - 1))
        {
            targetModeNum = 0;
        }

        targettingMode = (TargettingModes)Enum.ToObject(typeof(TargettingModes), targetModeNum);

    }

    protected void GetTargetEnemy()
    {
        //If there are targets in range, find the one that matches the targetting mode and set it as the current target. If there are no targets in range, set the current target to null
        if (targets.Count != 0)
        {
            GameObject targetedEnemy = null;

            switch (targettingMode)
            {
                //First: Target the enemy that is closest to reaching the end of the path (highest percentage distance)
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

                //Last: Target the enemy that is furthest from reaching the end of the path (lowest percentage distance)
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

                //Close: Target the enemy that is closest to the tower (lowest distance)
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

                //Strong: Target the enemy with the highest current health
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
        //If there is a current target, rotate the moveable target(tower model) to look at the enemy. (for the tower's aiming and shooting)
        if (currentTarget != null)
        {
            Vector3 lookAtDir = new Vector3(currentTarget.transform.position.x, MoveableTarget.transform.position.y, currentTarget.transform.position.z);
            MoveableTarget.transform.LookAt(lookAtDir);
        }
    }

    public override void InitializeTower()
    {
        //Set all the base values for the tower from the stats scriptable object
        damageBase = stats.Damage;
        damageValue = damageBase;

        timeBetweenAttacksBase = stats.TimeBetweenAttacks;
        timeBetweenAttackValue = timeBetweenAttacksBase;

        rangeBase = stats.Range;
        rangeValue = rangeBase;
        rangeSphere.isTrigger = true;
        rangeSphere.radius = rangeValue;
        rangeIndicator.transform.localScale *= (rangeValue * 2);

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

        if (degradeVFX != null) { degradeVFX.SetActive(false); }
        if (overdriveVFX != null) { overdriveVFX.SetActive(false); }

        if (stats.AttackEvent != null) attackEvent = stats.AttackEvent;
        degradeEvent = stats.DegradeEvent;
    }

    protected virtual void AttackTimer()
    {
        //If there is a target in range, count down the attack timer. If the timer reaches 0, attack and reset the timer
        if (Time.timeScale == 0) { return; }
        attackTimer -= Time.deltaTime;

        if (currentTarget != null && attackTimer <= 0)
            {
                Attack();
                    attackTimer = timeBetweenAttackValue * timeBetweenAttackMod;
            }
    }

    protected virtual void Attack()
    {
        //Play attack event, spawn projectile, and initialize it with the damage value and target
        if (attackEvent != null) attackEvent.Post(gameObject);
        GameObject projectileInstance = Instantiate(projectile, bulletExitPoint.transform.position, Quaternion.identity);
        projectileInstance.GetComponent<BaseProjectileClass>().InitializeProjectile(damageValue * damageMod, currentTarget, currentTarget.transform.position);
    }

    protected override void MaxDegradeTracker()
    {
        //If the degrade rank is at max && isn't already marked as max degraded, mark it as max degraded, play the degrade event, and show the degrade sign and VFX.
        if (degradeRank == maxDegradeRank && !isMaxDegraded)
        {
            isMaxDegraded = true;
            degradeEvent.Post(gameObject);
            degradeSign.SetActive(true);
            if (degradeVFX != null) { degradeVFX.SetActive(true); }
        }
        //If the tower isn't at max degrade, make sure the degrade sign and VFX are hidden
        else if (!isMaxDegraded)
        {
            degradeSign.SetActive(false);
            if (degradeVFX != null) { degradeVFX.SetActive(false); }
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
        //Set the tower to overdrive, which increases its attack speed for a short time. Also reset the attack timer so that the tower can immediately take advantage of the increased attack speed.
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
        if (hoverUIInstance != null)
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
        //Instantiate the hover UI prefab as a child at the preset position of hoverUIPosition. Then set the values of the hover UI to match the tower's current stats.
        hoverUIInstance = Instantiate(hoverUI, GameObject.FindGameObjectWithTag("HoverCanvas"). transform);
        hoverUIInstance.transform.position = Camera.main.WorldToScreenPoint(hoverUIPosition.transform.position);
        var offenseHoverUI = hoverUIInstance.GetComponent<OffenseTowerHoverUI>();

        if (offenseHoverUI != null)
        {
            offenseHoverUI.SetValues(this);
        }

        rangeIndicator.gameObject.SetActive(true);
    }

    public override void DeleteHoverUI()
    {
        Destroy(hoverUIInstance);
        rangeIndicator.gameObject.SetActive(false);
    }

    protected void FixHoverUIPosition()
    {
        hoverUIInstance.transform.position = Camera.main.WorldToScreenPoint(hoverUIPosition.transform.position);
    }
}
