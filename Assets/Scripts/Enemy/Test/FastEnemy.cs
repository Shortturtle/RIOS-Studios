using UnityEngine;

public class FastEnemy : BaseEnemy
{
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

    private void FixedUpdate()
    {
        MoveEnemy();
    }

    public override void Die()
    {
        transform.localScale = new Vector3(100,100,100);
    }
}
