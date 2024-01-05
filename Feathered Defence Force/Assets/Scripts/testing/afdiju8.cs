using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class afdiju8 : MonoBehaviour
{
    void Update()
    {
        int random = Random.Range(0, 2);
        if (random == 0)
        {
            Debug.LogError("fuck");
        }
        else
        {
            Debug.LogError("just learn to code");
        }
        
    }
}
