using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tower : MonoBehaviour
{
    [Header("sprite")]
    public Transform sprite;
    public float rotationAmount;
    public AudioSource audiosource;
    public AnimationCurve curve;
    public OguuRotate oguuRotate;

    public bool debug;
    void Start()
    {
        oguuRotate = new OguuRotate(sprite, audiosource);
    }

    void Update()
    {
        oguuRotate.Update();

        if (debug)
        {
            debug = false;
            oguuRotate.Charge(rotationAmount, curve, audiosource.clip.length);
        }
    }
}
