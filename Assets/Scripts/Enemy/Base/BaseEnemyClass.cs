using NUnit.Framework;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.Assertions.Must;
using static UnityEngine.EventSystems.EventTrigger;

[RequireComponent(typeof(Rigidbody))]
public class BaseEnemyClass : MonoBehaviour, IDamageable, IWaypointFollow
{
    #region Enemy Stats
    public EnemyStats enemyStats;
    public float currentHealth { get; set; }
    public float speed { get; set; }
    public bool isFlying { get; set; }
    public Transform target { get; set; }
    public int waypointIndex { get; set; } = 0;

    public int energyOnDeath = 0;

    public GameObject enemyDeathVFX;
    #endregion

    #region Skill Variables
    public GameObject freezeVFX;
    public bool isRewinding = false;
    public bool isStunned = false;
    public bool isHologram = false;
    public List<PointInTime> pointsInTime = new List<PointInTime>();
    private Animator animator;

    public class PointInTime
    {
        public Vector3 position;
        public Quaternion rotation;
        public float distance;
        public int waypointIndex;
        public Transform target;

        public PointInTime(Vector3 _position, Quaternion _rotation, float _distance, int _waypointIndex, Transform _target)
        {
            position = _position;
            rotation = _rotation;
            distance = _distance;
            waypointIndex = _waypointIndex;
            target = _target;
        }
    }
    protected float recordTime = 5f;

    [Header("Hologram stuff")]
    public GameObject enemyPrefab;
    public Material hologramMaterial;
    #endregion

    #region Distance Variables
    public WaypointManager waypointManager;
    protected Transform[] waypointList;
    protected float totalDistance;
    public float distanceTravelled;
    public float percentageDistance;
    protected enum direction
    {
        Forward, Backward
    }
    protected direction directionTravelling = direction.Forward;
    #endregion

    #region Ui Variable
    public GameObject healthBarPosition;
    #endregion

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    protected virtual void Start()
    {
        animator = GetComponentInChildren<Animator>();
    }

    public void InitializeEnemy_Start(WaypointManager wM)
    {
        currentHealth = enemyStats.maxHealth;
        speed = enemyStats.speed;
        isFlying = enemyStats.isFlying;
        energyOnDeath = enemyStats.energyOnDeath;
        waypointManager = wM;
        waypointList = waypointManager.points;
        totalDistance = waypointManager.totalDistance;
        waypointIndex = 0;
        target = waypointManager.points[waypointIndex];
    }

    public void InitializeEnemy_OnTrack(WaypointManager wM, int wayPointIndex, float _distanceTravelled)
    {
        currentHealth = enemyStats.maxHealth;
        speed = enemyStats.speed;
        energyOnDeath = enemyStats.energyOnDeath;
        waypointManager = wM;
        waypointList = waypointManager.points;
        totalDistance = waypointManager.totalDistance;
        distanceTravelled = _distanceTravelled;
        waypointIndex = wayPointIndex;
        target = waypointManager.points[waypointIndex];
        EnemyWaveManager.instance.AddEnemyDuringWave(gameObject);
    }

    // Update is called once per frame
    protected virtual void Update()
    {
        DistanceTracker();

        if (isRewinding)
        {
            Rewind();
        }

        else if (!isStunned)
        {
            Record();
            MoveEnemy();
        }
    }

    protected virtual void FixedUpdate()
    {
    }

