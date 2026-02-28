using System.Collections;
using UnityEngine;
using UnityEngine.VFX;

public class AOEAtkJIB : BaseProjectileClass
{
    public float AoERange;
    public GameObject atkProjectile;
    public VisualEffect vfx;
    public float lifeTime;

    public override void InitializeProjectile(float projectileDamage, GameObject projectileTarget, Vector3 projectileTargetPosition)
    {
        base.InitializeProjectile(projectileDamage, projectileTarget, projectileTargetPosition);
        vfx.SetVector3("StartPosition", transform.position);
        vfx.SetVector3("EndPosition", targetPosition);
        StartCoroutine(lifetimeCo());
    }
    protected override void ToTarget()
    {
    }

    protected override void ProjectileEffect() //AoE damage
    {
        Collider[] collidersInRange = Physics.OverlapSphere(targetPosition, AoERange);

        foreach (Collider col in collidersInRange)
        {
            BaseEnemyClass frickThisGuy = col.GetComponent<BaseEnemyClass>();

            if (frickThisGuy != null)
            {
                projectileEvent.Post(this.gameObject);
                frickThisGuy.Damage(damage);
            }
        }
    }

    protected IEnumerator lifetimeCo()
    {
        yield return new WaitForSeconds(lifeTime);
        ProjectileEffect();
        yield return new WaitForSeconds(0.5f);
        Destroy(gameObject);
    }


    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, AoERange);
    }
}
