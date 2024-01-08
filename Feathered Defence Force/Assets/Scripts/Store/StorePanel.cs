using System.Collections;
using System.Collections.Generic;
using System.Reflection.Emit;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class StorePanel : MonoBehaviour
{

    [HideInInspector] public GameObject birbPrefab;
    [HideInInspector] public GenericBirbTower birbInfoObject;
    private BirbInfo birbInfo;

    public Image spriteimage;
    public TextMeshProUGUI nameText;
    public TextMeshProUGUI costText;


    //void Start()
    //{
    //    birbInfo = birbPrefab.GetComponent<GenericBirbTower>().birbInfo;
    //    birbInfo = Instantiate(birbPrefab.GetComponent<GenericBirbTower>().birbInfo);
    //    birbInfo.Initialize();
    //    nameText.text = birbInfo.birbname;
    //    costText.text = birbInfo.cost.ToString();
    //    spriteimage.sprite = birbInfo.image;
    //}

    public void InstantiateInformation()
    {
        birbInfo = birbPrefab.GetComponent<GenericBirbTower>().birbInfo;
        birbInfo = Instantiate(birbInfo);
        birbInfo.Initialize();
        nameText.text = birbInfo.birbname;
        costText.text = birbInfo.cost.ToString();
        spriteimage.sprite = birbInfo.image;
    }

    public void SendToInfoScreen()
    {
        Infopanel.instance.gameObject.SetActive(true);
        Infopanel.instance.GiveInfo(birbInfo, birbPrefab);
    }
}
