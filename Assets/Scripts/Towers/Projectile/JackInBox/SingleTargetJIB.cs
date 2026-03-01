using UnityEngine;

public class SingleTargetJIB : BaseProjectileClass
{
    public override void InitializeProjectile(float projectileDamage, GameObject projectileTarget, Vector3 projectileTargetPosition)
    {
        projectileEvent.Post(gameObject);
        base.InitializeProjectile(projectileDamage, projectileTarget, projectileTargetPosition);
    }
}
