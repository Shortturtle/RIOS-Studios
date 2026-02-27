using UnityEngine;
using System.Collections;

public class PortalTower : OffenseTowerBase
{
    public float portalDuration;
    public Animator animator;

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
        damageValue = (30);
        overdriveCountdownTimer = overdriveTimerDuration;
    }

    protected override void OverDriveEnd()
    {
        base.OverDriveEnd();
        damageValue = damageBase;
    }
}
