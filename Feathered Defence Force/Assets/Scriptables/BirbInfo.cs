using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Birb", menuName = "Scriptable/GenericBirb"), Serializable]
public class BirbInfo : ScriptableObject
{
    public int currentTier = 0;
    public BirbTierInfo[] tiers = new BirbTierInfo[4];

    public string birbname;
    public string desc;

    public float speed;
    public float damage;
    public int cost;
    public float specialFloat;
    public float aoeSize;

    public EnemyStatus status;
    public GameObject throwablePrefab;

    public void Initialize()
    {
        StatUpdate(0);
    }

    public void Destroy()
    {
        //play sound and destroy object
    }
    public void Upgrade()
    {
        //if enough money and not max tier
        currentTier += 1;
        //switch (currentTier) // ??? Why the switch
        //{
        //    case 1:
        //        StatUpdate(1);
        //    break;

        //    case 2:
        //        StatUpdate(2);
        //    break;

        //    case 3:
        //        StatUpdate(3);
        //    break;

        //    default:
        //        StatUpdate(0);
        //    break;

        //}

        StatUpdate(currentTier);
    }

    public void StatUpdate(int tier)
    {
        speed = tiers[tier].speed;
        damage = tiers[tier].damage;
        cost = tiers[tier].cost;
        specialFloat = tiers[tier].specialFloat;
        aoeSize = tiers[tier].aoeSize;
    }


}
