using System.Collections;
using UnityEngine;

public class RailGunnerLaser : BaseProjectileClass
{
    public float lifetime = 2.5f;
    public GameObject laserVFX;

    protected override void Update()
    {
        lifetime -= Time.deltaTime;

        if ( lifetime <= 0)
        {
            Destroy(gameObject);
        }
    }

    public override void InitializeProjectile(float projectileDamage, GameObject projectileTarget, Vector3 projectileTowerPosition)
    {
        damage = projectileDamage;
        target = projectileTarget;
        targetPosition = projectileTowerPosition;

        transform.forward = (projectileTowerPosition - transform.position).normalized;
        ProjectileEffect();
    }

    protected override void ProjectileEffect()
    {
        StartCoroutine(ProjectileEffectCo());
    }

    private IEnumerator ProjectileEffectCo()
    {
        yield return new WaitForSeconds(0.6f);
        projectileEvent.Post(gameObject);
        BaseEnemyClass frickThisGuy = target.GetComponent<BaseEnemyClass>();

        if (frickThisGuy != null)
        {
            frickThisGuy.Damage(damage);
        }
    }
}
