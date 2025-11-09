using UnityEngine;

public class BaseProjectileClass : MonoBehaviour
{
    [SerializeField] public float speed;
    protected float damage;
    protected GameObject target;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    protected virtual void Update()
    {
        if (target != null)
        {
            ToTarget();
        }

        if (target == null)
        {
            Destroy(gameObject);
        }
    }

    public virtual void InitializeProjectile(float projectileDamage, GameObject projectileTarget)
    {
        damage = projectileDamage;
        target = projectileTarget;
    }

    protected virtual void ToTarget()
    {
        Vector3 dir = target.transform.position - transform.position;
        transform.Translate(dir.normalized * speed * Time.deltaTime, Space.World);

        if (Vector3.Distance(transform.position, target.transform.position) <= 0.3f)
        {
            ProjectileEffect();
        }
    }

    protected virtual void ProjectileEffect()
    {
        BaseEnemyClass fuckThisGuy = target.GetComponent<BaseEnemyClass>();

        if (fuckThisGuy != null)
        {
            fuckThisGuy.Damage(damage);
            Destroy(gameObject);
        }
    }
}
