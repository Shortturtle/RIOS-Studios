using UnityEngine;
using System;
using Unity.VisualScripting;
using System.Collections;

public class JackInBoxTower : OffenseTowerBase
{
    protected override void Attack()
    {
        base.Attack();
        StartCoroutine(ViolentlyMolestingRyan());
    }

    IEnumerator ViolentlyMolestingRyan()
    {
        return null;
    }

    protected override void Degrade()
    {
        timeBetweenAttackValue = (float)Math.Round(timeBetweenAttacksBase * (1f - (0.5f * ((float)degradeRank / (float)maxDegradeRank))), 2);
        degradeRank++;
        ResetDegradeTimer();
    }
    
    protected override void OverDrive()
    {
        timeBetweenAttackValue = (float)Math.Round(timeBetweenAttacksBase * 1.5, 2);
        overdriveCountdownTimer = overdriveTimerDuration;
    }

    protected override void OverDriveEnd()
    {
        base.OverDriveEnd();
        damageValue = damageBase;
    }
}
