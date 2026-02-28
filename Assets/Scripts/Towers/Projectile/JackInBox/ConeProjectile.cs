using UnityEngine;

public class ConeProjectile : BaseProjectileClass
{
    public ParticleSystem particleSys;
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
    }

    private void OnParticleCollision(GameObject other)
    {
        BaseEnemyClass enemy = other.GetComponent<BaseEnemyClass>();
        if (enemy != null)
        {
            target = other.gameObject;
            ProjectileEffect();
        }
    }
}
