using UnityEngine;

public class RailGunnerLaser : BaseProjectileClass
{
    public float lifetime = 1f;
    public GameObject laser;

    protected override void Update()
    {
        lifetime -= Time.deltaTime;

        if ( lifetime <= 0)
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
            (target.transform.position.x + transform.position.x)/2,
            (target.transform.position.y + transform.position.y)/2,
            (target.transform.position.z + transform.position.z)/2
            );

        transform.position = midpointBetweenVectors;

        float distanceBetweenTargets = Vector3.Distance(projectileTarget.transform.position, transform.position);

        laser.transform.localScale = new Vector3(laser.transform.localScale.x, distanceBetweenTargets, laser.transform.localScale.z);
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
