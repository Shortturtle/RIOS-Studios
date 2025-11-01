using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.Assertions.Must;

[RequireComponent(typeof(Rigidbody))]
public class BaseEnemyClass : MonoBehaviour, IDamageable, IWaypointFollow
{
    public EnemyStats enemyStats;
    public float currentHealth { get; set; }
    public float speed { get; set; }
    public Transform target { get; set; }
    public int waypointIndex { get; set; } = 1;

    protected float distanceTravelled;
    public float percentageDistance; 
    protected enum direction
    {
        Forward, Backward
    }
    protected direction directionTravelling = direction.Forward;

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
        Destroy(gameObject);
    }

    public void GetNextWaypoint() // waypoint tracking
    {
        if (waypointIndex >= WaypointManager.points.Length - 1)
        { 
            Die();
            return;
        }

        waypointIndex++;
        target = WaypointManager.points[waypointIndex];
    }

    virtual public void MoveEnemy() // enemy movement script
    {
        Vector3 dir = target.position - transform.position;
        transform.Translate(dir.normalized * speed * Time.deltaTime, Space.World);

        if (Vector3.Distance(transform.position, target.position) <= 0.3f)
        {
            GetNextWaypoint();
        }
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public virtual void Start()
    {
        currentHealth = enemyStats.maxHealth;
        speed = enemyStats.speed;
        target = WaypointManager.points[waypointIndex];
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    protected virtual void FixedUpdate()
    {
        MoveEnemy();
        DistanceTracker();
    }

    protected virtual void DistanceTracker()
    {
        if (directionTravelling == direction.Forward)
        {
            distanceTravelled += speed * Time.deltaTime;
        }

        else if (directionTravelling == direction.Backward)
        {
            distanceTravelled -= speed * Time.deltaTime;
        }

        percentageDistance = (distanceTravelled / WaypointManager.totalDistance) * 100;
    }
}
