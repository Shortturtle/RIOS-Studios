using UnityEngine;

public class DiverOverdrive : BaseProjectileClass
{
    public float lifeTime = 2f;
    public Vector3 boxSize;
    public GameObject waveTriggerCollider;

    public override void InitializeProjectile(float projectileDamage, GameObject projectileTarget, Vector3 projectileTargetPosition)
    {
        base.InitializeProjectile(projectileDamage, projectileTarget, projectileTargetPosition);
        transform.position = target.transform.position + (target.transform.forward * 2f);
        transform.forward = (target.transform.position - transform.position).normalized;
    }
    protected override void ProjectileEffect()
    {
        Collider[] hits = Physics.OverlapBox(waveTriggerCollider.transform.position, boxSize);

        foreach (Collider col in hits)
        {
            BaseEnemyClass enemy = col.GetComponent<BaseEnemyClass>();
            if (enemy != null)
            {
                BaseEnemyClass.PointInTime pointInTime = enemy.pointsInTime[4];
                enemy.transform.position = pointInTime.position;
                enemy.transform.rotation = pointInTime.rotation;
                enemy.distanceTravelled = pointInTime.distance;
                enemy.waypointIndex = pointInTime.waypointIndex;
                enemy.target = pointInTime.target;

                enemy.pointsInTime.RemoveRange(0,4);

                Debug.Log("Enemy Damaged");
            }
        }
    }

    protected override void ToTarget()
    {
        waveTriggerCollider.transform.Translate(Vector3.forward * speed *Time.deltaTime);
        ProjectileEffect();
    }

    protected override void Update()
    {
        lifeTime -= Time.deltaTime;

        if (lifeTime < 0)
        {
            Destroy(gameObject);
        }

        ToTarget();
    }

    protected void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireCube(waveTriggerCollider.transform.position, boxSize);
    }
}
