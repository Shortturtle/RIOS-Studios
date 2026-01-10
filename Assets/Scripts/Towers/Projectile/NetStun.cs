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
        Vector3 dir = targetPosition - transform.position;
        transform.Translate(dir.normalized * speed * Time.deltaTime, Space.World);

        if (Vector3.Distance(transform.position, targetPosition) <= 0.3f)
        {
            ProjectileEffect();
        }
    }

    protected override void ProjectileEffect() //Start Coroutine for the AoE and StunDoT
    {
        StartCoroutine(StunDoT());
    }

    IEnumerator StunDoT()
    {
        float elapsed = 0f;

        Collider[] collidersInRange = Physics.OverlapSphere(transform.position, AoERange);

        foreach (Collider hit in collidersInRange)
        {
            BaseEnemyClass enemy = hit.GetComponent<BaseEnemyClass>();
            if (enemy != null)
            {
                enemy.Stun(stunDuration);
            }
        }

        while (elapsed < dotDuration)
        {
            foreach (Collider col in collidersInRange)
            {
                if (col == null) continue;

                BaseEnemyClass enemy = col.GetComponent<BaseEnemyClass>();
                if (enemy != null)
                {
                    projectileEvent.Post(gameObject);
                    enemy.Damage(tickDamage);
                }
            }

            elapsed += tickInterval;
            yield return new WaitForSeconds(tickInterval);
        }

        Destroy(gameObject);
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, AoERange);
    }
}
