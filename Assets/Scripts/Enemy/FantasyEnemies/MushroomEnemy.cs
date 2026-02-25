using UnityEngine;

public class MushroomEnemy : BaseEnemyClass
{
    //cap shield gameobject to spawn
    public GameObject capShield;

    //ref quest
    private KillMushrooms killMushroom;

    protected override void Start()
    {
        //ref for quest
        killMushroom = GetComponent<KillMushrooms>();
        base.Start();
    }

    public override void Die()
    {
        //increase mushroom killed
        if (killMushroom != null) { killMushroom.KilledMushroom(); }

        //spawn cap shield
        Instantiate(capShield, transform.position, Quaternion.identity);

        base.Die();
    }
}
