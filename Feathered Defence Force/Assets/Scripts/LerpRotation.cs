using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class LerpRotation
{

    public TimerScript timerScript = new TimerScript(0);
    public bool curved = true;
    public Vector3 point1, point2;
    public Transform objectToRotate;
    public AnimationCurve curve;
    public bool active;
    public float delay;


    public void Update()
    {
        timerScript.Update();
        if (active)
        {
            if (curved)
            {
                objectToRotate.rotation = Quaternion.Slerp
                    (
                        Quaternion.Euler(point1),
                        Quaternion.Euler(point2),
                        curve.Evaluate(timerScript.Progress())
                    );
            }

            if (timerScript.Check())
            {
                objectToRotate.rotation = Quaternion.Euler(point2);
                SwitchPoints();
                timerScript.Restart();
            }
        }
    }

    public void StartEndlessRotation(Vector3 point1, Vector3 point2, Transform objectToRotate, AnimationCurve curve, float delay)
    {
        active = true;
        this.point1 = point1;
        this.point2 = point2;
        this.objectToRotate = objectToRotate;
        this.curve = curve;
        this.delay = delay;
        this.objectToRotate.rotation = Quaternion.Euler(point1);
        timerScript.Start(delay);
    }

    void SwitchPoints()
    {
        Vector3 temp = this.point1;
        this.point1 = this.point2;
        this.point2 = temp;
    }

}
