using UnityEngine;
using UnityEngine.EventSystems;
using System.Collections;

public class AoEPotion : BaseProjectileClass
{
    public float AoERange;
    public GameObject potion;
    public GameObject potionVFX;

    [Header("Bezier Curve Points")]
    public Vector3 start;
    public Vector3 crtl1;
    public Vector3 crtl2;
    public Vector3 end;

    private float t = 0;
    public float arcHeight = 1f;

    private void Start()
    {
        start = transform.position;
        end = targetPosition;

        //Calculate control points for the Bezier curve
        crtl1 = start + (Vector3.up * arcHeight);
        crtl2 = end + (Vector3.up * arcHeight);
    }

    Vector3 CalculateBezierPoint(float t, Vector3 p0, Vector3 p1, Vector3 p2, Vector3 p3)
    {
        //Clamp t between 0 and 1
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

    protected override void ToTarget() //Lobbing motion using Bezier curve
    {
        t += Time.deltaTime * speed;

        transform.position = CalculateBezierPoint(t, start, crtl1, crtl2, end);

        if (Vector3.Distance(transform.position, targetPosition) <= 0.3f)
        {
            ProjectileEffect();
        }
    }

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
        Instantiate(potionVFX, transform.position, Quaternion.identity);
        Destroy(gameObject);
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, AoERange);
    }
}
