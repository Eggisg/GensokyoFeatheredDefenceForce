using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

[CreateAssetMenu(fileName = "New Status", menuName = "Scriptable/Enemy"), Serializable]
public class EnemyInfo : ScriptableObject
{
    public bool boss;
    public float hp;
    public float speed;
    public float reward;
    
}
