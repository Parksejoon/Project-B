using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemParser : MonoBehaviour
{
	// 코드로 아이템 데이터를 가져옴
	public static ItemData GetItemByCode(int itemCode)
	{
		ItemData returnValue;

		returnValue.code = itemCode;
		returnValue.name = "";
		returnValue.texture = null;
		returnValue.prefab = null;
		returnValue.itemType = GetItemTypeByCode(itemCode);

		switch (returnValue.itemType)
		{
			case ItemType.None:
				break;

			case ItemType.Weapon:

				returnValue.name = "";
				returnValue.texture = Resources.Load("Texture/Weapon" + itemCode) as Texture;
				returnValue.prefab = Resources.Load("Weapon/" + itemCode) as GameObject;
				break;

			default:
				break;
		}

		return returnValue;
	}

	// 코드로 아이템 프리팹을 가져옴
	public static GameObject GetPrefabByCode(int itemCode)
	{
		return null;
	}

	// 아이템 코드로 아이템 타입 확인
	public static ItemType GetItemTypeByCode(int itemCode)
	{
		if (itemCode == 0) return ItemType.None;
		if (itemCode / 100 == 0) return ItemType.Weapon;

		return ItemType.None;
	}
}
