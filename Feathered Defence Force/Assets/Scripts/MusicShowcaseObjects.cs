using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class MusicShowcaseObjects : MonoBehaviour
{
    public Transform offScreenPoint;
    public Transform onScreenPoint;
    public Transform AttributionObject;
    public TextMeshProUGUI TextAttributionObject;

    private void Start()
    {
        AttributionObject.position = offScreenPoint.position;
    }
}
