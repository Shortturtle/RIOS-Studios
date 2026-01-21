using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;
using static BaseEnemyClass;

public class PortalProjectile : BaseProjectileClass
{
    public float AoERange = 1f;
    public static float portalDuration = 5f;
    public GameObject Portal;

    private void Start()
    {
        Physics.Raycast(targetPosition, Vector3.down, out RaycastHit hitInfo, 5f, LayerMask.GetMask("Path"));
        if (hitInfo.collider != null) //Check if the path is there
        {
            targetPosition = hitInfo.point;
        }
    }

    protected override void ToTarget()
    {
        Vector3 dir = targetPosition - transform.position;
        transform.Translate(dir.normalized * speed * Time.deltaTime, Space.World);

        if (Vector3.Distance(transform.position, targetPosition) <= 0.3f)
        {
            ProjectileEffect();
        }
    }

    protected override void ProjectileEffect() //Start Coroutine for the AoE and StunDoT
    {
        StartCoroutine(frickYouRyanTohJunAn());
    }

    IEnumerator frickYouRyanTohJunAn()
    {
        Collider[] collidersInRange = Physics.OverlapSphere(transform.position, AoERange);

        foreach (Collider hit in collidersInRange)
        {
            BaseEnemyClass enemy = hit.GetComponent<BaseEnemyClass>();
            if (enemy != null)
            {
                //Get the last point in time and teleport the enemy theres
                BaseEnemyClass.PointInTime pointInTime = enemy.pointsInTime[enemy.pointsInTime.Count - 1];
                enemy.transform.position = pointInTime.position;

                enemy.pointsInTime.Clear();                                                                         //Clear the rewinding points to prevent issues(teleporting all over the place)
            }
        }

        yield return new WaitForSeconds(portalDuration);
        Destroy(gameObject);
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.black;
        Gizmos.DrawWireSphere(transform.position, AoERange);
    }
}
