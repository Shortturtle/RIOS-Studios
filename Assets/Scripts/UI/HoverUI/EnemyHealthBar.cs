using System;
using TMPro;
using UnityEngine;

public class EnemyHealthBar : MonoBehaviour
{
    public TextMeshProUGUI healthText;
    public Material healthBarMaterial;
    private BaseEnemyClass enemy;
    private int healthCount;
    private int maxHealth;
    private float healthPercentage;

    private void Update()
    {
        UpdateHealthBar();
    }
    public void SetTarget(BaseEnemyClass tempEnemy)
    {
        enemy = tempEnemy;
        maxHealth = (int)enemy.enemyStats.maxHealth;
    }
    private void UpdateHealthBar()
    {
        healthCount = (int)enemy.currentHealth;
        healthCount = Mathf.Clamp(healthCount, 0, maxHealth);
        healthPercentage = (float)Math.Round(((float)healthCount / (float)maxHealth), 2);
        healthBarMaterial.SetFloat("_Percentage", healthPercentage);

        //update ui for health
        healthText.text = $"{healthCount}/{maxHealth}";
    }
}
