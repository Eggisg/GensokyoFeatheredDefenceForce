using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BirbRange : MonoBehaviour
{
    
    public GenericBirbTower birbtower;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("collission");
        if (!birbtower.friendlyFire && collision.gameObject.CompareTag("Enemy"))
        {
            Debug.Log("sent enemy data");
            birbtower.targets.Add(collision.transform);
        }
        else if (birbtower.friendlyFire && collision.gameObject.CompareTag("Tower"))
        {
            birbtower.targets.Add(collision.transform);
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        birbtower.targets.Remove(collision.transform);
    }
}
