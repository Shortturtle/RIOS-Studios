using UnityEngine;
using System.Collections;

public class SteampunkTankEnemy : BaseEnemyClass
{
    //values for the regen mechanic
    private bool healthMaxed = true;
    private bool wasHit = false;
    private bool regenerating;
    public float regenCountdown;
    public float healthGain;
    public float timeBetweenRegen;

    protected override void Update()
    {
        if (currentHealth < enemyStats.maxHealth)  //check if current health is not full [took dmg], healthMaxed = false
        {
            healthMaxed = false;
        }
        if (currentHealth >= enemyStats.maxHealth)  //check if currenthealth is maxed, healthMaxed = true, so enemy wont overheal
        {
            healthMaxed = true;
            currentHealth = enemyStats.maxHealth;  //so that heal doesnt go over the max value
        }
        if (!regenerating && !healthMaxed && !wasHit)  //if all cooldowns are off, enemy can start healing
        {
            StartCoroutine("RegenerateHealth");
        }

        base.Update();
    }

    public override void Damage(float damageAmount)
    {
        //if took dmg, stop current regen if any, and start coroutine to create cooldown for regen health
        wasHit = true;
        StopCoroutine("RegenerateHealth");
        regenerating = false;
        StopCoroutine("OnHitCooldown");
        StartCoroutine("OnHitCooldown");

        base.Damage(damageAmount);
    }

    //wait for a while before enemy can start healing so enemy doesnt constantly heal even if it takes dmg
    private IEnumerator OnHitCooldown()
    {
        yield return new WaitForSeconds(regenCountdown);
        wasHit = false;
    }

    //increase health (the regen) when all conditions are met
    private IEnumerator RegenerateHealth()
    {
        regenerating = true;
        currentHealth += healthGain;
        yield return new WaitForSeconds(timeBetweenRegen);
        regenerating = false;
    }
}
