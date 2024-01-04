using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using Unity.PlasticSCM.Editor.WebApi;
using Unity.VisualScripting;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    private List<Transform> waypoints = new List<Transform>();
    public bool active = true;
    public int currentWaypoint;
    public float speed;
    public float health;
    public bool debuttoggle;
    public Enemyinfo enemyinfo;

    [Header("sprite")]
    public LerpScript lerpScript = new LerpScript();
    public SpriteRenderer spriteRenderer;
    public Transform sprite;
    public Transform rotation1;
    public Transform rotation2;
    public float rotationSpeed;


    private void Start()
    {
        lerpScript.StartEndlessRotation(rotation1.rotation.eulerAngles, rotation2.rotation.eulerAngles, sprite, Manager.globalCurve, rotationSpeed);
        waypoints = Manager.globalWaypoints;
        sprite = transform.GetChild(0);

        spriteRenderer.sprite = enemyinfo.sprite;
        health = enemyinfo.health;
        speed = enemyinfo.speed;


    }

    private void Update()
    {
        if (debuttoggle)
        {
            lerpScript.active = false;
        }

        lerpScript.Update();
        if (active && waypoints.Count > 0)
        {
            transform.position = Vector3.MoveTowards(transform.position, waypoints[currentWaypoint].position, speed * Time.deltaTime);
        }
    }
}
