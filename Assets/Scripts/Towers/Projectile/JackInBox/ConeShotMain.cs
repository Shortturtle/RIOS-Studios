using JetBrains.Annotations;
using System.Collections.Generic;
using UnityEngine;

public class ConeShotMain : BaseProjectileClass
{
    public List<GameObject> coneProjectiles;
    public float spread;

    public override void InitializeProjectile(float projectileDamage, GameObject projectileTarget, Vector3 projectileTargetPosition)
    {
        int counter = 0;
        foreach (var projectile in coneProjectiles)
        {
            BaseProjectileClass temp = projectile.GetComponent<BaseProjectileClass>();
            if (temp != null)
            {
                Quaternion offset;
                switch (counter)
                {
                    case 0:
                    default:
                        offset = Quaternion.Euler(0,0,0);
                        break;
                    case 1:
                        offset = Quaternion.AngleAxis(spread, transform.up);
                        break;
                    case 2:
                        offset = Quaternion.AngleAxis(spread, -transform.up);
                        break;
                }
                temp.InitializeProjectile(projectileDamage, projectileTarget, projectileTargetPosition);
                temp.transform.forward = offset * (projectileTargetPosition - temp.transform.position).normalized;
                counter++;
            }
        }

        projectileEvent.Post(gameObject);
    }

    protected override void Update()
    {
        foreach (var projectile in coneProjectiles)
        {
            int deadProjectileCounter = 0;
            if (projectile == null)
            {
                deadProjectileCounter++;
            }

            if(deadProjectileCounter == coneProjectiles.Count)
            {
                Destroy(this.gameObject);
            }
        }
    }
}
