using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New BirbTier", menuName = "Scriptable/BirbTierInfo"), Serializable]
public class BirbTierInfo : ScriptableObject
{
    public float speed;
    public float damage;
    public int cost;
    public float specialFloat;
    public float aoeSize;
}
