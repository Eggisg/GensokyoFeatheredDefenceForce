using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using Unity.PlasticSCM.Editor.WebApi;
using Unity.VisualScripting;
using UnityEngine;

public class NewEnemy : CommonInheritor
{
    private List<Transform> waypoints = new List<Transform>();
    public bool active = true;
    public int currentWaypoint;
    public float speed;
    public float health;
    public bool debuttoggle;
    public EnemyInfo enemyinfo;
    public float fireMultiplier;
    public bool onFire;

    [Header("sprite")]
    public LerpScript lerpScript = new LerpScript();
    public SpriteRenderer spriteRenderer;
    public Transform sprite;
    public Transform rotation1;
    public Transform rotation2;
    public float rotationSpeed;

    public bool reversed;
    public List<EnemyStatus> statuses;

    public Color hurt;
    public Color Normal;
    private TimerScript pColorTimer;
    public float colorHurtTime;

    private void Start()
    {
        lerpScript.StartEndlessRotation(rotation1.rotation.eulerAngles, rotation2.rotation.eulerAngles, sprite, Manager.manager.curve, rotationSpeed);
        waypoints = Manager.manager.waypoints;
        sprite = transform.GetChild(0);

        spriteRenderer.sprite = enemyinfo.sprite;
        health = enemyinfo.hp;
        speed = enemyinfo.speed;

        transform.position = waypoints[0].position;
        gameObject.tag = "Enemy";

        pColorTimer = new TimerScript(colorHurtTime);


    }

    private void Update()
    {
        lerpScript.Update();
        pColorTimer.Update();

        sprite.GetComponent<SpriteRenderer>().color = Color.Lerp(hurt, Normal, Manager.manager.linearCurve.Evaluate(pColorTimer.Progress()));

        if (currentWaypoint >= waypoints.Count)
        {
            // hurt by enemyinfo.damage
            Destroy(gameObject);
        }
        else
        {


            if (active && waypoints.Count > 0)
            {
                transform.position = Vector3.MoveTowards(transform.position, waypoints[currentWaypoint].position, speed * Time.deltaTime);
            }
            if (active && transform.position == waypoints[currentWaypoint]!.position)
            {
                currentWaypoint++;
            }
        }

        CalculateFireMultiplier();
        CheckIfOnFire();
        foreach (EnemyStatus status in statuses)
        {
            status.Update();
        }
    }

    public void Damage(float mAmount, EnemyStatus mNewStatus = null)
    {
        //play a sound
        
        //enable/update healthbar

        health -= mAmount;
        if (health <= 0)
        {
            Kill();
        }
        else
        {
            Manager.PlayAudio(14, 0.5f);
            pColorTimer.Start(colorHurtTime);
        }

        if(mNewStatus != null)
        {
            statuses.Add(mNewStatus);
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

        //drop monye
        Destroy(gameObject);
    }


    private float CalculateFireMultiplier()
    {
        fireMultiplier = 1f;
        foreach (OiledStatus oiled in statuses)
        {
            fireMultiplier *= 1.4f;
        }
        return fireMultiplier;
    }
    private bool CheckIfOnFire(bool enableVisual = false)
    {
        onFire = false;
        foreach (FireStatus fire in statuses)
        {
            onFire = true;
        }
        return onFire;
    }
}
