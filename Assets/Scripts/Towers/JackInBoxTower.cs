using UnityEngine;
using System;
using Unity.VisualScripting;
using System.Collections;

public class JackInBoxTower : OffenseTowerBase
{
    public Animator animator;
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
        StartCoroutine(PlayAnimation());
    }
    IEnumerator PlayAnimation()
    {
        if (animator)
            animator.SetBool("Attacking", true);
        yield return new WaitForSeconds(1f);
        base.Attack();
        yield return new WaitForSeconds(0.25f);
        if (animator)
            animator.SetBool("Attacking", false);
    }

    protected void SwapProjectile()
    {
        animator.SetBool("Gun", false);
        animator.SetBool("Megaphone", false);
        animator.SetBool("Firework", false);

        projectileIndex++;

        if (projectileIndex > 2)
        {
            projectileIndex = 0;
        }

        switch (projectileIndex)
        {
            case 0:
                projectile = singleTargetProjectile;
                animator.SetBool("Gun", true);
                break;
            case 1:
                projectile = coneShotProjectile;
                animator.SetBool("Megaphone", true);
                break;
            case 2:
                projectile = aoeProjectile;
                animator.SetBool("Firework", true);
                break;
        }
    }

    IEnumerator ViolentlyMolestingRyan()
    {
        return null;
    }

    protected override void Degrade()
    {
        timeBetweenAttackValue = (float)Math.Round(timeBetweenAttacksBase * (1f + (1f * ((float)degradeRank / (float)maxDegradeRank))), 2);
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
