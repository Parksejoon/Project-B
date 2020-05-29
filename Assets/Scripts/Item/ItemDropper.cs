using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
struct Drop
{
	public Drop(int _itemCode, float _dropChance)
	{
		itemCode = _itemCode;
		dropChance = _dropChance;
	}

	public int		itemCode;		// 아이템 코드

	[Range(0.0f, 1.0f)]
	public float	dropChance;		// 아이템 드롭 확률
}

public class ItemDropper : MonoBehaviour
{
	[SerializeField]
	private Drop[]		dropItemList;		// 드롭 아이템 목록


	// 랜덤 아이템 드롭
	public void DropRandomItem()
	{
		float sumChance = 0;
		float rand = Random.Range(0, 1.0f);
		Drop target = new Drop(0, 0);

		foreach (var dropItem in dropItemList)
		{
			sumChance += dropItem.dropChance;

			if (rand <= sumChance)
			{
				target = dropItem;

				break;
			}
		}
		
		var obj = Instantiate(ItemParser.GetFieldPrefab(), transform.position, Quaternion.identity, transform);

		obj.GetComponent<FieldItem>().Init(ItemParser.GetItemByCode(target.itemCode));
	}
}
