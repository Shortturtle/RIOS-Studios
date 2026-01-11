using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class MainBerryEnemy : BaseEnemyClass
{
    public GameObject subBerry;
    public int berrySpawns;
    public int currentMainIndex;


    //i give up
    public override void Die()
    {
        currentMainIndex = waypointIndex;


        while (berrySpawns > 0)
        {
            Instantiate(subBerry, this.transform.position, Quaternion.identity);
            berrySpawns--;
        }

        base.Die();
    }

    
}
