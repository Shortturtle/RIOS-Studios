using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class MainBerryEnemy : BaseEnemyClass
{
    public GameObject subBerry;
    public int berrySpawns;


    public override void Die()
    {
        Vector3 dirToNextWaypoint = (waypointList[waypointIndex].position - transform.position);
        dirToNextWaypoint.Normalize();
        float splitNumber = -1f;
        while (berrySpawns > 0)
        {
            splitNumber += 1;
            GameObject tempEnemy = Instantiate(subBerry, transform.position + (dirToNextWaypoint *( speed * splitNumber)), Quaternion.identity);
            tempEnemy.GetComponent<BaseEnemyClass>().InitializeEnemy_OnTrack(waypointManager,waypointIndex, distanceTravelled + (speed * splitNumber));
            berrySpawns--;
        }

        base.Die();
    }
}
