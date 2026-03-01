using UnityEngine;

public class HologramScript : MonoBehaviour
{
    public BaseEnemyClass originalEnemy; //Ref to the og enemy that the hologram is mimicking

    BaseEnemyClass myEnemy; //Ref to the hologram's own enemy class

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        myEnemy = GetComponent<BaseEnemyClass>();
    }

    // Update is called once per frame
    void Update()
    {
        if (myEnemy != null && myEnemy.currentHealth <= 0) //If the hologram dies, damage the original enemy and destroy the hologram
        {
            if (originalEnemy != null)
            {
                //The damage dealt to the original enemy is 5% of its max health, but it can be adjusted as needed
                float damage = originalEnemy.enemyStats.maxHealth * 0.05f;
                originalEnemy.Damage(damage);
            }

            Destroy(gameObject);
        }
    }
}
