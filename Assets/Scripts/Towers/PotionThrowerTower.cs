using UnityEngine;
using System;

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
        animator.SetBool("Throwing", true);
        base.Attack();
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
