using UnityEngine;

public interface IEnemyDamageable
{
    EnemyStats enemyStats { get; }
    float currentHealth { get; set; }

    void Damage(float damageAmount);

    void Die();
}
