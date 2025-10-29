using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.Assertions.Must;

public class BaseEnemy : MonoBehaviour, IDamageable, IWaypointFollow
{
    public EnemyStats enemyStats;
    public float currentHealth { get; set; }
    public float speed { get; set; }
    public Transform target { get; set; }
    public int waypointIndex { get; set; } = 1;

    public virtual void Damage(float damageAmount)
    {
        currentHealth -= damageAmount;

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    public virtual void Die()
    {
        Destroy(gameObject);
    }

    public void GetNextWaypoint()
    {
        if (waypointIndex >= WaypointManager.points.Length - 1)
        { 
            Die();
            return;
        }

        waypointIndex++;
        target = WaypointManager.points[waypointIndex];
    }

    virtual public void MoveEnemy()
    {
        Vector3 dir = target.position - transform.position;
        Debug.Log(dir);
        transform.Translate(dir.normalized * speed * Time.deltaTime, Space.World);

        if (Vector3.Distance(transform.position, target.position) <= 0.3f)
        {
            GetNextWaypoint();
        }
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        currentHealth = enemyStats.maxHealth;
        speed = enemyStats.speed;
        target = WaypointManager.points[waypointIndex];
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void FixedUpdate()
    {
        MoveEnemy();
    }
}
