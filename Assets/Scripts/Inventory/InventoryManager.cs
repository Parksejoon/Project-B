using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryManager : MonoBehaviour
{
	[SerializeField]
	private GameObject		inventoryPanel;			// 인벤토리 패널

	private ItemUI[]		inventoryArray;			// UI에 표시되는 아이템들의 배열

	private bool			isOpen = false;         // 인벤토리가 열려있는지


	// 초기화
	private void Awake()
	{
		inventoryArray = inventoryPanel.GetComponentsInChildren<ItemUI>();
	}

	// 프레임
	private void Update()
	{
		if (Input.GetKeyDown(KeyCode.I))
		{
			isOpen = !isOpen;
			inventoryPanel.SetActive(isOpen);
		}
	}

	// 아이템 추가
	public bool AddItem(ItemData itemData)
	{
		foreach (var targetUI in inventoryArray)
		{
			if (targetUI.IsEmpty())
			{
				targetUI.SetItem(itemData);

				return true;
			}
		}

		return false;
	}

	// 아이템 삭제
	public void DeleteItem(int index)
	{
		inventoryArray[index].DeleteItem();
	}
}
