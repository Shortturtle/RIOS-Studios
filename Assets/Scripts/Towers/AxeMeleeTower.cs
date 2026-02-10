using UnityEngine;
using System;

public class AxeMeleeTower : OffenseTowerBase
{
    protected override void Degrade()
    {
        damageValue = (float)Math.Round(damageBase * (1f - (0.5f * ((float)degradeRank / (float)maxDegradeRank))), 2);
        degradeRank++;
        ResetDegradeTimer();
    }

    protected override void Attack()
    {
        base.Attack();
    }

    protected override void OverDrive()
    {
        timeBetweenAttackValue = (float)Math.Round(timeBetweenAttacksBase * 2, 2);
        overdriveCountdownTimer = overdriveTimerDuration;
    }

    protected override void OverDriveEnd()
    {
        base.OverDriveEnd();
        damageValue = damageBase;
    }
}
