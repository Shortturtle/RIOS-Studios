using UnityEngine;
using System.Collections;

public class NetTosserTower : OffenseTowerBase
{
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
        yield return new WaitForSeconds(1f);
        if (animator)
            animator.SetBool("Attacking", false);
    }
}
