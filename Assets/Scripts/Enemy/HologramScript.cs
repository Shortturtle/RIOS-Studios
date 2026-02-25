using UnityEngine;

public class HologramScript : MonoBehaviour
{
    public BaseEnemyClass originalEnemy;

    BaseEnemyClass myEnemy;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        myEnemy = GetComponent<BaseEnemyClass>();
    }

    // Update is called once per frame
    void Update()
    {
        if (myEnemy.currentHealth <= 0)
        {
            if (originalEnemy != null)
            {
                float damageAmount = originalEnemy.enemyStats.maxHealth * 0.1f; //Calculate 10% of max health as damage
                originalEnemy.Damage(damageAmount);
            }
            Destroy(gameObject);
        }
    }
}
