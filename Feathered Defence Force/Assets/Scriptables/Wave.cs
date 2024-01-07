using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

[CreateAssetMenu(fileName = "New Birb", menuName = "Scriptable/wave"), Serializable]
public class Wave : ScriptableObject
{
    public int enemies;
    public int bosses;

}
