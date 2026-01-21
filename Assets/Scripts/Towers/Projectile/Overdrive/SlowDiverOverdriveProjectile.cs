using Unity.VisualScripting;
using UnityEngine;
using System.Collections;

public class SlowDiverOverdriveProjectile : BaseProjectileClass
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
        StartCoroutine(pushBack());
    }

    IEnumerator pushBack()
    {
        Collider[] collidersInRange = Physics.OverlapSphere(transform.position, AttackRadius);

        foreach (Collider hit in collidersInRange)
        {
            BaseEnemyClass enemy = hit.gameObject.GetComponent<BaseEnemyClass>();
            if(enemy != null)
            {
                enemy.StartRewind();
                Debug.Log("Enemy rewind started.");
            }
                
        }

        yield return new WaitForSeconds(2f);
        Destroy(gameObject);
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, AttackRadius);
    }
}
