using System.Collections;
using UnityEngine;

public class FruitEnemy : BaseEnemyClass
{
    private bool healthMaxed = true;
    private bool wasHit = false;
    private bool regenerating;
    public float regenCountdown;
    public float healthGain;
    public float timeBetweenRegen;

    //private KillFruits killFruit;

    protected override void Start()
    {
        //killFruit = GetComponent<KillFruits>();
        base.Start();
    }

    protected override void Update()
    {
        if(currentHealth < enemyStats.maxHealth)
        {
            healthMaxed = false;
        }
        if(currentHealth >= enemyStats.maxHealth)
        {
            currentHealth = enemyStats.maxHealth;
            healthMaxed = true;
        }
        if(!regenerating && !healthMaxed && !wasHit)
        {
            StartCoroutine("RegenerateHealth");
        }

        base.Update();
    }

    public override void Damage(float damageAmount)
    {
        wasHit = true;
        StopCoroutine("RegenerateHealth");
        regenerating = false;
        StopCoroutine("OnHitCooldown");
        StartCoroutine("OnHitCooldown");

        base.Damage(damageAmount);
    }

    public override void Die()
    {
        //if(killFruit != null) { killFruit.KilledFruit(); }

        base.Die();
    }

    private IEnumerator OnHitCooldown()
    {
        yield return new WaitForSeconds(regenCountdown);
        wasHit = false;
    }

    private IEnumerator RegenerateHealth()
    {
        regenerating = true;
        currentHealth += healthGain;
        yield return new WaitForSeconds(timeBetweenRegen);
        regenerating = false;
    }
}
