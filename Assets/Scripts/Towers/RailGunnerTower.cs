using System;
using UnityEngine;
using System.Collections;
using UnityEngine.Playables;

public class RailGunnerTower : OffenseTowerBase
{
    public Animator animator;
    public GameObject chargeUpVFX;
    protected override void Degrade()
    {
        damageValue = (float)Math.Round(damageBase * (1f - (0.5f * ((float)degradeRank / (float)maxDegradeRank))), 2);
        degradeRank++;
        ResetDegradeTimer();
    }

    protected override void Attack()
    {
        StartCoroutine(PlayAnimation());
    }

    IEnumerator PlayAnimation()
    {
        if (animator)
            animator.SetBool("Shooting", true);
        yield return new WaitForSeconds(0.2f);

        base.Attack();

        yield return new WaitForSeconds(1.6f);
        if (animator)
            animator.SetBool("Shooting", false);
        GameObject chargeUpVFXInstance = Instantiate(chargeUpVFX, bulletExitPoint.transform.position, Quaternion.identity);
        chargeUpVFXInstance.transform.parent = bulletExitPoint.transform;
        yield return new WaitForSeconds((float)chargeUpVFXInstance.GetComponent<PlayableDirector>().duration);
        Destroy(chargeUpVFXInstance);
    }

    protected override void OverDrive()
    {
        damageValue = (float) Math.Round(damageBase * 1.5, 2);
        overdriveCountdownTimer = overdriveTimerDuration;
    }

    protected override void OverDriveEnd()
    {
        base.OverDriveEnd();
        damageValue = damageBase;
    }
}
