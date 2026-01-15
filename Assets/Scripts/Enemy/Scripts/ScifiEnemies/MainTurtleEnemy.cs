using System.Collections;
using UnityEngine;

public class MainTurtleEnemy : BaseEnemyClass
{
    public GameObject turtleHologram;
    private bool spawningHologram = false;
    private bool initialCdDone = false;
    public float initialCooldown;
    public float spawnCooldown;

    protected override void Start()
    {
        StartCoroutine(StartingCooldown());
        base.Start();
    }

    protected override void Update()
    {
        if (!isStunned && initialCdDone)
        {
            if(!spawningHologram)
            {
                StartCoroutine("SpawnHolograms");
            }
        }
        base.Update();
    }

    private IEnumerator StartingCooldown()
    {
        yield return new WaitForSeconds(initialCooldown);
        initialCdDone = true;
    }
    private IEnumerator SpawnHolograms()
    {
        spawningHologram = true;
        GameObject tempEnemy = Instantiate(turtleHologram, transform.position, Quaternion.identity);
        tempEnemy.GetComponent<BaseEnemyClass>().InitializeEnemy_OnTrack(waypointManager, waypointIndex, distanceTravelled);

        yield return new WaitForSeconds(spawnCooldown);

        spawningHologram = false;
    }
}
