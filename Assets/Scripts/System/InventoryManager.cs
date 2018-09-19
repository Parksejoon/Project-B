using UnityEngine;

public class InventoryManager : MonoBehaviour
{
	// 인스펙터 노출 변수
	// 일반
	[SerializeField]
	private Slot[]				inventory;				// 인벤토리


	// 아이템 등록
	public void AddItem(GameObject itemPrefab, int itemCount)
	{
		foreach (Slot slot in inventory)
		{
			if (slot.AddItem(itemPrefab, itemCount))
			{
				break;
			}
		}
	}

	// 아이템 사용
	public bool UseItem(int slotNumber)
	{
		return inventory[slotNumber - 1].UseItem();
	}
}