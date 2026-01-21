using UnityEngine;

public class AOEAtkJIB : BaseProjectileClass
{
    public float AoERange;
    public GameObject atkProjectile;

    protected override void ProjectileEffect() //AoE damage
    {
        Collider[] collidersInRange = Physics.OverlapSphere(transform.position, AoERange);

        foreach (Collider col in collidersInRange)
        {
            BaseEnemyClass frickThisGuy = col.GetComponent<BaseEnemyClass>();

            if (frickThisGuy != null)
            {
                projectileEvent.Post(this.gameObject);
                frickThisGuy.Damage(damage);
            }
        }

        Destroy(gameObject);
    }


    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, AoERange);
    }
}
