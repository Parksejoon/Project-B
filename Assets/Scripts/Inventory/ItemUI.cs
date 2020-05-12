using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct ItemData
{
	public int code;
	public string name;
	public Texture texture;
}

public class ItemUI : MonoBehaviour
{
	private ItemData	itemData;		// 아이템 데이터

	private UITexture	uITexture;       // 텍스쳐


	// 초기화
	private void Awake()
	{
		uITexture = GetComponent<UITexture>();
	}

	// refresh
	private void Refresh()
	{
		uITexture.mainTexture = itemData.texture;
	}

	// 존재하는지 확인
	public bool IsEmpty()
	{
		if (itemData.code == 0) return true;

		return false;
	}

	// 아이템 등록
	public void SetItem(ItemData _itemData)
	{
		Debug.Log("ASDFF");

		itemData = _itemData;

		Refresh();
	}

	// 아이템 삭제
	public void DeleteItem()
	{
		itemData.code = 0;
		itemData.name = "";
		itemData.texture = null;

		Refresh();
	}
}
