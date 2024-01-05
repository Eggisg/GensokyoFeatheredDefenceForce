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

    [Header("sprite")]
    public LerpScript lerpScript = new LerpScript();
    public SpriteRenderer spriteRenderer;
    public Transform sprite;
    public Transform rotation1;
    public Transform rotation2;
    public float rotationSpeed;


    public List<Status> statuses;


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


    }

    private void Update()
    {
        lerpScript.Update();

        if (waypoints.Count == 0)
        {
            // hurt by enemyinfo.damage
            Destroy(gameObject);
        }
        else
        {
            if (active && transform.position == waypoints[0]!.position)
            {
                waypoints.RemoveAt(0);
            }

            if (active && waypoints.Count > 0)
            {
                transform.position = Vector3.MoveTowards(transform.position, waypoints[currentWaypoint].position, speed * Time.deltaTime);
            }
        }

        foreach (Status status in statuses)
        {
            status.Update();
        }
    }

    public void Damage(float mAmount, Status mNewStatus = null)
    {
        //make a little bounce effect
        //play a sound
        //enable/update healthbar

        enemyinfo.hp -= mAmount;
        if (enemyinfo.hp <= 0)
        {

        }

        if(mNewStatus != null)
        {
            statuses.Add(mNewStatus);
        }

    }

    public void Kill()
    {
        if (enemyinfo.boss)
        {
            Manager.PlayAudio(0);
        }

        //drop monye
    }
}
