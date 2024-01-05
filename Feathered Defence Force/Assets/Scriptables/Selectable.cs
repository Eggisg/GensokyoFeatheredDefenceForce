using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Selectable : MonoBehaviour
{
    public bool selectable = true;
    protected bool MouseHovering {  get; private set; }
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("cursor"))
        {
            if (!selectable)
            {
                CursorScript.NA();
            }
            else
            {
                CursorScript.Select();
                MouseHovering = true;
            }
            
        }
        else
        {
            MouseHovering = false;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        CursorScript.Normal();
    }


}
