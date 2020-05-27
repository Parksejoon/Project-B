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
		returnValue.itemType = GetItemTypeByCode(itemCode);
		returnValue.name = "Temp";
		returnValue.texture = GetTextureByCode(itemCode);
		returnValue.prefab = GetPrefabByCode(itemCode);

		return returnValue;
	}

	// 코드로 아이템 프리팹을 가져옴
	public static GameObject GetPrefabByCode(int itemCode)
	{
		GameObject returnValue = null;

		switch (GetItemTypeByCode(itemCode))
		{
			case ItemType.None:
				break;

			case ItemType.Weapon:

				returnValue = Resources.Load("Weapons/" + itemCode) as GameObject;
				break;

			default:
				break;
		}


		return returnValue;
	}

	// 코드로 아이템 텍스쳐를 가져옴
	public static Texture GetTextureByCode(int itemCode)
	{
		Texture returnValue = null;

		switch (GetItemTypeByCode(itemCode))
		{
			case ItemType.None:
				break;

			case ItemType.Weapon:

				returnValue = Resources.Load("Texture/Weapons" + itemCode) as Texture;
				break;

			default:
				break;
		}


		return returnValue;
	}

	// 아이템 코드로 아이템 타입 확인
	public static ItemType GetItemTypeByCode(int itemCode)
	{
		if (itemCode == 0) return ItemType.None;
		if (itemCode / 100 == 0) return ItemType.Weapon;

		return ItemType.None;
	}
}
