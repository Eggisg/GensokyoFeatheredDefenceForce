using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Deligate : MonoBehaviour
{
    //Define the deligate type
    public delegate void CoolDeligate();

    //Create an instance of CoolDeligate
    private CoolDeligate coolDeligate;

    void Start()
    {
        AddToDeligate(Method1);

        //like coolDeligate(); but avoids NullRefrenceException
        coolDeligate?.Invoke();
    }

    public void AddToDeligate(CoolDeligate methodToAdd)
    {
        coolDeligate = methodToAdd;
    }


    public void Method1()
    {
        Debug.Log("Buh");
    }
}
