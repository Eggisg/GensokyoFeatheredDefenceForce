using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BirbInfo : ScriptableObject
{
    public int currentTier = 1;
    public BirbTierInfo[] tiers = new BirbTierInfo[3];

    public string birbname;
    public string desc;

    public float speed;
    public float damage;
    public int cost;
    public float specialFloat;
    public float aoeSize;


    public void Destroy()
    {
        //play sound and destroy object
    }
    public void Upgrade()
    {
        //if enough money and not max tier
        currentTier += 1;
        switch (currentTier)
        {


            case 1:
                

                break;

            case 2:


                break;

            case 3:

                break;

            default:
                break;

        }
    }


}
