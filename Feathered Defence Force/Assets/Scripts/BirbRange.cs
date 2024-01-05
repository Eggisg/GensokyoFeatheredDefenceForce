using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BirbRange : MonoBehaviour
{
    public bool friendlyFire = false;
    GenericBirbTower birbtower;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!friendlyFire && collision.gameObject.CompareTag("Enemy"))
        {
            birbtower.targets.Add(collision.gameObject.GetComponent<NewEnemy>());
        }
        else if (friendlyFire && collision.gameObject.CompareTag("Tower"))
        {
            birbtower.targets.Add(collision.gameObject.GetComponent<GenericBirbTower>());
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        
    }
}
