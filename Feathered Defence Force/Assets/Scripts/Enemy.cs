using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] private EnemyInfo pEnemyInfo;
    public List<Status> statuses;
    void Start()
    {
        
    }

    void Update()
    {
        foreach (Status status in statuses)
        {
            status.Update();
        }
    }

    public void Damage(float amount)
    {
        //make a little bounce effect
        //play a sound
        //enable/update healthbar

        pEnemyInfo.hp -= amount;
        if (pEnemyInfo.hp <= 0)
        {

        }
    }

    public void Kill()
    {
        if (pEnemyInfo.boss)
        {
            Manager.PlayAudio(0);
        }
    }
    
}
