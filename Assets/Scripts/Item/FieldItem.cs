using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FieldItem : MonoBehaviour
{
	private SpriteRenderer spriteRenderer;		// 스프라이트 렌더러

	private ItemData itemData;      // 아이템 데이터


	// 초기화
	private void Awake()
	{
		spriteRenderer = GetComponent<SpriteRenderer>();
	}

	// 트리거 엔터
	private void OnTriggerEnter2D(Collider2D collision)
	{
		if (collision.CompareTag("Player"))
		{
			Debug.Log("AD");
			InventoryManager.instance.AddItem(itemData);
			DeleteThis();	
		}
	}

	// 초기화
	public void Init(ItemData _itemData)
	{
		itemData = _itemData;

		spriteRenderer.sprite = Sprite.Create(itemData.texture, new Rect(0, 0, itemData.texture.width, itemData.texture.height), new Vector2(0.5f, 0.5f));
	}

	// 아이템 삭제
	private void DeleteThis()
	{
		Destroy(gameObject);
	}

	public ItemData GetItemData()
	{
		return itemData;
	}
}
