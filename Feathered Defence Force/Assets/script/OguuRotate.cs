using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

[Serializable]
public class OguuRotate
{


    public float spinAmount;
    AnimationCurve curve;
    Transform transform;
    public TimerScript timer = new TimerScript(0);
    Vector3 startrotation;
    Vector3 desiredrotation;
    public AudioSource audioSource;

    public bool active;
    internal OguuRotate(Transform transform, AudioSource audiosource)
    {
        this.transform = transform;
        this.audioSource = audiosource;
    }

    public void Update()
    {
        if (active)
        {
            timer.Update();

            transform.rotation = Quaternion.Euler(Vector3.Lerp
                (
                    startrotation,
                    desiredrotation,
                    curve.Evaluate(timer.Progress())
                ));
            if (timer.Check())
            {
                active = false;
            }
        }

    }

    public void Charge(float spinAmount, AnimationCurve curve, float time)
    {
        this.spinAmount = spinAmount;
        this.curve = curve;
        desiredrotation = new Vector3
            (
                transform.rotation.eulerAngles.x, 
                transform.rotation.eulerAngles.y + spinAmount, 
                transform.rotation.eulerAngles.z
            );
        startrotation = transform.rotation.eulerAngles;
        timer.Start(time);
        audioSource.Play();
        active = true;
    }
}
