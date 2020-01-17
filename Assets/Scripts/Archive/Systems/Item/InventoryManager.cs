using UnityEngine;

public class InventoryManager : MonoBehaviour
{
	public static InventoryManager instance;	// 싱글톤 인스턴스
	
	[SerializeField]
	private Slot[]		inventory;              // 인벤토리

	public	int			maxItemCount =  5;		// 최대 아이템 갯수


	// 초기화
	private void Awake()
	{
		instance = this;
	}

	// 아이템 추가
	public bool AddItem(GameObject itemPrefab, int itemCount)
	{
		foreach (Slot slot in inventory)
		{
			if (slot.AddItem(itemPrefab, itemCount))
			{
				return true;
			}
		}

		return false;
	}

	// 아이템 사용
	public GameObject UseItem(int slotNumber)
	{
		return inventory[slotNumber - 1].UseItem();
	}
}