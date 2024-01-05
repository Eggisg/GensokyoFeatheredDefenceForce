using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireStatus : Status
{
    public float damage;
    public float damageDelay;
    public Sprite flamePNG;

    internal override void AddStatus(Enemy enemy)
    {
        enemy.statuses.Add(this);
        pTimer.Start(damageDelay);

        //create flame sprite object on the enemy or enable one
    }

    internal override void Update()
    {
        pTimer.Update();

    }
    internal override void Remove()
    {

    }

}
