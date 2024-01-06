using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New FireStatus", menuName = "Scriptable/Status/eFire"), Serializable]
public class FireStatus : EnemyStatus
{
    public float damage;
    public float damageDelay;
    

    internal override void Instantiate()
    {
        base.Instantiate();
    }

    internal override void TimerDone()
    {
        enemy.Damage(damage * enemy.fireMultiplier);

        base.TimerDone();
    }

    internal override void TimerStart()
    {
        pTimer.Start(damageDelay);
    }



}
