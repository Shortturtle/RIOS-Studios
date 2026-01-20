using UnityEngine;
using System;

public class PortalTower : OffenseTowerBase
{
    public GameObject overdriveProjectile;

    protected override void Attack()
    {
        base.Attack();
    }

    protected override void OverDrive()
    {
        stats.Projectile = overdriveProjectile;
        damageValue = (20);
        overdriveCountdownTimer = overdriveTimerDuration;
    }

    protected override void OverDriveEnd()
    {
        base.OverDriveEnd();
        damageValue = damageBase;
    }
}
