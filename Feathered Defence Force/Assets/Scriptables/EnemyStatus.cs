using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public abstract class EnemyStatus : ScriptableObject
{
    protected TimerScript pTimer = new TimerScript(0); // create timer
    public float duration; // how long until it game ends itself
    private float pTime = 0; //^
    [HideInInspector] public NewEnemy enemy; // the script its attached to
    

    internal virtual void Instantiate() //called on adding
    {
        TimerStart();
    }
    internal void Update()
    {
        //timer updater
        pTimer.Update();
        if(pTimer.Check())
        {
            TimerDone();
        }

        // duration to game end
        pTime += Time.deltaTime;
        if (pTime >= duration)
        {
            Remove();
        }
    }
    internal virtual void Remove()
    {
        enemy.statuses.Remove(this);
    }
    internal virtual void TimerDone()
    {
        pTimer.Restart();
    }
    internal virtual void TimerStart()
    {
        pTimer.Start(duration);
    }
}
