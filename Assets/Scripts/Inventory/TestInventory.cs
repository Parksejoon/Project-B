using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestInventory : MonoBehaviour
{
	[SerializeField]
	private InventoryManager inventoryManager;      // 인벤토리 매니저

	public GameObject prefab1;
	public GameObject prefab2;
	public GameObject prefab3;
	public GameObject prefab4;

	public Texture texture1;
	public Texture texture2;
	public Texture texture3;
	public Texture texture4;


	// ($$ 프레임 $$)
	private void Update()
	{
		if (Input.GetKeyDown(KeyCode.N))
		{
			ItemData itemData;

			itemData.code = 1;
			itemData.name = "1";
			itemData.texture = texture1;
			itemData.prefab = prefab1;
			itemData.itemType = ItemType.Weapon;

			inventoryManager.AddItem(itemData);
		}

		if (Input.GetKeyDown(KeyCode.M))
		{
			ItemData itemData;

			itemData.code = 2;
			itemData.name = "2";
			itemData.texture = texture2;
			itemData.prefab = prefab2;
			itemData.itemType = ItemType.Weapon;

			inventoryManager.AddItem(itemData);
		}

		if (Input.GetKeyDown(KeyCode.J))
		{
			ItemData itemData;

			itemData.code = 3;
			itemData.name = "3";
			itemData.texture = texture3;
			itemData.prefab = prefab3;
			itemData.itemType = ItemType.Weapon;

			inventoryManager.AddItem(itemData);
		}

		if (Input.GetKeyDown(KeyCode.K))
		{
			ItemData itemData;

			itemData.code = 4;
			itemData.name = "4";
			itemData.texture = texture4;
			itemData.prefab = prefab4;
			itemData.itemType = ItemType.Weapon;

			inventoryManager.AddItem(itemData);
		}
	}
}
