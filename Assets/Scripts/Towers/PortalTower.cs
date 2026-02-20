using UnityEngine;
using System;

public class PortalTower : OffenseTowerBase
{
    public float portalDuration;
    public GameObject overdriveProjectile;

    protected override void Degrade()
    {
        PortalProjectile.portalDuration = ((float)PortalProjectile.portalDuration - (0.5f * ((float)degradeRank / (float)maxDegradeRank)));
        degradeRank++;
        ResetDegradeTimer();
    }

    protected override void OverDrive()
    {
        //stats.Projectile = overdriveProjectile;
        damageValue = (30);
        overdriveCountdownTimer = overdriveTimerDuration;
    }

    protected override void OverDriveEnd()
    {
        base.OverDriveEnd();
        damageValue = damageBase;
    }
}
