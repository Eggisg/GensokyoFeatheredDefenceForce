using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.UI;

public class BirbInfoScreen : MonoBehaviour
{
    [HideInInspector] public GameObject birb;
    [HideInInspector] public GenericBirbTower birbScript;
    public Image birbImage;
    public Image[] buttons = new Image[4];

    public void StartMenu(GameObject birb, GenericBirbTower birbScript)
    {
        this.birb = birb;
        this.birbScript = birbScript;
        birbScript.birbSprite.color = Color.magenta;
        UpdateInformation();
    }
    public void UpdateInformation()
    {
        for (int i = 0; i < buttons.Length; i++)
        {
            if (i != birbScript.targetID-1)
            {
                buttons[i].color = Color.white;
            }
            else
            {
                buttons[i].color = Color.gray;
            }
        }
        birbImage.sprite = birbScript.birbInfo.image;
    }

    public void ChangeAttackMode(int mode)
    {
        birbScript.targetID = mode;
        UpdateInformation();
    }
    public void Remove()
    {
        Manager.AddMonye((int)(birbScript.birbInfo.cost * 0.5f));
        Destroy(birb);
    }
    public void Close()
    {
        birbScript.birbSprite.color = Color.white;
        Destroy(transform.parent.gameObject);
    }
}
