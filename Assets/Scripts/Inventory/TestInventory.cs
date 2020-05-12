using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestInventory : MonoBehaviour
{
	[SerializeField]
	private InventoryManager inventoryManager;      // 인벤토리 매니저

	public Texture texture1;
	public Texture texture2;
	public Texture texture3;
	public Texture texture4;


	// 프레임
	private void Update()
	{
		if (Input.GetKeyDown(KeyCode.N))
		{
			Debug.Log("ASD");

			ItemData itemData;

			itemData.code = 1;
			itemData.name = "ASD";
			itemData.texture = texture1;

			inventoryManager.AddItem(itemData);
		}
	}
}
