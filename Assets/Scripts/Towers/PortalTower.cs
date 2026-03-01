using UnityEngine;
using System.Collections;

public class PortalTower : OffenseTowerBase
{
    public float portalDuration;
    public Animator animator;
    public GameObject portalOverdriveVFX;

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
        GameObject projectileInstance = Instantiate(portalOverdriveVFX, bulletExitPoint.transform.position, Quaternion.identity);
        projectileInstance.GetComponent<BaseProjectileClass>().InitializeProjectile(30f, currentTarget, currentTarget.transform.position);

        overdriveCountdownTimer = overdriveTimerDuration;
    }

    protected override void OverDriveEnd()
    {
        base.OverDriveEnd();
        damageValue = damageBase;
    }
}
