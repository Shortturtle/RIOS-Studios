using UnityEngine;
using System;
using System.Collections;

public class SlowDiverTower : OffenseTowerBase
{
    public float slowRadius = 6f;
    public GameObject overdriveProjectile;
    public Animator animator;
    protected bool usedOverdriveAttack = true;

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
        if (attackEvent != null) attackEvent.Post(gameObject);
        SlowAoE.SlowRadius = slowRadius;
        overdriveCountdownTimer = overdriveTimerDuration;
        OverDriveAttack();
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
        yield return null;
        GameObject projectileInstance = Instantiate(overdriveProjectile, currentTarget.transform.position, Quaternion.identity);
        projectileInstance.GetComponent<BaseProjectileClass>().InitializeProjectile(damageValue, currentTarget, currentTarget.transform.position);
        usedOverdriveAttack = true;
    }

    protected override void OverDriveEnd()
    {
        base.OverDriveEnd();
    }
}
