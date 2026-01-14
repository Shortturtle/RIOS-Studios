using UnityEngine;

public class SlowDiverTower : OffenseTowerBase
{
    protected override void OverDrive()
    {
        timeBetweenAttackValue = timeBetweenAttacksBase / 4;
        attackTimer = 0;
        overdriveCountdownTimer = overdriveTimerDuration;
    }
}
