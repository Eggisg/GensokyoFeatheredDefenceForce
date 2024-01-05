using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenericBirbTower : CommonInheritor
{
    public bool friendlyFire = false;
    public bool active = false;
    public bool audioOn;
    private TimerScript pTimerScript;
    public BirbInfo birbInfo;
    public List<Transform> targets;
    public bool canShoot;
    public Status status;
    public int targetID;

    internal virtual void Start()
    {
        Debug.Log(birbInfo.speed);
        pTimerScript = new TimerScript(birbInfo.speed);
    }

    internal virtual void Update()
    {

        
        if (active)
        {
            if (targets.Count > 0)
            {
                Shoot();
            }

            pTimerScript.Update();
            if (pTimerScript.Check())
            {
                Debug.Log("canshoot = true");
                pTimerScript.Start(birbInfo.speed);
                canShoot = true;
                
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
        //create object
        if (canShoot)
        {
            GameObject mBullet = Instantiate(birbInfo.throwablePrefab, transform.position, Quaternion.identity, transform);
            ThrownObject mBulletScript = mBullet.GetComponent<ThrownObject>();
            mBulletScript.OriginTower = this;
            mBulletScript.target = Targeting(targetID);
        }


    }

    public Transform Targeting(int mID)
    {
        Mathf.Clamp(mID, 1, 3);
        if (1 == mID) //furthest
        {

        }
        else if (2 == mID) //!furthest
        {

        }
        else if (3 == mID) //highest HP
        {

        }
        return targets[0]!;
    }



    public virtual void Hit(GameObject mHitObject)
    {
        NewEnemy enemy = mHitObject.GetComponent<NewEnemy>();

    }

}
