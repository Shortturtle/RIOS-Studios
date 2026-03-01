using UnityEngine;
using System;
using System.Collections;

public class AxeMeleeTower : OffenseTowerBase
{
    public Animator animator;
    public GameObject overdriveAttack;
    public bool usedOverdriveAttack = true;

    protected override void Degrade()
    {
        damageValue = (float)Math.Round(damageBase * (1f - (0.5f * ((float)degradeRank / (float)maxDegradeRank))), 2);
        degradeRank++;
        ResetDegradeTimer();
    }

    protected override void Attack()
    {
        if (!usedOverdriveAttack) { return ; }

        StartCoroutine(PlayAnimation());
    }

    IEnumerator PlayAnimation()
    {
        if (animator)
            animator.SetBool("Attacking", true);
        yield return new WaitForSeconds(0.8f);
        base.Attack();
        yield return new WaitForSeconds(0.4f);
        if (animator)
            animator.SetBool("Attacking", false);
    }

    protected override void OverDrive()
    {
        damageValue = damageBase * 2f;
        OverDriveAttack();
        overdriveCountdownTimer = overdriveTimerDuration;
    }

    protected void OverDriveAttack()
    {
        if (!usedOverdriveAttack) { return; }

        usedOverdriveAttack = false;

        if (currentTarget == null)
        {
            StartCoroutine(OverDriveAttackStall());
        }

        else
        {
            StartCoroutine(OverdriveAttackCo());
        }
    }
    protected IEnumerator OverdriveAttackCo()
    {
        if (animator)
            animator.SetBool("Overdive", true);
        yield return new WaitForSeconds(0.5f);
        GameObject tempOverdrive = Instantiate(overdriveAttack, transform.position, Quaternion.identity);
        tempOverdrive.GetComponent<BaseProjectileClass>().InitializeProjectile(damageValue * damageMod, currentTarget, currentTarget.transform.position);
        yield return new WaitForSeconds(2.25f);
        if (animator)
            animator.SetBool("Overdive", false);
        usedOverdriveAttack = true;
    }

    protected IEnumerator OverDriveAttackStall()
    {
        while (currentTarget == null)
        {
            Debug.Log("No target to overdrive");
            yield return null;
        }

        StartCoroutine(OverdriveAttackCo());
    }

    protected override void OverDriveEnd()
    {
        base.OverDriveEnd();
        damageValue = damageBase;
    }
}
