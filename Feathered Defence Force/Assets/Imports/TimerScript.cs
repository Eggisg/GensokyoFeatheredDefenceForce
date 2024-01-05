using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class TimerScript
{
    [SerializeField] private float timer = 0;
    [SerializeField] private float duration;
    [SerializeField] public bool enabled = true;

    internal TimerScript(float duration)
    {
        this.duration = duration;
    }
    #region getset
    public float elapsedTime
    {
        get
        {
            return timer;
        }
    }
    public float Duration
    {
        get
        {
            return duration;
        }
    }
    #endregion

    public bool Check()
    {

        if (timer >= duration)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
    public void Restart()
    {
        timer = 0;
    }
    public void Start(float duration)
    {
        timer = 0;
        this.duration = duration;
    }
    public void Update()
    {
        if (enabled)
        {
            timer += Time.deltaTime;
        }

    }
    public float Progress()
    {
        return (timer / duration);
    }
}
