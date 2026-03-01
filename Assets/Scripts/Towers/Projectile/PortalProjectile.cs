using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.UIElements;
using static BaseEnemyClass;

public class PortalProjectile : BaseProjectileClass
{
    public static float portalDuration = 3f;
    public PlayableDirector playableDirector;
    public double loopStartTime = 2f;
    public double loopEndTime = 3f;
    public AK.Wwise.Event portalOpenEvent;
    public AK.Wwise.Event portalCloseEvent;
    private bool active = false;

    private void Start()
    {
        Physics.Raycast(targetPosition, Vector3.down, out RaycastHit hitInfo, 5f, LayerMask.GetMask("Path"));
        if (hitInfo.collider != null) //Check if the path is there
        {
            targetPosition = hitInfo.point;
        }

        Debug.Log(portalDuration);

        ProjectileEffect();
    }

    protected override void ProjectileEffect()
    {
        transform.position = target.transform.position + (target.transform.forward * 2f);
        transform.forward = (target.transform.position - transform.position).normalized;
        StartCoroutine(moveThisGuy());
    }

    protected override void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!active) { return; }

        BaseEnemyClass enemy = other.GetComponent<BaseEnemyClass>();
        if (enemy != null)
        {
            //Get the last point in time and teleport the enemy theres
            BaseEnemyClass.PointInTime pointInTime = enemy.pointsInTime[enemy.pointsInTime.Count - 1];
            enemy.transform.position = pointInTime.position;
            enemy.transform.rotation = pointInTime.rotation;
            enemy.distanceTravelled = pointInTime.distance;
            enemy.waypointIndex = pointInTime.waypointIndex;
            enemy.target = pointInTime.target;

            enemy.pointsInTime.Clear(); //Clear the rewinding points to prevent issues(teleporting all over the place)
            enemy.Damage(damage);        }
    }

    private IEnumerator moveThisGuy()
    {
        playableDirector.Play();
        active = true;
        portalOpenEvent.Post(gameObject);   

        while (playableDirector.time  <=  loopStartTime)
        {
            yield return null;
        }

        float timer = 0;

        while (timer < portalDuration)
        {
            timer += Time.deltaTime;
            if (playableDirector.time >= loopEndTime)
            {
                playableDirector.time = loopStartTime;
            }

            yield return null;
        }
        
        active = false;
        portalCloseEvent.Post(gameObject);
        playableDirector.time = loopEndTime;

        while(playableDirector.time <= playableDirector.duration)
        {
            if (playableDirector.time == playableDirector.duration)
            {
                Destroy(gameObject);    
            }

            yield return null;
        }
    }
}
