using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class CursorScript : MonoBehaviour
{
    public static CursorScript instance;
    public float desiredZ;
    public Transform offset;
    public Transform playerCamera;
    public SpriteRenderer spriteRenderer;
    public Color normal, select, na;
    // Start is called before the first frame update
    void Start()
    {
        instance = this;
        //Cursor.lockState = CursorLockMode.Confined;
       // Cursor.visible = false;
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 mLimit1 = playerCamera.position + offset.localPosition;
        Vector2 mLimit2 = playerCamera.position - offset.localPosition;

        transform.position = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 10));
        Vector2 mLimit = VectorFuncs.ClampVector(transform.position, mLimit1, mLimit2);
        transform.position = new Vector3(mLimit.x, mLimit.y, desiredZ);
    }

    public static void Normal()
    {
        instance.spriteRenderer.color = instance.normal;
    }
    public static void Select()
    {
        instance.spriteRenderer.color = instance.select;
    }
    public static void NA()
    {
        instance.spriteRenderer.color = instance.na;
    }
    

}
