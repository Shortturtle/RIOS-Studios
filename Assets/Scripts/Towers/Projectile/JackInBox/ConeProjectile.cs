using UnityEngine;

public class ConeProjectile : BaseProjectileClass
{
    public float deathTimer;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    protected override void Update()
    {
        deathTimer  -= Time.deltaTime;
        if (deathTimer < 0)
        {
            Destroy(gameObject);
        }

        base.Update();
    }

    protected override void ToTarget()
    {
        Vector3 dir = transform.forward;
        transform.Translate(dir.normalized * speed * Time.deltaTime, Space.World);
    }

    private void OnTriggerEnter(Collider other)
    {
        BaseEnemyClass enemy = other.GetComponent<BaseEnemyClass>();
        if (enemy != null)
        {
            target = other.gameObject;
            ProjectileEffect();
        }
    }
}
