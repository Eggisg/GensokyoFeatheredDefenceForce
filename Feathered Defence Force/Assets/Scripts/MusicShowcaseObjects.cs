using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class MusicShowcaseObjects : MonoBehaviour
{
    public RectTransform offScreenPoint;
    public RectTransform onScreenPoint;
    public RectTransform AttributionObject;
    public TextMeshProUGUI TextAttributionObject;

    private void Start()
    {
        AttributionObject.position = offScreenPoint.position;
    }
    public void DebugButton()
    {
        Manager.PlayMusic(3, 1);
    }
}
