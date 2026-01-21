using UnityEngine;
using System;

public class SlowDiverTower : OffenseTowerBase
{
    public GameObject overdriveProjectile;

    protected override void Degrade()
    {
        SlowAoE.SlowRadius = (float)Math.Round(SlowAoE.SlowRadius * (1f - (0.5f * ((float)degradeRank / (float)maxDegradeRank))), 2);
        degradeRank++;
        ResetDegradeTimer();
    }

    protected override void OverDrive()
    {
        //timeBetweenAttackValue = timeBetweenAttacksBase / 4;
        //attackTimer = 0;
        //overdriveCountdownTimer = overdriveTimerDuration;
        if(currentTarget != null)
        {
            AttackOverdrive();
        }
            
    }

    protected void AttackOverdrive()
    {
        attackEvent.Post(this.gameObject);
        GameObject projectileInstance = Instantiate(overdriveProjectile, bulletExitPoint.transform.position, Quaternion.identity);
        projectileInstance.GetComponent<BaseProjectileClass>().InitializeProjectile(damageValue, currentTarget, currentTarget.transform.position);
    }

    protected override void OverDriveEnd()
    {
        base.OverDriveEnd();
        damageValue = damageBase;
    }
}
