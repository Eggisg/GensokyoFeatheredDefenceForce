using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenericBirbTower : MonoBehaviour
{
    public bool active = false;
    public bool audioOn;
    private TimerScript pTimerScript;

    internal virtual void Update()
    {
        if (active)
        {
            pTimerScript.Update();

            if (pTimerScript.Check())
            {

            }
        }
    }

    public void ToggleAudio()
    {

    }

    private void PlayAudio(int mID)
    {
        if (audioOn)
        {
            Manager.PlayAudio(mID);
        }
    }

}
