using UnityEngine;

public class VeggieEnemy : BaseEnemyClass
{
    public int shieldGainAmount;
    private int shieldCurrentAmount = 0;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("CapShield"))
        {
            shieldCurrentAmount = shieldGainAmount;
        }
    }

    public override void Damage(float damageAmount)
    {
        if (shieldCurrentAmount > 0)
        {
            //add shield decrease when hit
            shieldCurrentAmount--;
            return;
        }

        base.Damage(damageAmount);
    }
}
