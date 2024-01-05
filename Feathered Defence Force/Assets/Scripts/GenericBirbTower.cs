using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenericBirbTower : CommonInheritor
{
    public bool active = false;
    public bool audioOn;
    private TimerScript pTimerScript;
    public BirbInfo birbInfo;
    public List<CommonInheritor> targets;
    public bool canShoot;
    public Status status;

    internal virtual void Update()
    {
        if (active)
        {
            pTimerScript.Update();

            if (pTimerScript.Check())
            {

            }
        }
    }

    public void ToggleAudio()
    {
        audioOn = !audioOn;
    }

    private void PlayAudio(int mID)
    {
        if (audioOn)
        {
            Manager.PlayAudio(mID);
        }
    }

    public virtual void Shoot()
    {
        if (canShoot)
        {
            //create object
        }
    }
    public virtual void Hit(GameObject mHitObject)
    {
        NewEnemy enemy = mHitObject.GetComponent<NewEnemy>();

    }

}
