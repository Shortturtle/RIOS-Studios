using Unity.VisualScripting;
using UnityEngine;

public class AxeSwing : BaseProjectileClass
{
    public float AttackRadius;
    protected override void Update()
    {
        if (target == null)
        {
            Destroy(gameObject);
        }
    }

    public override void InitializeProjectile(float projectileDamage, GameObject projectileTarget, Vector3 projectileTargetPosition)
    {
        base.InitializeProjectile(projectileDamage, projectileTarget, projectileTargetPosition);
        ProjectileEffect();
    }

    protected override void ProjectileEffect()
    {
        Collider[] collidersInRange = Physics.OverlapSphere(transform.position, AttackRadius);

        foreach (Collider col in collidersInRange)
        {
            BaseEnemyClass frickThisGuy = col.GetComponent<BaseEnemyClass>();

            if (frickThisGuy != null)
            {
                projectileEvent.Post(this.gameObject);
                frickThisGuy.Damage(damage);
            }
        }

        Destroy(gameObject);
    }

    protected override void ToTarget()
    {
        // AxeSwing does not need to move towards a target
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, AttackRadius);
    }
}
