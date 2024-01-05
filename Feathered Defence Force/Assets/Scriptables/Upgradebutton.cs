using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UIElements;

public class Upgradebutton : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public bool pHovering;


    public void OnPointerEnter(PointerEventData eventData)
    {
        pHovering = true;
        ShowNewStats(true);
    }
    public void OnPointerExit(PointerEventData eventData)
    {
        pHovering = false;
        ShowNewStats(false);
    }

    public void ShowNewStats(bool includeUpgrade)
    {
        
    }
}
