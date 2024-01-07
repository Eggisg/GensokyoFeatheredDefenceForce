using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Infopanel : MonoBehaviour
{
    public static Infopanel instance;
    public GameObject everythingVisual;
    public GameObject prefab;
    public UnityEngine.UI.Image button;
    bool hasBirb;
    bool canBuy;
    BirbInfo birbInfo;
    StoreManager storeManager;

    public TextMeshProUGUI birbName, desc, speed, damage, cost, special;
    public UnityEngine.UI.Image image;

    private void Awake()
    {
        instance = this;
    }

    private void OnDisable()
    {
        hasBirb = false;
        birbInfo = null;
        everythingVisual.SetActive(false);
    }
    void Start()
    {
        storeManager = StoreManager.instance;
    }

    void Update()
    {
        
    }
    public void GiveInfo(BirbInfo newInfo, GameObject prefab)
    {
        this.prefab = prefab;
        birbInfo = newInfo;
        everythingVisual.SetActive(true);
        SetInfo();
    }

    void SetInfo()
    {
        birbName.text = birbInfo.birbname;
        desc.text = birbInfo.desc;
        speed.text = birbInfo.speed.ToString("0.00");
        damage.text = birbInfo.damage.ToString("0.00");
        cost.text = birbInfo.cost.ToString();
        image.sprite = birbInfo.image;

        if (Manager.instance.playerMoney >= birbInfo.cost)
        {
            button.color = Color.white;
            canBuy = true;
        }
        else
        {
            button.color = Color.red;
            canBuy=false;
        }
    }

    public void Buy()
    {
        if (canBuy)
        {
            storeManager.StartPlacingTower(prefab);

        }
    }
}   
