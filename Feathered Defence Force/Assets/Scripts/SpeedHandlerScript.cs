using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeedHandlerScript : MonoBehaviour
{
    public static SpeedHandlerScript instance;
    [SerializeField] private KeyCode speedUp;
    [SerializeField] private KeyCode speedDown;
    [SerializeField] private float elapsedTime;
    [SerializeField] private float delay;
    [SerializeField] private int maxSpeed;

    //create deligate
    public delegate void OnSpeedUpdate();
    //create instance of deligate
    public OnSpeedUpdate onSpeedUpdate;
    
    private void Awake()
    {
        instance = this;
    }

    void Update()
    {
        if (!Manager.instance.dead)
        {
            if (Time.timeScale != 0) //evil divide by 0 remover
            {
                elapsedTime += Time.deltaTime / Time.timeScale;
            }

            if (Input.GetKeyDown(speedUp))
            {
                elapsedTime = 0;
                AddSpeed();
            }
            if (Input.GetKey(speedUp))
            {

                if (elapsedTime >= delay)
                {
                    elapsedTime -= 0.08f;
                    AddSpeed();
                }
            }

            if (Input.GetKeyDown(speedDown))
            {
                elapsedTime = 0;
                RemoveSpeed();
            }
            if (Input.GetKey(speedDown))
            {
                if (elapsedTime >= delay)
                {
                    elapsedTime -= 0.08f;
                    RemoveSpeed();
                }
            }
        }
        else
        {
            Time.timeScale = 1;
        }

    }
    private void AddSpeed()
    {

        if (Time.timeScale < maxSpeed)
        {
            Time.timeScale += 1;
            onSpeedUpdate?.Invoke();
        }
    }
    private void RemoveSpeed()
    {
        if(Time.timeScale > 0) 
        {
            Time.timeScale -= 1;
            onSpeedUpdate?.Invoke();
        }
    }

    public static void AddFunction(OnSpeedUpdate method)
    {
        instance.onSpeedUpdate += method;
    }
}
