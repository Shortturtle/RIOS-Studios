using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseBossClass : BaseEnemyClass
{
    private bool attacking = false;

    public float timeBetweenAttacks = 1f;
    public float bufferDuration;

    protected override void Start()
    {
        base.Start();
    }
    public override void InitializeEnemy_Start(WaypointManager wM)
    {
        base.InitializeEnemy_Start(wM);
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
            AttackTimer();
            Record();
            MoveEnemy();
        }
    }

    protected virtual void AttackTimer()
    {
        
    }

    public override void Stun(float duration)
    {
        StartCoroutine(StunRoutine(duration/2));
    }

    public override void freeze(float duration)
    {
        StartCoroutine(FreezeRoutine(duration/2));
    }
}
