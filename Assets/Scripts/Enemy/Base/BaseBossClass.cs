using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseBossClass : BaseEnemyClass
{
    protected override void Start()
    {
        base.Start();
    }
    public override void InitializeEnemy_Start(WaypointManager wM)
    {
        base.InitializeEnemy_Start(wM);
        recordTime = 3f;
    }

    protected override void Update()
    {
        DistanceTracker();

        if (isRewinding)
        {
            Rewind();
        }

        else if (!isStunned)
        {
            Record();
            MoveEnemy();
        }
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
