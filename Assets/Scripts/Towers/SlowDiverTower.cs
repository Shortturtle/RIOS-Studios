using UnityEngine;
using System;
using System.Collections;

public class SlowDiverTower : OffenseTowerBase
{
    public GameObject overdriveProjectile;
    public Animator animator;

    protected override void Attack()
    {
        StartCoroutine(PlayAnimation());
    }

    IEnumerator PlayAnimation()
    {
        if (animator)
            animator.SetBool("Attacking", true);
        yield return new WaitForSeconds(0.5f);

        base.Attack();

        yield return new WaitForSeconds(5f);
        if (animator)
            animator.SetBool("Attacking", false);
    }

    protected override void Degrade()
    {
        SlowAoE.SlowRadius = (float)Math.Round(SlowAoE.SlowRadius * (1f - (0.5f * ((float)degradeRank / (float)maxDegradeRank))), 2);
        degradeRank++;
        ResetDegradeTimer();
    }

    protected override void OverDrive()
    {
        if (animator)
            animator.SetBool("OverDrive", true);

        timeBetweenAttackValue = timeBetweenAttacksBase / 4;
        attackTimer = 0;
        overdriveCountdownTimer = overdriveTimerDuration;
        //if(currentTarget != null)
        //{
        //    AttackOverdrive();
        //}

    }

    protected void AttackOverdrive()
    {
        attackEvent.Post(this.gameObject);
        GameObject projectileInstance = Instantiate(overdriveProjectile, bulletExitPoint.transform.position, Quaternion.identity);
        projectileInstance.GetComponent<BaseProjectileClass>().InitializeProjectile(damageValue, currentTarget, currentTarget.transform.position);
    }

    protected override void OverDriveEnd()
    {
        if (animator)
            animator.SetBool("OverDrive", false);

        base.OverDriveEnd();
        damageValue = damageBase;
    }
}
