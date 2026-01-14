using Unity.VisualScripting;
using UnityEngine;
using System.Collections;

public class SlowAoE : BaseProjectileClass
{
    public float SlowRadius = 10f;
    public float slowDuration = 3f;

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

    protected override void ProjectileEffect() //Start Coroutine for the Slow AoE
    {
        StartCoroutine(slowAoe());
    }

    IEnumerator slowAoe()
    {
        Collider[] collidersInRange = Physics.OverlapSphere(transform.position, SlowRadius);

        foreach (Collider hit in collidersInRange)
        {
            BaseEnemyClass enemy = hit.GetComponent<BaseEnemyClass>();
            if (enemy != null)
            {
                enemy.speed *= 0.5f; //Half their speed

                yield return new WaitForSeconds(slowDuration);

                enemy.speed *= 2f; //Reset speed to normal
            }
        }

        Destroy(gameObject);
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, SlowRadius);
    }
}
