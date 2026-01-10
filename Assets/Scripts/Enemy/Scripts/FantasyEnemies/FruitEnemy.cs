using System.Collections;
using UnityEngine;

public class FruitEnemy : BaseEnemyClass
{
    private bool canRegen = true;
    public float regenCountdown;
    public float healthGain;
    public float timeBetweenRegen;

    private KillFruits killFruit;

    protected override void Start()
    {
        killFruit = GetComponent<KillFruits>();
        base.Start();
    }

    protected override void Update()
    {
        if(currentHealth < enemyStats.maxHealth)
        {
            StartCoroutine(RegenerateHealth());
        }

        base.Update();
    }

    public override void Damage(float damageAmount)
    {
        canRegen = false;

        base.Damage(damageAmount);

        StartCoroutine(Regenerate());
    }

    public override void Die()
    {
        if(killFruit != null) { killFruit.KilledFruit(); }

        base.Die();
    }

    private IEnumerator Regenerate()
    {
        yield return new WaitForSeconds(regenCountdown);

        canRegen = true;
    }

    //might have to put in fixed update or smth
    private IEnumerator RegenerateHealth()
    {
        while (canRegen)
        {
            currentHealth += healthGain;

            yield return new WaitForSeconds(timeBetweenRegen);
        }
    }
}
