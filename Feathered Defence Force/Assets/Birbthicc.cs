using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Birbthicc : MonoBehaviour
{
    public GenericBirbTower birbtower;
    public GameObject birbthic;
    public static bool showCollider;
    public bool buh;

    private void Update()
    {
        buh = showCollider;
        birbthic.SetActive(showCollider);

        if (colliders.Count == 0)
        {
            birbtower.canPlace = true;
        }
        else
        {
            birbtower.canPlace = false;
        }
    }

    public List<Collider2D> colliders;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag(gameObject.tag))
        {
            colliders.Add(collision);
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {

        if (collision.gameObject.CompareTag(gameObject.tag))
        {
            colliders.Remove(collision);
        }
    }
}