    public virtual void Damage(float damageAmount) // Script to damage enemies (can be overridden)
    {
        currentHealth -= damageAmount;

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    public virtual void Die() //death code (can be overridden)
    {
        ResourceManager.instance.AddEnergy(energyOnDeath);
        Instantiate(enemyDeathVFX, transform.position, Quaternion.identity);
        Destroy(gameObject);
    }

    public void GetNextWaypoint() // waypoint tracking
    {
        if (waypointIndex >= waypointList.Length - 1)
        {
            BaseReached();
            return;
        }

        waypointIndex++;
        target = waypointList[waypointIndex];
    }

    public void GetPreviousWaypoint()
    {
        if (waypointIndex == 0)
        {
            return;
        }

        waypointIndex--;
        target = waypointList[waypointIndex];
    }

    virtual public void MoveEnemy() // enemy movement script
    {
        Vector3 dir = target.position - transform.position;
        transform.Translate(dir.normalized * speed * Time.deltaTime, Space.World);
        transform.LookAt(target.position);

        if (Vector3.Distance(transform.position, target.position) <= 0.3f)
        {
            GetNextWaypoint();
        }
    }

    public void Stun(float duration)
    {
        StartCoroutine(StunRoutine(duration));
    }
    private IEnumerator StunRoutine(float duration)
    {
        isStunned = true;
        animator.speed = 0f; //Pause animation
        yield return new WaitForSeconds(duration);
        isStunned = false;
        animator.speed = 1f; //Resume animation
    }

    public void freeze(float duration)
    {
        StartCoroutine(FreezeRoutine(duration));
    }
    private IEnumerator FreezeRoutine(float duration)
    {
        isStunned = true;
        freezeVFX.SetActive(true);
        animator.speed = 0f; //Pause animation
        yield return new WaitForSeconds(duration);
        isStunned = false;
        freezeVFX.SetActive(false);
        animator.speed = 1f; //Resume animation
    }

    protected void Record()
    {
        //Check: Do we have more points in time than we would get in 5s? If yes, then start overwriting the oldest points
        if (pointsInTime.Count > Mathf.Round(recordTime / Time.fixedDeltaTime/*get the time between each fixedUpdate*/))
        {
            pointsInTime.RemoveAt(pointsInTime.Count - 1); //Remove the oldest point in time (elements at the BOTTOM of the list)
        }

        pointsInTime.Insert(0, new PointInTime(transform.position, transform.rotation, distanceTravelled, waypointIndex, target)); //Add values(current position) to the START/TOP of the list
    }

    protected void Rewind()
    {
        Transform tempTarget;

        if (waypointIndex != 0)
        {
            tempTarget = waypointList[waypointIndex - 1]; //Reset the target waypoint to the previous one IF the enemy rewinds past it
        }

        else
        {
            tempTarget = target;
        }
        if (pointsInTime.Count > 0)
        {
            PointInTime pointInTime = pointsInTime[0]; //Get the first element in the list

            //Set the position and rotation of the enemy to the values of the first element in the list
            transform.position = pointInTime.position;
            transform.rotation = pointInTime.rotation;
            distanceTravelled = pointInTime.distance;
            waypointIndex = pointInTime.waypointIndex;
            target = pointInTime.target;

            pointsInTime.RemoveAt(0); //Remove the first element in the list

            if (Vector3.Distance(transform.position, tempTarget.position) <= 0.3f)
            {
                GetPreviousWaypoint();
            }
        }
        else
        {
            StopRewind();
        }
    }

    public void StartRewind()
    {
        isRewinding = true;
        Debug.Log("Rewinding started");
        directionTravelling = direction.Backward;
        animator.speed = -1f; //Play animation in reverse
    }

    public void StopRewind()
    {
        isRewinding = false;
        Debug.Log("Rewinding stopped");
        directionTravelling = direction.Forward;
        animator.speed = 1f; //Resume normal animation
    }

    public void SpawnHologram()
    {
        //instantiate a copy of enemy and change material to hologram material, then spawn the hologram at the enemy position with 1hp
        //when the hologram is destroyed, it will not give energy but deal 10%hp damage to the enemy and destroy itself

        //Spawn copy of enemy at enemy position
        GameObject hologram = Instantiate(enemyPrefab, transform.position, transform.rotation);

        //Change every material in the list to hologram material
        Renderer[] renderers = hologram.GetComponentsInChildren<Renderer>();

        foreach (Renderer rend in renderers)
        {
            Material[] mats = new Material[rend.materials.Length];

            for (int i = 0; i < rend.materials.Length; i++)
            {
                mats[i] = hologramMaterial;
            }

            rend.materials = mats;
        }

        //Set hologram health to 5% and stop the hologram from moving
        BaseEnemyClass hologramEnemy = hologram.GetComponent<BaseEnemyClass>();
        if (hologramEnemy != null)
        {
            hologramEnemy.isHologram = true;
            hologramEnemy.currentHealth = hologramEnemy.enemyStats.maxHealth * 0.05f;
            hologramEnemy.speed = 0f;
            hologramEnemy.animator.speed = 0f;

            hologramEnemy.energyOnDeath = 0; //Set the hologram to not give energy on death
        }

        //Add hologram script to actually make it function. (Deal 5% damage to the original enemy when destroyed)
        HologramScript hologramScript = hologram.AddComponent<HologramScript>();
        hologramScript.originalEnemy = this; //ref to current enemy
    }

    protected virtual void DistanceTracker()
    {
        switch (directionTravelling)
        {
            case direction.Forward:
                distanceTravelled += speed * Time.deltaTime;
                break;

            case direction.Backward:
                distanceTravelled -= speed * Time.deltaTime;
                break;
        }

        percentageDistance = (distanceTravelled / totalDistance) * 100;
    }

    protected virtual void BaseReached()
    {
        ResourceManager.instance.ReduceHealth(currentHealth);
        Destroy(this.gameObject);
    }

    public static implicit operator GameObject(BaseEnemyClass v)
    {
        throw new NotImplementedException();
    }
}