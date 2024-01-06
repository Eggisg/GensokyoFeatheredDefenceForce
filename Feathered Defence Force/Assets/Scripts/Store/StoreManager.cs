using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class StoreManager : MonoBehaviour
{
    public static bool storeShenanigans;
    public Transform cursor;
    public GenericBirbTower birb;
    public List<GameObject> birbPrefabs;
    public GameObject closedStore;
    public GameObject openedStore;
    public GameObject buyingBirb;
    public GameObject purchaseButton;
    public bool placingtower = false;

    public Vector2 prefabOffsets;
    public RectTransform firstSpot;
    public Vector2Int columnsAndRows;

    private void Start()
    {
        ////for (int i = 0; i < birbPrefabs.Count; i++)
        ////{
        ////    Debug.Log($"{i} {(i % columnsAndRows.x) * 5} {(i / columnsAndRows.y) * -5}");
        ////    Vector3 newPos = new Vector2((i % columnsAndRows.x) * prefabOffsets.x, (i / columnsAndRows.y) * prefabOffsets.y);
        ////    Instantiate(birbPrefabs[i], newPos, Quaternion.identity, openedStore.transform) ;
        /// iau haty mabaisc mawth
        ////}


        for (int i = 0; i < birbPrefabs.Count; i++)
        {
            int row = i / columnsAndRows.x;
            int col = i % columnsAndRows.x;

            Debug.Log($"{i} {col * prefabOffsets.x} {row * -prefabOffsets.y}");

            Vector3 newPos = new Vector3(col * prefabOffsets.x, row * -prefabOffsets.y, 0f);
            newPos += firstSpot.position;
            Instantiate(birbPrefabs[i], newPos, Quaternion.identity, openedStore.transform);
        }

    }

    public void Update()
    {
        if(placingtower)
        {
            birb!.transform.position = cursor.position;
            if (Input.GetMouseButton(1) && birb!.canPlace)
            {
                purchaseButton.SetActive(true);
                placingtower = false;
            }
        }
    }


    public void OpenStore()
    {
        storeShenanigans = true;
        closedStore.SetActive(false);
        openedStore.SetActive(true);
    }
    public void CloseStore()
    {
        storeShenanigans = false;
        closedStore.SetActive(true);
        openedStore.SetActive(false);
    }
    public void CancelPurchase()
    {
        purchaseButton.SetActive(false);
        storeShenanigans = false;
    }
    public void ChooseBirb(GenericBirbTower birb)
    {
        openedStore.SetActive(false);

    }
    public void ConfirmBuy()
    {
        storeShenanigans = false;
        birb.PlaceTower();
        birb = null;
        buyingBirb.SetActive(false);
        CloseStore();
    }







}
