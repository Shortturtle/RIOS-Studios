using UnityEngine;
using System;
using System.Collections;

public class PotionThrowerTower : OffenseTowerBase
{
    public Animator animator;

    protected override void Degrade()
    {
        damageValue = (float)Math.Round(damageBase * (1f - (0.5f * ((float)degradeRank / (float)maxDegradeRank))), 2);
        degradeRank++;
        ResetDegradeTimer();
    }
    protected override void Attack()
    {
        StartCoroutine(PlayAnimation());
    }

    IEnumerator PlayAnimation()
    {
        if (animator)
            animator.SetBool("Throwing", true);
        yield return new WaitForSeconds(0.5f);
        base.Attack();
        yield return new WaitForSeconds(1f);
        if (animator)
            animator.SetBool("Throwing", false);
    }

    protected override void OverDrive()
    {
        damageValue = (float)Math.Round(damageBase * 1.5, 2);
        overdriveCountdownTimer = overdriveTimerDuration;
    }

    protected override void OverDriveEnd()
    {
        base.OverDriveEnd();
        damageValue = damageBase;
    }
}
