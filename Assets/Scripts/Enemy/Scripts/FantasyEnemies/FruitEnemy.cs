using System.Collections;
using UnityEngine;

public class FruitEnemy : BaseEnemyClass
{
    private bool canRegen = true;
    public float regenCountdown;
    public float healthGain;
    public float timeBetweenRegen;

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
