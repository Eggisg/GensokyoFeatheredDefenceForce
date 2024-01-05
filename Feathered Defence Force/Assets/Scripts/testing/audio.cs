using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class audio : MonoBehaviour
{
    public bool debug1, debug2, debug3;
    public AudioClip[] audios = new AudioClip[3];
    public AudioSource source;
    void Update()
    {
        if (debug1)
        {
            debug1 = false;
            Manager.PlayAudio(audios[0]);
        }
        if (debug2)
        {
            debug2 = false;
            Manager.PlayAudio(audios[1]);
        }
        if (debug3)
        {
            debug3 = false;
            Manager.PlayAudio(audios[2]);
        }
    }
}
