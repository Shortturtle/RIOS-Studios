using UnityEngine;
using System.Collections;

public class PortalTower : OffenseTowerBase
{
    public float portalDuration;
    public Animator animator;
    public GameObject portalOverdriveVFX;
    public bool usedOverdriveAttack = true;

    protected override void Attack()
    {
        StartCoroutine(PlayAnimation());
    }

    IEnumerator PlayAnimation()
    {
        if (animator)
            animator.SetBool("Bazinging", true);
        yield return new WaitForSeconds(0.5f);
        base.Attack();
        yield return new WaitForSeconds(2f);
        if (animator)
            animator.SetBool("Bazinging", false);
    }

    protected override void Degrade()
    {
        PortalProjectile.portalDuration = ((float)PortalProjectile.portalDuration - (0.5f * ((float)degradeRank / (float)maxDegradeRank)));
        degradeRank++;
        ResetDegradeTimer();
    }

    protected override void OverDrive()
    {
        if (attackEvent != null) attackEvent.Post(gameObject);
        PortalProjectile.portalDuration = portalDuration;
        overdriveCountdownTimer = overdriveTimerDuration;
        OverDriveAttack();
    }

    protected void OverDriveAttack()
    {
        if(!usedOverdriveAttack) { return; }

        usedOverdriveAttack = false;

        if(currentTarget == null)
        {
            StartCoroutine(OverDriveAttackStall());
        }

        else
        {
            GameObject projectileInstance = Instantiate(portalOverdriveVFX, bulletExitPoint.transform.position, Quaternion.identity);
            projectileInstance.transform.forward = new Vector3((currentTarget.transform.position.x - bulletExitPoint.transform.position.x), 0, (currentTarget.transform.position.z - bulletExitPoint.transform.position.z)).normalized;
            projectileInstance.GetComponent<BaseProjectileClass>().InitializeProjectile(damageValue, currentTarget, currentTarget.transform.position);
            usedOverdriveAttack = true;
        }
    }

    protected IEnumerator OverDriveAttackStall()
    {
        while (currentTarget == null)
        {
            Debug.Log("No target to overdrive");
            yield return null;
        }

        GameObject projectileInstance = Instantiate(portalOverdriveVFX, bulletExitPoint.transform.position, Quaternion.identity);
        projectileInstance.transform.forward = (currentTarget.transform.position - bulletExitPoint.transform.position).normalized;
        projectileInstance.GetComponent<BaseProjectileClass>().InitializeProjectile(damageValue, currentTarget, currentTarget.transform.position);
        usedOverdriveAttack = true;
    }

    protected override void OverDriveEnd()
    {
        base.OverDriveEnd();
    }
}
