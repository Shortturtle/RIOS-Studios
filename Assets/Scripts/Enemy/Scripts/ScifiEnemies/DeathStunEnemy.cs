using UnityEngine;

public class DeathStunEnemy : BaseEnemyClass
{
    //game object for spawning stun field
    public GameObject stunField;

    //check if atk is melee
    public bool rangedAttack = true;

    //called in axe swing, set ranged atk to false, so it detects as melee
    public void DamageMelee() { rangedAttack = false; }

    //in dmg, after dmg taken, sets ranged atk back to true of enemy isnt dead
    public override void Damage(float damageAmount)
    {
        currentHealth -= damageAmount;

        if (currentHealth <= 0) { Die(); }
        else { rangedAttack = true; }
    }

    //on death, 50% chance to spawn the stun field if killed by ranged atk
    public override void Die()
    {
        int chance = Random.Range(0, 2);
        if (rangedAttack && chance == 1) { Instantiate(stunField, transform.position, Quaternion.identity); }
        
        base.Die();
    }
}
