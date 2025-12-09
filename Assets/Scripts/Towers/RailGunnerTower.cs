using System;
using UnityEngine;

public class RailGunnerTower : OffenseTowerBase
{
    protected override void Degrade()
    {
        damageValue = (float)Math.Round(damageBase * (1f - (0.25f * (degradeRank / maxDegradeRank))), 2);
    }

    protected override void Attack()
    {
        base.Attack();
    }

    protected override void OverDrive()
    {
        damageValue = (float) Math.Round(damageBase * 1.5, 2);
    }

    protected override void OverDriveEnd()
    {
        damageValue = damageBase;
    }
}
