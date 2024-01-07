using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class StorePanel : MonoBehaviour
{

    public GameObject birbPrefab;
    public BirbInfo birbInfo;
    public Image spriteimage;
    public TextMeshProUGUI nameText;
    public TextMeshProUGUI costText;

    private void Awake()
    {
        
    }

    void Start()
    {

        birbInfo = Instantiate(birbPrefab.GetComponent<GenericBirbTower>().birbInfo);
        birbInfo.Initialize();
        nameText.text = birbInfo.name;
        costText.text = birbInfo.cost.ToString();
        spriteimage.sprite = birbInfo.image;
    }

    public void SendToInfoScreen()
    {
        Infopanel.instance.gameObject.SetActive(true);
        Infopanel.instance.GiveInfo(birbInfo, birbPrefab);
    }
}
