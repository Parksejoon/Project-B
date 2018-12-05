using UnityEngine;

public class Slot : MonoBehaviour
{
	[SerializeField]
	private	bool				isConstantSlot = false;     // 고정 슬롯인지
	[SerializeField]
	private Texture				noneTexture = null;         // 투명 이미지

	private UILabel				itemCountLabel;             // 아이템 개수 텍스트
	private UITexture			itemImageTexture;			// 아이템 이미지 텍스쳐

	public	GameObject			itemPrefab = null;          // 아이템 프리팹
	public	int					itemCount = 0;				// 아이템 개수

	// 초기화
	private void Awake()
	{
		itemCountLabel		= GetComponentInChildren<UILabel>();
		itemImageTexture	= GetComponentInChildren<UITexture>();

		itemCountLabel.text = "";

		if (isConstantSlot)
		{
			itemCountLabel.text = itemCount.ToString();
		}
	}

	// 아이템 등록
	public bool AddItem(GameObject _itemPrefab, int _itemCount)
	{
		if (itemPrefab == null || itemPrefab == _itemPrefab)
		{
			if (itemCount + _itemCount <= InventoryManager.instance.maxItemCount)
			{
				itemPrefab = _itemPrefab;
				itemCount += _itemCount;

				itemCountLabel.text = itemCount.ToString();
				itemImageTexture.mainTexture = _itemPrefab.GetComponent<SpriteRenderer>().sprite.texture;

				return true;
			}

			return false;
		}

		return false;
	}

	// 아이템 비교
	public bool CompareItem(GameObject _itemPrefab, int _itemCount)
	{
		if (itemPrefab == _itemPrefab)
		{
			AddItem(_itemPrefab, _itemCount);

			return true;
		}

		return false;
	}

	// 아이템 사용
	public GameObject UseItem()
	{
		if (itemCount > 0)
		{
			itemCount--;
			itemCountLabel.text = itemCount.ToString();

			GameObject returnPrefab = itemPrefab;

			if (itemCount <= 0 && !isConstantSlot)
			{
				ResetSlot();
			}

			return returnPrefab;
		}

		return null;
	}

	// 슬롯 초기화
	private void ResetSlot()
	{
		itemCountLabel.text = "";
		itemImageTexture.mainTexture = noneTexture; 
		itemPrefab = null;
	}
}
