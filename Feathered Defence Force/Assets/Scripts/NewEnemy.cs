using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using Unity.PlasticSCM.Editor.WebApi;
using Unity.VisualScripting;
using UnityEngine;

public class NewEnemy : CommonInheritor
{
    [Header("Enemy Related")]
    private List<Transform> waypoints = new List<Transform>();
    public bool active = true;
    public int currentWaypoint;
    public float speed;
    public float health;
    public EnemyInfo enemyinfo;
    public float distance;


    [Header("Fire Related")]
    public float fireMultiplier = 1f;
    public bool onFire;
    public LerpVector3 fireScaleLerper;
    public Transform fireVisual;
    public Transform fireScale1;
    public Transform fireScale2;



    [Header("Sprite Related")]
    public float rotationSpeed;
    public LerpRotation lerpScript = new LerpRotation();
    public SpriteRenderer spriteRenderer;
    public Transform sprite;
    public Transform rotation1;
    public Transform rotation2;

    [Header("Status Related/misc")]
    public bool reversed;
    public List<EnemyStatus> statuses;

    public Color shellOil;
    public bool oiled;
    public TimerScript oilTimer;
    public LerpColour colorLerp;

    [Header("OnHurt Related")]
    public float colorHurtTime;
    public Color hurt;
    public Color Normal;
    private TimerScript pColorTimer;


    [Header("Debug Related")]
    public bool debugAddFire;
    public FireStatus debugFire;

    public bool debugAddShellOil;
    public OiledStatus debugOil;

    private void Start()
    {
        lerpScript.StartEndlessRotation(rotation1.rotation.eulerAngles, rotation2.rotation.eulerAngles, sprite, Manager.instance.curve, rotationSpeed);
        waypoints = Manager.instance.waypoints;
        sprite = transform.GetChild(0);

        spriteRenderer.sprite = enemyinfo.sprite;
        if (enemyinfo.boss)
        {
            spriteRenderer.transform.localScale = new Vector3(1f, 1f, 1f);
        }
        else
        {
            spriteRenderer.transform.localScale = new Vector3(0.8f, 0.8f, 0.8f);
        }
        health = enemyinfo.hp;
        speed = enemyinfo.speed;

        transform.position = waypoints[0].position;
        gameObject.tag = "Enemy";

        pColorTimer = new TimerScript(colorHurtTime);
        oilTimer = new TimerScript(1);
        colorLerp = new LerpColour();
        fireScaleLerper = new LerpVector3();
        fireScaleLerper.StartEndlessLerping(fireScale1.localScale, fireScale2.localScale, fireVisual, Manager.instance.curve, 1);
    }

    private void Update()
    {
        if (debugAddFire)
        {
            debugAddFire = false;
            Damage(0, debugFire);
        }
        if (debugAddShellOil)
        {
            debugAddShellOil = false;
            Damage(0, debugOil);
        }

        lerpScript.Update();
        pColorTimer.Update();
        fireScaleLerper.Update();
        colorLerp.Update();

        if (!oiled)
        {
            sprite.GetComponent<SpriteRenderer>().color = Color.Lerp(hurt, Normal, Manager.instance.linearCurve.Evaluate(pColorTimer.Progress()));
        }
        else
        {
            sprite.GetComponent<SpriteRenderer>().color = Color.Lerp(hurt, shellOil, Manager.instance.linearCurve.Evaluate(pColorTimer.Progress()));
        }
        

        if (currentWaypoint >= waypoints.Count)
        {
            ReachedEnd();
        }
        else
        {
            if (active && waypoints.Count > 0)
            {
                transform.position = Vector3.MoveTowards(transform.position, waypoints[currentWaypoint].position, speed * WaveManager.speedboost * Time.deltaTime);
                distance = Vector3.Distance(transform.position, waypoints[currentWaypoint].position) + currentWaypoint * -100;
            }
            if (active && transform.position == waypoints[currentWaypoint]!.position)
            {
                currentWaypoint++;
            }
        }

        CalculateFireMultiplier();
        fireVisual.gameObject.SetActive(CheckIfOnFire());


        for (int i = 0; i < statuses.Count; i++)
        {
            statuses[i].Update();
        }

    }

    public void Damage(float mAmount, EnemyStatus mNewStatus = null)
    {

        health -= mAmount;
        if (health <= 0)
        {
            Kill();
        }
        else if (mAmount > 0)
        {
            Manager.PlayAudio(14, 0.5f);
            pColorTimer.Start(colorHurtTime);
        }

        if (mNewStatus != null)
        {
            EnemyStatus newStatus = Instantiate(mNewStatus);
            newStatus.enemy = this;
            newStatus.Instantiate();
            statuses.Add(newStatus);
        }

    }

    public void Kill()
    {
        Manager.PlayParticle(0, transform.position);
        if (enemyinfo.boss)
        {
            Manager.PlayAudio(7, 1);
        }
        else
        {
            Manager.PlayAudio(11, 0.7f);
        }
        Manager.AddMonye((int)enemyinfo.minReward);

        //drop monye
        Destroy(gameObject);
    }


    private float CalculateFireMultiplier()
    {
        fireMultiplier = 1f;
        oiled = false;

        foreach (EnemyStatus status in statuses)
        {
            OiledStatus oiledStatus = status as OiledStatus;

            if (oiledStatus != null)
            {
                fireMultiplier *= 1.4f;
                oiled = true;
            }
        }

        return fireMultiplier;
    }
    private bool CheckIfOnFire(bool enableVisual = false)
    {
        onFire = false;
        foreach (EnemyStatus status in statuses)
        {
            FireStatus firestatus = status as FireStatus;

            if (firestatus != null)
            {
                onFire = true;
            }
        }

        return onFire;
    }

    private void ReachedEnd()
    {
        Manager.PlayAudio(12);
        if (enemyinfo.boss)
        {
            Manager.RemoveHealth(5);
        }
        else
        {
            Manager.RemoveHealth(5);
        }
        
        Destroy(gameObject);
    }

}
