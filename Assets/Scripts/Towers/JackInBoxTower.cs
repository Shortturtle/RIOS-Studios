using UnityEngine;
using System;
using Unity.VisualScripting;
using System.Collections;

public class JackInBoxTower : OffenseTowerBase
{
    public GameObject singleTargetProjectile;
    public GameObject coneShotProjectile;
    public GameObject aoeProjectile;

    protected int projectileIndex = 0;

    public override void InitializeTower()
    {
        base.InitializeTower();
    }
    protected override void Attack()
    {
        SwapProjectile();
        base.Attack();
    }

    protected void SwapProjectile()
    {
        projectileIndex++;

        if (projectileIndex > 2)
        {
            projectileIndex = 0;
        }

        switch (projectileIndex)
        {
            case 0:
                projectile = singleTargetProjectile;
                break;
            case 1:
                projectile = coneShotProjectile;
                break;
            case 2:
                projectile = aoeProjectile;
                break;
        }
    }

    IEnumerator ViolentlyMolestingRyan()
    {
        return null;
    }

    protected override void Degrade()
    {
        timeBetweenAttackValue = (float)Math.Round(timeBetweenAttacksBase * (1f + (0.5f * ((float)degradeRank / (float)maxDegradeRank))), 2);
        degradeRank++;
        ResetDegradeTimer();
    }
    
    protected override void OverDrive()
    {
        timeBetweenAttackValue = (float)Math.Round(timeBetweenAttacksBase * 0.5, 2);
        overdriveCountdownTimer = overdriveTimerDuration;
    }

    protected override void OverDriveEnd()
    {
        base.OverDriveEnd();
        timeBetweenAttackValue = timeBetweenAttacksBase;
    }
}
