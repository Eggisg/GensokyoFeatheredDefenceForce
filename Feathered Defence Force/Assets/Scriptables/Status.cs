using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public abstract class Status : ScriptableObject
{
    protected TimerScript pTimer;
    internal abstract void AddStatus(NewEnemy enemy);
    internal virtual void Update()
    {
        pTimer.Update();
    }
    internal abstract void Remove();
}
