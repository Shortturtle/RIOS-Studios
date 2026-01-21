using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class MainBerryEnemy : BaseEnemyClass
{
    //the sub berry prefab, and no. of berries that will spawn
    public GameObject subBerry;
    public int berrySpawns;

    //on death, spawn sub berries
    public override void Die()
    {
        //tracks current waypoint & distance travelled info so the berries spawned can access and use it
        Vector3 dirToNextWaypoint = (waypointList[waypointIndex].position - transform.position);
        dirToNextWaypoint.Normalize();
        float splitNumber = -1f;

        //for every berry spawns left, spawns a sub berry with a gap between each other
        while (berrySpawns > 0)
        {
            splitNumber += 0.75f;
            GameObject tempEnemy = Instantiate(subBerry, transform.position + (dirToNextWaypoint *( speed * splitNumber)), Quaternion.identity);
            tempEnemy.GetComponent<BaseEnemyClass>().InitializeEnemy_OnTrack(waypointManager,waypointIndex, distanceTravelled + (speed * splitNumber));
            berrySpawns--;
        }

        base.Die();
    }
}
