using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryManager : MonoBehaviour
{
	[SerializeField]
	private GameObject		inventoryPanel;         // 인벤토리 패널

	private ItemUI[]		inventoryArray;         // 아이템 인벤토리 배열

	private bool			isOpen = false;         // 인벤토리가 열려있는지

	private const string dataCaheInventoryItemListKey = "InventoryItemList";


	// 초기화
	private void Awake()
	{
		// 인벤토리 슬롯 UI 가져오기
		inventoryArray = inventoryPanel.GetComponentInChildren<UIGrid>().GetComponentsInChildren<ItemUI>();

		// 데이터 셋
		int[] itemCodeList = DataCache<int>.GetArrayData(dataCaheInventoryItemListKey);
		
		for (int i = 0; i < itemCodeList.Length; i++)
		{
			inventoryArray[i].SetItemCode(itemCodeList[i]);
		}
	}

	// 시작 
	private void Start()
	{
		// 슬롯 목록 초기화
		ItemUI[] itemSlotArray = inventoryPanel.GetComponentsInChildren<ItemUI>();

		foreach (var itemUI in itemSlotArray)
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

	// 오브젝트 비활성화 시
	private void OnDisable()
	{
		SaveData();
	}

	// 인벤토리 목록 저장
	private void SaveData()
	{
		List<int> itemCodeList = new List<int>();

		foreach (var itemUI in inventoryArray)
		{
			itemCodeList.Add(itemUI.GetItemCode());
		}
		
		DataCache<int>.SaveArrayData(dataCaheInventoryItemListKey, itemCodeList.ToArray());
	}

	// 아이템 홀드 리셋
	public void ResetHoldItem()
	{
		ItemUI.ResetHold();
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
