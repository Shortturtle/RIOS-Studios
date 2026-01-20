using UnityEngine;
using System;

public class PortalTower : OffenseTowerBase
{
    protected override void Attack()
    {
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
