using JetBrains.Annotations;
using System.Collections.Generic;
using UnityEngine;

public class ConeShotMain : BaseProjectileClass
{
    public List<GameObject> coneProjectiles;
    public float spread;

    public override void InitializeProjectile(float projectileDamage, GameObject projectileTarget, Vector3 projectileTargetPosition)
    {
        foreach (var projectile in coneProjectiles)
        {
            int counter = 0;
            BaseProjectileClass temp = projectile.GetComponent<BaseProjectileClass>();
            if (temp != null)
            {
                Vector3 offset;
                switch (counter)
                {
                    case 0:
                        offset = Vector3.zero;
                        break;
                    case 1:
                        break;
                }
                temp.InitializeProjectile(projectileDamage, projectileTarget, projectileTargetPosition);
            }
        }
    }

    protected override void Update()
    {
        foreach (var projectile in coneProjectiles)
        {
            int counter = 0;
            if (projectile == null)
            {
                counter++;
            }

            if(counter == coneProjectiles.Count)
            {
                Destroy(this.gameObject);
            }
        }
    }
}
