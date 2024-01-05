using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Birb", menuName = "Scriptable/Music"), Serializable]
public class MusicScriptable : ScriptableObject
{
    public string musicName;
    public AudioClip musicClip;
}
