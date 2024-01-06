using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class StoreManager : MonoBehaviour
{
	public bool doStore;
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
		//for (int i = 0; i < birbPrefabs.Count; i++)
		//{
		//	Debug.Log($"{i} {(i % columnsAndRows.x) * 5} {(i / columnsAndRows.y) * -5}");
		//	Vector3 newPos = new Vector2((i % columnsAndRows.x) * prefabOffsets.x, (i / columnsAndRows.y) * prefabOffsets.y);
		//	Instantiate(birbPrefabs[i], newPos, Quaternion.identity, openedStore.transform);
		//}

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
		if (placingtower)
		{
			birb!.transform.position = cursor.position;
			if (Input.GetMouseButton(1) && !birb.canPlace)
			{
				purchaseButton.SetActive(true);
				placingtower = false;
			}
		}

		bool prevStoreStatus = false;

		if (doStore == prevStoreStatus)
		{
			switch (doStore)
			{
				case true: // If we should open the store
					OpenStore();
					prevStoreStatus = true;
					return;

				case false: // And if we should close the store
					CloseStore();
					prevStoreStatus = false;
					return;
			}
		}
	}

	public void OpenStore()
	{
		closedStore.SetActive(false);
		openedStore.SetActive(true);
		Debug.Log("Store is open!");
	}

	public void CloseStore()
	{
		closedStore.SetActive(true);
		openedStore.SetActive(false);
		Debug.Log("Store is closed?!? How am I to buy my Touhou themed blowjob now...");
	}

	public void CancelPurchase()
	{
		purchaseButton.SetActive(false);
		doStore = false;
	}

	public void ChooseBirb(GenericBirbTower birb)
	{
		openedStore.SetActive(false);
	}

	public void ConfirmBuy()
	{
		doStore = false;
		birb.PlaceTower();
		birb = null;
		buyingBirb.SetActive(false);
		CloseStore();
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
