using UnityEngine;

public class VeggieEnemy : BaseEnemyClass
{
    //max shield amount and current shield enemy has
    public int shieldGainAmount;
    private int shieldCurrentAmount = 0;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("CapShield"))
        {
            if(shieldGainAmount == 0)
            {
                //when touch cap shield, gain shield by max amount and destroy the cap shield touched
                shieldCurrentAmount = shieldGainAmount;
                Destroy(other.gameObject);
            }
        }
    }

    public override void Damage(float damageAmount)
    {
        if (shieldCurrentAmount > 0)
        {
            //when enemy is hit while it has shield, decrease shield, only when shield is 0, will the enemy take dmg normally
            shieldCurrentAmount--;
            return;
        }

        base.Damage(damageAmount);
    }
}
