using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.Assertions.Must;

public class Enemy : MonoBehaviour, IEnemyDamageable, IEnemyWaypointFollow
{
    [field: SerializeField] public EnemyStats enemyStats {  get; }
    public float currentHealth { get; set; }
    [field: SerializeField] public WaypointManager waypointManager { get; set; }
    public float speed { get; set; }

    private Transform[] waypoints;

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
        throw new System.NotImplementedException();
    }

    public void MoveEnemy()
    {
        throw new System.NotImplementedException();
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
        currentHealth = enemyStats.maxHealth;
        speed = enemyStats .speed;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
