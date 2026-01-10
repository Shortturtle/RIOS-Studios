using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseBossClass : BaseEnemyClass
{
    public List<BossAttack> attackList = new List<BossAttack>();
    private BossAttack lastBossAttack = null;
    private bool attacking = false;

    public float timeBetweenAttacks = 1f;
    public float bufferDuration;

    protected override void Start()
    {
        base.Start();
    }

    protected override void Update()
    {
        DistanceTracker();

        if (isRewinding)
        {
            Rewind();
        }

        else if (!isStunned && !attacking)
        {
            Record();
            MoveEnemy();
        }
    }

    protected virtual void AttackTimer()
    {
        
    }
}
