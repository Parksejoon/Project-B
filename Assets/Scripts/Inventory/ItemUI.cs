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
	public static ItemUI	clickTarget;			// 현재 클릭으로 들고있는 아이템UI
	private BoxCollider2D	boxCollider2D;			// 클릭용 collider

	private ItemData	itemData;               // 아이템 데이터
	private Vector3		originPositon;          // 원래 자리
	private IEnumerator mouseFollow;			// 마우스 따라가기 코루틴

	private UITexture	uITexture;				// 텍스쳐


	// 초기화
	private void Awake()
	{
		boxCollider2D = GetComponent<BoxCollider2D>();
		uITexture = GetComponent<UITexture>();

		originPositon = transform.position;

		GetComponent<UIEventTrigger>().onClick.Add(new EventDelegate(ClickItem));
	}

	// copy
	private void Copy(ItemUI target)
	{
		itemData = target.itemData;
	}

	// refresh
	private void Refresh()
	{
		uITexture.mainTexture = itemData.texture;
	}

	// position reset
	private void ResetPosition()
	{
		transform.position = originPositon;
	}

	// 스왑
	private void Swap(ref ItemUI target)
	{
		ItemUI temp = new ItemUI();

		temp.Copy(target);
		target.Copy(this);
		Copy(temp);

		Refresh();
		target.Refresh();
	}

	// 홀드
	private void Hold()
	{
		clickTarget = this;
		boxCollider2D.enabled = false;

		mouseFollow = MouseFollow();
		StartCoroutine(mouseFollow);
	}

	// 언홀드
	private void UnHold()
	{
		boxCollider2D.enabled = true;
		
		if (mouseFollow != null)
		{
			StopCoroutine(mouseFollow);
		}

		ResetPosition();
	}

	// ($$ 데이터 디버그 로그 $$)
	public void ShowData()
	{
		Debug.Log(gameObject.name + itemData.code);
	}

	// 아이템 클릭
	public void ClickItem()
	{
		// 새로 아이템을 드는 경우
		if (clickTarget == null)
		{
			// 아이템이 있으면 홀딩
			if (!IsEmpty())
			{
				Hold();
			}
		}
		// 기존에 있는 아이템을 바꾸는 경우
		else
		{
			// 비어있으면 언홀드
			if (IsEmpty())
			{
				clickTarget.UnHold();

				Swap(ref clickTarget);

				clickTarget = null;
			}
			else
			{
				Swap(ref clickTarget);
			}
		}
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

	// 마우스 따라감 코루틴
	private IEnumerator MouseFollow()
	{
		while (true)
		{
			transform.position = (Vector2)CameraManager.uiCamera.ScreenToWorldPoint(Input.mousePosition);

			yield return null;
		}
	}
}
