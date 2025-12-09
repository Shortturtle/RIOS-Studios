using UnityEngine;

public class RailGunnerLaser : BaseProjectileClass
{
    public float lifetime = 0.5f;

    protected override void Update()
    {
        lifetime -= Time.deltaTime;

        if ( lifetime >= 0)
        {
            Destroy(gameObject);
        }
    }

    public override void InitializeProjectile(float projectileDamage, GameObject projectileTarget)
    {
        damage = projectileDamage;
        target = projectileTarget;

        Vector3 midpointBetweenVectors = new Vector3
            (
            (projectileTarget.transform.position.x - transform.position.x)/2,
            (projectileTarget.transform.position.y - transform.position.y)/2,
            (projectileTarget.transform.position.z - transform.position.z)/2
            );

        transform.position = midpointBetweenVectors;

        float distanceBetweenTargets = Vector3.Distance(projectileTarget.transform.position, target.transform.position);

        transform.localScale = new Vector3(distanceBetweenTargets / 2, transform.localScale.y, transform.localScale.z);
        transform.LookAt(projectileTarget.transform);
        ProjectileEffect();
    }

    protected override void ProjectileEffect()
    {
        BaseEnemyClass frickThisGuy = target.GetComponent<BaseEnemyClass>();

        if ( frickThisGuy != null)
        {
            frickThisGuy.Damage(damage);
        }
    }
}
