using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.Assertions.Must;

public class Enemy : MonoBehaviour, IDamageable, IWaypointFollow
{
    public EnemyStats enemyStats {  get; }
    public float currentHealth { get; set; }
    public float speed { get; set; }
    public Transform target { get; set; }
    public int waypointIndex { get; set; } = 0;

    public void Damage(float damageAmount)
    {
        currentHealth -= damageAmount;

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    public void Die()
    {
        Destroy(gameObject);
    }

    public void GetNextWaypoint()
    {
        if (waypointIndex == WaypointManager.points.Length - 1)
        {
            Destroy(gameObject);
            return;
        }

        waypointIndex++;
        target = WaypointManager.points[waypointIndex];
    }

    public void MoveEnemy()
    {
        Vector3 dir = transform.position - target.position;
        transform.Translate(dir.normalized * speed * Time.deltaTime);

        if (Vector3.Distance(transform.position, target.position) <= 0.3f)
        {
            GetNextWaypoint();
        }
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
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
