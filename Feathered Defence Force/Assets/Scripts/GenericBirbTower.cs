using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenericBirbTower : CommonInheritor
{
    public bool friendlyFire = false;
    public bool active = false;
    public bool audioOn;
    public bool canShoot;
    public int targetID;
    public bool canPlace;
    public SpriteRenderer birbSprite;
    public BirbRange range;
    public BirbInfo birbInfo;
    public EnemyStatus status;
    public List<Transform> targets;
    private TimerScript pTimerScript;

    internal virtual void Start()
    {
        Debug.Log(birbInfo.speed);
        pTimerScript = new TimerScript(birbInfo.speed);
        birbInfo = Instantiate(birbInfo);
        GetComponentInChildren<SpriteRenderer>().sprite = birbInfo.image;
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
            canShoot = false;
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
        return targets[0]!; // Do you mean null? What's with the ! being after the thingamabob??
    }



    public virtual void Hit(GameObject mHitObject)
    {
        if (audioOn)
        {
            Manager.PlayAudio(17, 1);
        }
        
        NewEnemy enemy = mHitObject.GetComponent<NewEnemy>();
        enemy.Damage(birbInfo.damage, birbInfo.status);

    }

    public void PlaceTower()
    {
        Debug.Log("tf2 sexupdate");
        active = true;
        transform.position = new Vector3(transform.position.x, transform.position.y, 44);
        range.showCollider = false;
    }
}
