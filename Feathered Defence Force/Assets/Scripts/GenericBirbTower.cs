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
    [HideInInspector] public int targetID;
    public bool canPlace;
    public bool mouseHovering;
    public SpriteRenderer birbSprite;
    public GameObject birbMenuPrefab;
    private GameObject birbmenu;
    public BirbRange range;
    public BirbInfo birbInfo;
    public EnemyStatus status;
    public List<Transform> targets;
    private TimerScript pTimerScript;


    [Header("MovingBirb")]
    public bool movingBirb;

    internal virtual void Start()
    {
        Debug.Log(birbInfo.speed);
        pTimerScript = new TimerScript(birbInfo.speed);
        birbInfo = Instantiate(birbInfo);
        birbSprite.sprite = birbInfo.image;
        targetID = 1;
    }

    internal virtual void Update()
    {
        if (active)
        {
            if (mouseHovering && Input.GetMouseButtonDown(1) && birbmenu == null && !StoreManager.instance.placingtower && !StoreManager.instance.placingtower2)
            {
                birbmenu = Instantiate(birbMenuPrefab, transform);
                birbmenu.GetComponentInChildren<BirbInfoScreen>().StartMenu(gameObject, this);
            }
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
        else if (movingBirb)
        {
            transform.position = new Vector3(CursorScript.instance.transform.position.x, CursorScript.instance.transform.position.y, transform.position.z);
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
            mBulletScript.target = Targeting();
        }
    }

    public Transform Targeting()
    {
        targetID = Mathf.Clamp(targetID, 1, 4);
        List<Transform> list = targets;
        if (targetID < 3)
        {

            list.Sort(SortEnemiesDistance);
            if (1 == targetID) //furthest from beginning
            {
                return list[0];
            }
            else //closest to beginning
            {
                return list[list.Count - 1];
            }
        }
        else
        {
            list.Sort(SortEnemiesHP);
            if (3 == targetID) //highest HP
            {
                return list[list.Count - 1];
            }
            else //lowest hp
            {
                return list[0];
            }
        }
        //return targets[0]; // Do you mean null? What's with the ! being after the thingamabob?? //shhhhhhhh its not real
    }

    private int SortEnemiesDistance(Transform enemy1, Transform enemy2)
    {
        NewEnemy enemy1Info = enemy1.gameObject.GetComponent<NewEnemy>();
        NewEnemy enemy2Info = enemy2.gameObject.GetComponent<NewEnemy>();
        return enemy1Info.distance.CompareTo(enemy2Info.distance);
    }
    private int SortEnemiesHP(Transform enemy1, Transform enemy2)
    {
        NewEnemy enemy1Info = enemy1.gameObject.GetComponent<NewEnemy>();
        NewEnemy enemy2Info = enemy2.gameObject.GetComponent<NewEnemy>();
        return enemy1Info.health.CompareTo(enemy2Info.health);
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
