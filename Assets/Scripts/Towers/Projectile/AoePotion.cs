using UnityEngine;
using UnityEngine.EventSystems;

public class AoEPotion : BaseProjectileClass
{
    public Transform pivot, model;
    public float AoERange;
    public GameObject potion;

    public Vector3 start;
    public Vector3 crtl1;
    public Vector3 crtl2;
    public Vector3 end;

    private float t = 0;
    public float arcHeight = 5f;

    private void Start()
    {
        //Vector3 startPosition = transform.position;

        //Vector3 centerPosition = (transform.position + targetPosition) * 2f;
        //transform.position = centerPosition;

        //transform.forward = targetPosition - transform.position;

        //model.transform.position = startPosition;

        start = model.transform.position;
        end = targetPosition;

        crtl1 = start + (Vector3.up * arcHeight);
        crtl2 = end + (Vector3.up * arcHeight);
    }

    Vector3 CalculateBezierPoint(float t, Vector3 p0, Vector3 p1, Vector3 p2, Vector3 p3)
    {
        // Clamp t between 0 and 1
        t = Mathf.Clamp01(t);
        float u = 1 - t;
        float u2 = u * u;
        float u3 = u2 * u;
        float t2 = t * t;
        float t3 = t2 * t;

        Vector3 result =
            (u3) * p0 +
            (3f * u2 * t) * p1 +
            (3f * u * t2) * p2 +
            (t3) * p3;

        return result;
    }

    protected override void ToTarget() //put the lob
    {
        //pivot.localRotation = Quaternion.RotateTowards(pivot.localRotation, Quaternion.Euler(0f, 0f, 180f), speed * Time.deltaTime);
        //model.rotation = Quaternion.identity;

        t += Time.deltaTime * speed;

        transform.position = CalculateBezierPoint(t, start, crtl1, crtl2, end);

        if (Vector3.Distance(transform.position, targetPosition) <= 0.3f)
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
