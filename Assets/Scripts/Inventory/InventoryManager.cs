﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryManager : MonoBehaviour
{
	[SerializeField]
	private GameObject		inventoryPanel;         // 인벤토리 패널

	[SerializeField]
	private GameObject		itemInventory;			// 아이템 인벤토리

	private ItemUI[]		inventoryArray;			// UI에 표시되는 아이템들의 배열

	private bool			isOpen = false;         // 인벤토리가 열려있는지


	// 초기화
	private void Awake()
	{
		inventoryArray = itemInventory.GetComponentsInChildren<ItemUI>();

		foreach (var itemUI in inventoryArray)
		{
			itemUI.Init();
		}
	}

	// 프레임
	private void Update()
	{
		// (## 인벤토리 여는 키 ##)
		if (Input.GetKeyDown(KeyCode.I))
		{
			isOpen = !isOpen;
			inventoryPanel.SetActive(isOpen);
		}

		// ($$ 테스트용 $$)
		if (Input.GetKeyDown(KeyCode.L))
		{
			foreach (var targetUI in inventoryArray)
			{
				targetUI.ShowData();
			}
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

		Debug.Log("Inventory is full.");
		return false;
	}

	// 아이템 삭제
	public void DeleteItem(int index)
	{
		inventoryArray[index].DeleteItem();
	}
}
