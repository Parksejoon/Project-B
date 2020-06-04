using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemSlot : MonoBehaviour
{
	public static ItemSlot		clickTarget;			// 현재 클릭으로 들고있는 아이템UI
	public static GameObject	itemDescriptionPanel;	// 아이템 설명 패널

	private BoxCollider2D	boxCollider2D;      // 클릭용 collider
	private UITexture		uITexture;          // 텍스쳐 ui

	protected ItemData		itemData;			// 아이템 데이터

	private Vector3			originPositon;      // 원래 자리
	private IEnumerator		mouseFollow;        // 마우스 따라가기 코루틴
	private IEnumerator		descriptionTimer;   // 설명창 타이머 코루틴

	private bool			timerFlag;			// 타이머 플래그


	// 초기화
	public virtual void Init()
	{
		boxCollider2D = GetComponent<BoxCollider2D>();
		uITexture = GetComponent<UITexture>();

		// 클릭 활성화
		var eventTrigger = GetComponent<UIEventTrigger>();
		eventTrigger.onClick.Add(new EventDelegate(ClickItem));
		eventTrigger.onHoverOver.Add(new EventDelegate(StartDescriptionTimer));
		eventTrigger.onHoverOut.Add(new EventDelegate(StopDescriptionTimer));

		// 초기 포지션값 생성
		originPositon = transform.position;

		// 텍스쳐 refresh
		TextureRefresh();
	}

	// ($$ 데이터 디버그 로그 $$)
	public void ShowData()
	{
		Debug.Log(gameObject.name + " " + itemData.code);
	}

	// 홀드 리셋
	public static void ResetHold()
	{
		if (clickTarget != null)
		{
			clickTarget.UnHold();
		}
	}

	// 홀드
	private void Hold()
	{
		clickTarget = this;
		boxCollider2D.enabled = false;

		mouseFollow = MouseFollow();
		StartCoroutine(mouseFollow);
	}

	// 언홀드 스왑
	private void UnHold(ItemSlot swapTarget)
	{
		boxCollider2D.enabled = true;
		
		if (mouseFollow != null)
		{
			StopCoroutine(mouseFollow);
		}

		ResetPosition();

		Swap(swapTarget);

		clickTarget = null;
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
		clickTarget = null;
	}

	// 아이템 설명창 타이머 시작
	public void StartDescriptionTimer()
	{
		// 아무것도 클릭중이 아닐 때 & 아이템이 존재할 때
		if (clickTarget == null && itemData.code != 0)
		{
			descriptionTimer = DescriptionTimer();
			StartCoroutine(descriptionTimer);
		}
	}

	// 아이템 설명창 타이머 중지
	public void StopDescriptionTimer()
	{
		if (descriptionTimer != null)
		{
			StopCoroutine(descriptionTimer);
		}
	}

	// 아이템 설명 표시
	private void ShowItemDescription()
	{
		itemDescriptionPanel.SetActive(true);
		itemDescriptionPanel.transform.position = (Vector2)CameraManager.uiCamera.ScreenToWorldPoint(Input.mousePosition);
	}

	// 아이템 설명 끔
	private void DisableItemDescription()
	{
		itemDescriptionPanel.SetActive(false);
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
				clickTarget.UnHold(this);
			}
			else
			{
				Swap(clickTarget);
			}
		}
	}

	// 아이템 등록
	public void SetItem(ItemData _itemData)
	{
		itemData = _itemData;

		TextureRefresh();
	}

	// 아이템 삭제
	public void DeleteItem()
	{
		itemData = ItemParser.GetItemByCode(0);

		TextureRefresh();
	}

	// 아이템 코드 get
	public int GetItemCode()
	{
		return itemData.code;
	}

	// 아이템 프리팹 get
	public GameObject GetPrefab()
	{
		return itemData.prefab;
	}

	// 존재하는지 확인
	public bool IsEmpty()
	{
		if (itemData.code == 0) return true;

		return false;
	}

	// copy item ui struct
	private void Copy(ItemSlot target)
	{
		itemData = target.itemData;
	}

	// 텍스쳐 refresh
	private void TextureRefresh()
	{
		uITexture.mainTexture = itemData.texture;
	}

	// position reset
	private void ResetPosition()
	{
		transform.position = originPositon;
	}

	// 스왑
	private void Swap(ItemSlot target)
	{
		var temp = new ItemSlot();

		temp.Copy(target);
		target.Copy(this);
		Copy(temp);

		TextureRefresh();
		target.TextureRefresh();
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

	// 설명창 타이머 코루틴
	private IEnumerator DescriptionTimer()
	{
		bool descriptionEnabled = false;
		var previousPos = (Vector2)CameraManager.uiCamera.ScreenToWorldPoint(Input.mousePosition);
		var timerCor = MouseOverTimer();

		StartCoroutine(timerCor);

		while (true)
		{
			if (clickTarget != null)
			{
				DisableItemDescription();
				descriptionEnabled = false;
				yield return null;

				continue;
			}

			var currentPos = (Vector2)CameraManager.uiCamera.ScreenToWorldPoint(Input.mousePosition);

			// 설명창이 띄어져있으면
			if (descriptionEnabled)
			{
				// 마우스가 이동 했으면
				if (previousPos != currentPos)
				{
					// 설명창을 끔
					DisableItemDescription();

					// 타이머를 재시작
					descriptionEnabled = false;
					StartCoroutine(timerCor);
				}
			}
			// 설명창이 띄어져있지 않으면
			else
			{
				// 마우스가 이동 했으면
				if (previousPos != currentPos)
				{
					previousPos = currentPos;

					// 타이머를 처음부터 다시 측정
					StopCoroutine(timerCor);

					timerCor = MouseOverTimer();
					StartCoroutine(timerCor);
				}

				// 시간이 다되면
				if (timerFlag)
				{
					// 설명창을 띄움
					ShowItemDescription();

					// 설명창이 띄어져있다고 flag를 변경
					descriptionEnabled = true;
				}
			}

			yield return null;
		}

		DisableItemDescription();
	}

	// 마우스 올려논 시간 타이머 코루틴
	private IEnumerator MouseOverTimer()
	{
		timerFlag = false;

		yield return new WaitForSeconds(0.5f);

		timerFlag = true;
	}
}
