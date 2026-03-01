using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class PortalOverdrive : BaseProjectileClass
{
    [Header("DoT Stuff")]
    public float dotDuration;
    public float tickInterval;

    [Header("Hitbox Stuff")]
    public float hitboxLength;
    public float hitboxRadius;

    public override void InitializeProjectile(float projectileDamage, GameObject projectileTarget, Vector3 projectileTargetPosition)
    {
        base.InitializeProjectile(projectileDamage, projectileTarget, projectileTargetPosition);
        StartCoroutine(ProjectileEffectCo());
    }

    protected override void ProjectileEffect()
    {
        StartCoroutine(DoTDamage());
    }

    protected IEnumerator ProjectileEffectCo()
    {
        yield return new WaitForSeconds(1.3f);
        ProjectileEffect();
        yield return new WaitForSeconds(3f);
        Destroy(gameObject);
    }

    protected IEnumerator DoTDamage()
    {
        float elapsed = 0f;

        while (elapsed < dotDuration)
        {
            Collider[] hits = Physics.OverlapCapsule(transform.position, (transform.position +(transform.forward * hitboxLength)), hitboxRadius);

            foreach (Collider col in hits)
            {
                BaseEnemyClass enemy = col.GetComponent<BaseEnemyClass>();
                if (enemy != null)
                {
                    Debug.Log("Enemy Damaged");
                    enemy.Damage(damage);
                }
            }

            yield return new WaitForSeconds(tickInterval);
            elapsed += tickInterval;
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere((transform.position + (transform.forward * hitboxLength)), hitboxRadius);
        Gizmos.DrawWireSphere(transform.position, hitboxRadius);

        // Draw the four connecting lines (assuming vertical capsule along the Y axis)
        Vector3 fwd = transform.forward * hitboxRadius;
        Vector3 right = transform.right * hitboxRadius;

        Gizmos.DrawLine((transform.position + (transform.forward * hitboxLength)) + right, transform.position + right);
        Gizmos.DrawLine((transform.position + (transform.forward * hitboxLength)) - right, transform.position - right);
        Gizmos.DrawLine((transform.position + (transform.forward * hitboxLength)) + fwd, transform.position + fwd);
        Gizmos.DrawLine((transform.position + (transform.forward * hitboxLength)) - fwd, transform.position - fwd);
    }   
}
