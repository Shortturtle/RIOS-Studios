using UnityEngine;

public class MushroomEnemy : BaseEnemyClass
{
    public GameObject capShield;
    //public Transform capSpawn;

    private KillMushrooms killMushroom;

    protected override void Start()
    {
        killMushroom = GetComponent<KillMushrooms>();
        base.Start();
    }

    public override void Die()
    {
        if (killMushroom != null) { killMushroom.KilledMushroom(); }

        Instantiate(capShield, this.transform.position, Quaternion.identity);

        base.Die();
    }
}
