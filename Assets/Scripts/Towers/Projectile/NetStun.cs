using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class NetStun : BaseProjectileClass
{
    [Header("DoT Stuff")]
    public float dotDuration;
    public float tickInterval;
    public float tickDamage;

    public float AoERange;
    public float stunDuration = 2f;
    public GameObject Net;

    private bool hasHit = false;

    private void Start()
    {
        Physics.Raycast(targetPosition, Vector3.down, out RaycastHit hitInfo, 5f, LayerMask.GetMask("Path"));
        if(hitInfo.collider != null) //Check if the path is there
        {
            targetPosition = hitInfo.point;
        }
    }

    protected override void ToTarget()
    {
        if (hasHit) return;

        Vector3 dir = targetPosition - transform.position;
        transform.Translate(dir.normalized * speed * Time.deltaTime, Space.World);

        if (Vector3.Distance(transform.position, targetPosition) <= 0.3f)
        {
            hasHit = true;
            ProjectileEffect();
        }
    }


    protected override void ProjectileEffect() //Start Coroutine for the AoE and StunDoT
    {
        speed = 0f; //Stop moving
        StartCoroutine(StunDoT());
    }

    IEnumerator StunDoT()
    {
        float elapsed = 0f;

        Collider[] initialHits = Physics.OverlapSphere(transform.position, AoERange);

        //Stun the enemy
        foreach (Collider hit in initialHits)
        {
            BaseEnemyClass enemy = hit.GetComponent<BaseEnemyClass>();
            if (enemy != null)
            {
                enemy.Stun(stunDuration);
            }
        }

        //DoT effect
        while (elapsed < dotDuration)
        {
            //yield return new WaitForSeconds(tickInterval); //Wait for the tick interval then start damage

            Collider[] hits = Physics.OverlapSphere(transform.position, AoERange);

            foreach (Collider col in hits)
            {
                BaseEnemyClass enemy = col.GetComponent<BaseEnemyClass>();
                if (enemy != null)
                {
                    enemy.Damage(tickDamage);
                }
            }

            projectileEvent.Post(gameObject);
            elapsed += tickInterval;
        }

        Destroy(gameObject);
        yield break;
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, AoERange);
    }
}
