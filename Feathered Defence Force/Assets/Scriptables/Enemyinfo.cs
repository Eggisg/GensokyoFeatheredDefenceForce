using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New EnemyInfo", menuName = "Scriptable/EnemyInfo")]
public class Enemyinfo : ScriptableObject
{
    public Sprite sprite;
    public float speed;
    public float health;
}
