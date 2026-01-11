using UnityEngine;

public class MushroomEnemy : BaseEnemyClass
{
    public GameObject capShield;
    //public Transform capSpawn;

    public override void Die()
    {
        Instantiate(capShield, this.transform.position, Quaternion.identity);

        base.Die();
    }
}
