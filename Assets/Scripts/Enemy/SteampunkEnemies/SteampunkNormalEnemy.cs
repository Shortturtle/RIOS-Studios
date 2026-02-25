using UnityEngine;

public class SteampunkNormalEnemy : BaseEnemyClass
{
    //fro spawn speed field
    public GameObject speedField;

    //pretty much the same exact code as the scifi deathstun enemy
    public bool rangedAttack = true;

    public void DamageMelee()
    {
        rangedAttack = false;
    }

    public override void Damage(float damageAmount)
    {
        currentHealth -= damageAmount;

        if (currentHealth <= 0) { Die(); }
        else { rangedAttack = true; }
    }
    public override void Die()
    {
        ////disabled mechanic
        //int chance = Random.Range(0, 2);
        //if (rangedAttack && chance == 1)
        //{
        //    Instantiate(speedField, transform.position, Quaternion.identity);
        //}

        base.Die();
    }
}
