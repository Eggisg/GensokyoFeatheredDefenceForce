using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class StoreManager : MonoBehaviour
{
    public bool doStore;
    public bool storeDoingStuff;
    public bool placingtower = false;
    public bool placingtower2 = false;
    public GameObject placingText;
    public float panelscale;


    public static StoreManager instance;


    public Transform cursor;
    //public List<GameObject> birbPanelPrefabs;
    public GameObject basePanel;
    public GameObject closedStore;
    public GameObject openedStore;
    public GameObject buyingBirb;
    public GameObject purchaseButton;

    GameObject newBirb;

    public Vector2 prefabOffsets;
    public RectTransform firstSpot;
    public Vector2Int columnsAndRows;
    public KeyCode placeKey;
    public int cost;



    private void Awake()
    {
        instance = this;
    }
    private void Start()
    {
        //for (int i = 0; i < birbPrefabs.Count; i++)
        //{
        //	Debug.Log($"{i} {(i % columnsAndRows.x) * 5} {(i / columnsAndRows.y) * -5}");
        //	Vector3 newPos = new Vector2((i % columnsAndRows.x) * prefabOffsets.x, (i / columnsAndRows.y) * prefabOffsets.y);
        //	Instantiate(birbPrefabs[i], newPos, Quaternion.identity, openedStore.transform);
        //}

        //for (int i = 0; i < birbPanelPrefabs.Count; i++)
        //{
        //    int row = i / columnsAndRows.x;
        //    int col = i % columnsAndRows.x;

        //    Vector3 newPos = new Vector3(col * prefabOffsets.x, row * -prefabOffsets.y, 0f);
        //    newPos += firstSpot.position;
        //    GameObject panel = Instantiate(birbPanelPrefabs[i], newPos, Quaternion.identity, openedStore.transform);
        //    panel.transform.localScale = new Vector3(panelscale, panelscale, panelscale);


        //}
    }


    public void CreatePanels(List<GameObject> birbPrefabList)
    {
        birbPrefabList.Sort(SortByCost);

        //part 1 of fixing resolution with shop
        float screenWidth = Screen.width;
        float screenHeight = Screen.height;

        for (int i = 0; i < birbPrefabList.Count; i++)
        {
            int row = i / columnsAndRows.x;
            int col = i % columnsAndRows.x;

            //part 2 of fixing resolution with shop, calculates position blah blah 
            Vector3 newPos = new Vector3(col * prefabOffsets.x * screenWidth / 1920f, row * -prefabOffsets.y * screenHeight / 1080f, 0f);
            newPos += firstSpot.position;

            GameObject panelObject = Instantiate(basePanel, newPos, Quaternion.identity, openedStore.transform);
            panelObject.transform.localScale = new Vector3(panelscale, panelscale, panelscale);
            StorePanel panelScript = panelObject.GetComponent<StorePanel>();
            panelScript.birbPrefab = birbPrefabList[i];

            panelScript.InstantiateInformation();
        }
    }

    public void Update()
    {

        if (placingtower)
        {
            GenericBirbTower birbtowerScript = newBirb.GetComponent<GenericBirbTower>();
            if (birbtowerScript.canPlace)
            {
                birbtowerScript.birbSprite.color = Color.green;
            }
            else
            {
                birbtowerScript.birbSprite.color = Color.red;
            }

            newBirb.transform.position = cursor.position;
            if ((Input.GetKey(placeKey) || Input.GetMouseButtonDown(0)) && newBirb.GetComponent<GenericBirbTower>().canPlace)
            {
                purchaseButton.SetActive(true);
                buyingBirb.SetActive(true);
                birbtowerScript.birbSprite.color = Color.white;
                placingtower = false;
                placingtower2 = true;
            }
        }
        else if (!placingtower2)
        {
            switch (doStore)
            {
                case true: // If we should open the store
                    OpenStore();
                    break;

                case false: // And if we should close the store
                    CloseStore();
                    break;
            }
        }
        else
        {
            placingText.SetActive(true);
            placingText.GetComponent<TextMeshProUGUI>().text = "press e change placement";
            if (Input.GetKey(KeyCode.Q))
            {
                openedStore.SetActive(false);
                closedStore.SetActive(false);
                buyingBirb.SetActive(false);
                placingtower = true;
                placingtower2 = false;
                placingText.SetActive(true);
                placingText.GetComponent<TextMeshProUGUI>().text = "press e confirm placement";
            }
        }
    }

    public void OpenStore()
    {
        storeDoingStuff = true;
        closedStore.SetActive(false);
        openedStore.SetActive(true);
        Debug.Log("Store is open!");
    }

    public void CloseStore()
    {
        closedStore.SetActive(true);
        openedStore.SetActive(false);
        //Debug.Log("Store is closed?!? How am I to buy my Touhou themed blowjob now..."); //you will never get it now
    }

    public void StartPlacingTower(GameObject prefab, int cost)
    {
        this.cost = cost;
        openedStore.SetActive(false);
        closedStore.SetActive(false);
        buyingBirb.SetActive(false);


        placingtower = true;

        placingText.SetActive(true);
        placingText.GetComponent<TextMeshProUGUI>().text = "press e confirm placement";
        Birbthicc.showCollider = true;

        newBirb = Instantiate(prefab, CursorScript.instance.transform.position, Quaternion.identity);
        newBirb.GetComponent<GenericBirbTower>().active = false;
        newBirb.GetComponentInChildren<BirbRange>().showCollider = true;
    }
    public void StartPlacingTower()
    {
        openedStore.SetActive(false);
        closedStore.SetActive(false);
        buyingBirb.SetActive(false);


        placingtower = true;

        placingText.SetActive(true);
        placingText.GetComponent<TextMeshProUGUI>().text = "press e confirm placement";
    }
    public void ConfirmPlacement()
    {
        buyingBirb.SetActive(true);
        placingtower = false;
        placingtower2 = true;
        //press E to change placement

    }

    public void CancelPurchase()
    {
        purchaseButton.SetActive(false);
        Destroy(newBirb);
        placingtower2 = false;
        doStore = false;
        buyingBirb.SetActive(false);
        placingText.SetActive(false);
    }

    public void ConfirmBuy()
    {
        storeDoingStuff = false;
        doStore = false;
        newBirb.GetComponent<GenericBirbTower>().PlaceTower();
        Birbthicc.showCollider = false;

        Manager.AddMonye(-cost);
        newBirb.GetComponent<GenericBirbTower>().active = true;
        newBirb = null;

        placingtower = false;
        placingtower2 = false;
        buyingBirb.SetActive(false);
        placingText.SetActive(false);

    }
    public int SortByCost(GameObject birb1, GameObject birb2)
    {
        BirbInfo birb1info = birb1.GetComponent<GenericBirbTower>().birbInfo;
        BirbInfo birb2info = birb2.GetComponent<GenericBirbTower>().birbInfo;
        birb1info = Instantiate(birb1info);
        birb1info.Initialize();
        birb2info = Instantiate(birb2info);
        birb2info.Initialize();

        return birb1info.cost.CompareTo(birb2info.cost);
    }

    /// <summary>
    /// Lets a button call the method to switch doStore, depending on the input.
    /// </summary>
    /// <param name="input"></param>
    public void ChangeDoStore(bool input)
    {
        switch (input)
        {
            case true:
                doStore = true;
                break;

            case false:
                doStore = false;
                break;
        }
    }
}
