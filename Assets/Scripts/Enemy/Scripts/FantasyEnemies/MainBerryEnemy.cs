using UnityEngine;

public class MainBerryEnemy : BaseEnemyClass
{
    public GameObject subBerry;
    public int berrySpawns;
    public override void Die()
    {
        while (berrySpawns > 0)
        {
            Instantiate(subBerry);
            berrySpawns--;
        }

        base.Die();
    }
}
