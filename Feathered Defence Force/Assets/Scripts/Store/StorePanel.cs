using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class StorePanel : MonoBehaviour
{
    public GameObject birbPrefab;
    public BirbInfo birbInfo;
    public Infopanel infopanel;
    public TextMeshProUGUI nameText;
    public TextMeshProUGUI descText;
    public TextMeshProUGUI attackText;
    public TextMeshProUGUI speedText;
    public TextMeshProUGUI rangeText;
    public TextMeshProUGUI costText;



    void Start()
    {
        birbInfo = Instantiate(birbPrefab.GetComponent<GenericBirbTower>().birbInfo);
    }

    void Update()
    {
        
    }

    public void SendToInfoScreen()
    {

    }
}
