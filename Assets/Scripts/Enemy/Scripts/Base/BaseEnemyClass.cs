using NUnit.Framework;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.Assertions.Must;

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
    #endregion

    #region Skill Variables
    public bool isRewinding = false;
    public List<PointInTime> pointsInTime = new List<PointInTime>();

    public class PointInTime
    {
        public Vector3 position;
        public Quaternion rotation;

        public PointInTime(Vector3 _position, Quaternion _rotation)
        {
            position = _position;
            rotation = _rotation;
        }
    }
    protected float recordTime = 5f;

    public bool isStunned = false;
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

    public virtual void Die() // death code (can be overridden)
    {
        ResourceManager.instance.AddEnergy(energyOnDeath);
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
        yield return new WaitForSeconds(duration);
        isStunned = false;
    }

    protected void Record()
    {
        //Check: Do we have more points in time than we would get in 5s? If yes, then start overwriting the oldest points
        if (pointsInTime.Count > Mathf.Round(recordTime / Time.fixedDeltaTime/*get the time between each fixedUpdate*/))
        {
            pointsInTime.RemoveAt(pointsInTime.Count - 1);                                                          //Remove the oldest point in time (elements at the BOTTOM of the list)
        }

        pointsInTime.Insert(0, new PointInTime(transform.position, transform.rotation));                            //Add values(current position) to the START/TOP of the list
    }

    protected void Rewind()
    {
        Transform tempTarget;

        if (waypointIndex != 0)
        {
            tempTarget = waypointList[waypointIndex - 1];                                                           //Reset the target waypoint to the previous one IF the enemy rewinds past it
        }

        else {
            tempTarget = target;
        }
        if (pointsInTime.Count > 0)
        {
            PointInTime pointInTime = pointsInTime[0];                                                              //Get the first element in the list

            transform.position = pointInTime.position;
            transform.rotation = pointInTime.rotation;

            pointsInTime.RemoveAt(0);                                                                               //Remove the first element in the list

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
    }

    public void StopRewind()
    {
        isRewinding = false;
        Debug.Log("Rewinding stopped");
        directionTravelling = direction.Forward;
    }

    protected virtual void DistanceTracker()
    {
        switch (directionTravelling)
        {
            case direction.Forward:
                distanceTravelled += speed * Time.deltaTime;
                percentageDistance = (distanceTravelled / totalDistance) * 100;
                return;

            case direction.Backward:
                distanceTravelled -= speed * Time.deltaTime;
                percentageDistance = (distanceTravelled / totalDistance) * 100;
                return;
        }
    }

    protected virtual void BaseReached()
    {
        ResourceManager.instance.ReduceHealth(currentHealth);
        Destroy(this.gameObject);
    }
}