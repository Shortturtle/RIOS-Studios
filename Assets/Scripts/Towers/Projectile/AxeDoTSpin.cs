using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class AxeDoTSpin : BaseProjectileClass
{
    [Header("DoT Stuff")]
    public float dotDuration;
    public float tickInterval;
    public float AttackRadius;

    public override void InitializeProjectile(float projectileDamage, GameObject projectileTarget, Vector3 projectileTargetPosition)
    {
        base.InitializeProjectile(projectileDamage, projectileTarget, projectileTargetPosition);
        StartCoroutine(DoTDamage());
    }
    protected IEnumerator DoTDamage()
    {
        float elapsed = 0f;

        while (elapsed < dotDuration)
        {
            Collider[] hits = Physics.OverlapSphere(transform.position, AttackRadius);

            foreach (Collider col in hits)
            {
                BaseEnemyClass enemy = col.GetComponent<BaseEnemyClass>();
                if (enemy != null)
                {
                    enemy.Damage(damage);
                }
            }

            projectileEvent.Post(gameObject);

            yield return new WaitForSeconds(tickInterval);
            elapsed += tickInterval;
        }

        Destroy(gameObject);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, AttackRadius);
    }

}
