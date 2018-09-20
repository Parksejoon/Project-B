using UnityEngine;

public class Slot : MonoBehaviour
{
	// 인스펙터 노출 변수
	// 수치
	[SerializeField]
	private	bool				isConstantSlot = false;		// 고정 슬롯인지

	public	GameObject			itemPrefab = null;			// 아이템 프리팹
	public	int					itemCount = 0;				// 아이템 개수

	// 인스펙터 비노출 변수
	// 일반
	private UILabel				itemCountLabel;				// 아이템 개수 텍스트


	// 초기화
	private void Awake()
	{
		itemCountLabel = GetComponentInChildren<UILabel>();

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
			itemPrefab = _itemPrefab;
			itemCount += _itemCount;

			itemCountLabel.text = itemCount.ToString();

			return true;
		}
		else
		{
			return false;
		}
	}

	// 아이템 사용
	public bool UseItem()
	{
		if (itemCount > 0)
		{
			itemCount--;
			itemCountLabel.text = itemCount.ToString();

			if (itemCount <= 0 && !isConstantSlot)
			{
				ResetSlot();
			}

			return true;
		}

		return false;
	}

	// 슬롯 초기화
	private void ResetSlot()
	{
		itemCountLabel.text = "";
		itemPrefab = null;
	}
}
