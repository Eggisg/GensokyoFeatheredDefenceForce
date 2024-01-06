using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireStatus : EnemyStatus
{
    public float damage;
    public float damageDelay;
    public Sprite flamePNG;
    

    internal override void AddStatus(NewEnemy enemy)
    {


        base.AddStatus(enemy);
    }

    internal override void TimerDone()
    {
        pEnemy.Damage(damage * pEnemy.fireMultiplier);

        base.TimerDone();
    }

    internal override void TimerStart()
    {
        pTimer.Start(damageDelay);
    }



}
