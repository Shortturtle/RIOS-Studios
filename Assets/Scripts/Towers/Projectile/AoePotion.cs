using UnityEngine;
using UnityEngine.EventSystems;

public class AoEPotion : BaseProjectileClass
{
    public Transform pivot, model;
    public float AoERange;
    public GameObject potion;

    private void Start()
    {
        Vector3 startPosition = transform.position;

        Vector3 centerPosition = (transform.position + targetPosition) * 2f;
        transform.position = centerPosition;

        transform.forward = targetPosition - transform.position;

        model.transform.position = startPosition;
    }

    protected override void ToTarget() //put the lob
    {
        pivot.localRotation = Quaternion.RotateTowards(pivot.localRotation, Quaternion.Euler(0f, 0f, 180f), speed * Time.deltaTime);
        model.rotation = Quaternion.identity;

        if (Vector3.Distance(model.transform.position, targetPosition) <= 0.3f)
        {
            ProjectileEffect();
        }
    }

    protected override void ProjectileEffect() //aoe
    {
        Collider[] collidersInRange = Physics.OverlapSphere(model.transform.position, AoERange);

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
}
