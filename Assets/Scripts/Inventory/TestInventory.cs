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
			ItemData itemData = ItemParser.GetItemByCode(1);

			inventoryManager.AddItem(itemData);
		}

		if (Input.GetKeyDown(KeyCode.M))
		{
			ItemData itemData = ItemParser.GetItemByCode(2);

			inventoryManager.AddItem(itemData);
		}
	}
}
