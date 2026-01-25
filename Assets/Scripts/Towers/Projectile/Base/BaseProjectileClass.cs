using UnityEngine;

public class BaseProjectileClass : MonoBehaviour
{
    [SerializeField] public float speed;
    [SerializeField] public AK.Wwise.Event projectileEvent;
    protected float damage;
    protected GameObject target;
    protected Vector3 targetPosition;

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

    public virtual void InitializeProjectile(float projectileDamage, GameObject projectileTarget, Vector3 projectileTargetPosition)
    {
        damage = projectileDamage;
        target = projectileTarget;
        targetPosition = projectileTargetPosition;
    }

    protected virtual void ToTarget()
    {
        Vector3 dir = targetPosition - transform.position;
        transform.Translate(dir.normalized * speed * Time.deltaTime, Space.World);

        if (Vector3.Distance(transform.position, targetPosition) <= 0.3f)
        {
            ProjectileEffect();
        }
    }

    protected virtual void ProjectileEffect()
    {
        BaseEnemyClass frickThisGuy = target.GetComponent<BaseEnemyClass>();

        if (frickThisGuy != null)
        {
            AudioManager.instance.PlayAudioEvent(projectileEvent);
            frickThisGuy.Damage(damage);
            Destroy(gameObject);
        }
    }
}
