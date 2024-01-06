using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThrownObject : MonoBehaviour
{
    public Transform target;
    public float speed = 5;
    public float rotationSpeed = 0.3f;
    TimerScript timer;
    public GenericBirbTower OriginTower;

    private void Start()
    {
        timer = new TimerScript(rotationSpeed);
        transform.rotation = Quaternion.identity;
    }
    void Update()
    {
        timer.Update();
        speed += 1f * Time.deltaTime;
        if (target != null)
        {
            transform.position = Vector3.MoveTowards(transform.position, target.position, speed * Time.deltaTime);
        }
        else
        {
            if(OriginTower.targets.Count > 0)
            {
                target = OriginTower.Targeting(OriginTower.targetID);
            }
            else
            {
                Destroy(gameObject);
            }
        }
        

        if (timer.Check())
        {
            timer.Restart();
            transform.rotation = Quaternion.identity;
        }

        transform.rotation = Quaternion.Euler(Vector3.Lerp
            (
                Quaternion.identity.eulerAngles, 
                new Vector3(0, 0, 360),
                Manager.instance.linearCurve.Evaluate(timer.Progress())
            ));

        if (target != null && transform.position == target.position)
        {
            OriginTower.Hit(target.gameObject);
            Destroy(gameObject);
        }



    }
}
