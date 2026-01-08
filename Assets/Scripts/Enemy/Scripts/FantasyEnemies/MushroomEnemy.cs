using UnityEngine;

public class MushroomEnemy : BaseEnemyClass
{
    public GameObject capShield;

    public override void Die()
    {
        Instantiate(capShield);

        base.Die();
    }
}
