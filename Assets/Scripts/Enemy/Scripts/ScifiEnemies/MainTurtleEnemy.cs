using System.Collections;
using UnityEngine;

public class MainTurtleEnemy : BaseEnemyClass
{
    //variables to manage spawning of holos like cooldowns
    public GameObject turtleHologram;
    private bool spawningHologram = false;
    private bool initialCdDone = false;
    public float initialCooldown;
    public float spawnCooldown;

    protected override void Start()
    {
        //start the initial buffer
        StartCoroutine(StartingCooldown());
        base.Start();
    }

    protected override void Update()
    {
        //for starting coroutine to summon holos
        if (!isStunned && initialCdDone)
        {
            if(!spawningHologram)
            {
                StartCoroutine("SpawnHolograms");
            }
        }
        base.Update();
    }

    //buffer at the start so it doesnt instantly spawn holos when it spawns in
    private IEnumerator StartingCooldown()
    {
        yield return new WaitForSeconds(initialCooldown);
        initialCdDone = true;
    }

    //spawning holograms, similar code to sub berry thing
    private IEnumerator SpawnHolograms()
    {
        spawningHologram = true;
        GameObject tempEnemy = Instantiate(turtleHologram, transform.position, Quaternion.identity);
        tempEnemy.GetComponent<BaseEnemyClass>().InitializeEnemy_OnTrack(waypointManager, waypointIndex, distanceTravelled);

        yield return new WaitForSeconds(spawnCooldown);

        spawningHologram = false;
    }
}
