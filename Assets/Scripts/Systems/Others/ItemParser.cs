﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ItemType
{
	None = 0,
	Weapon = 1
}

public struct ItemData
{
	public int			code;
	public string		name;
	public ItemType		itemType;
	public Texture2D	sprite;
	public GameObject	prefab;
}

public class ItemParser : MonoBehaviour
{
	private static Dictionary<int, string> itemNameDictionary;


	// 코드로 아이템 데이터를 가져옴
	public static ItemData GetItemByCode(int itemCode)
	{
		ItemData returnValue;

		returnValue.code		= itemCode;
		returnValue.itemType	= GetItemTypeByCode(itemCode);
		returnValue.name		= itemNameDictionary[itemCode];
		returnValue.sprite		= GetSpriteByCode(itemCode);
		returnValue.prefab		= GetPrefabByCode(itemCode);

		return returnValue;
	}

	// 모든 아이템 이름 dictionary를 json으로 불러옴
	public static void InitNameDictionary()
	{
		var jsonText = Resources.Load("Names") as TextAsset;

		itemNameDictionary = JsonManager.JsonToOject<Dictionary<int, string>>(jsonText.ToString());

		Debug.Log(itemNameDictionary[0]);
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

				returnValue = Resources.Load("Weapons/" + itemCode + "/Prefab") as GameObject;
				break;

			default:
				break;
		}


		return returnValue;
	}

	// 코드로 아이템 텍스쳐를 가져옴
	public static Texture2D GetSpriteByCode(int itemCode)
	{
		Texture2D returnValue = null;

		switch (GetItemTypeByCode(itemCode))
		{
			case ItemType.None:
				break;

			case ItemType.Weapon:

				returnValue = Resources.Load("Weapons/" + itemCode + "/Sprite") as Texture2D;
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
