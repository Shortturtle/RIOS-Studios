using UnityEngine;

public class DeathStunEnemy : BaseEnemyClass
{
    public GameObject stunField;
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
        if (rangedAttack)
        {
            Instantiate(stunField, transform.position, Quaternion.identity);
        }
        
        base.Die();
    }
}
