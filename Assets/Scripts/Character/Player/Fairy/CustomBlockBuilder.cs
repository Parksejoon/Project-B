using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 커스텀 블록 설치
// 클릭 입력,
public class CustomBlockBuilder : MonoBehaviour
{
	public	GameObject			customBlockPrefab;							// 커스텀 블럭 프리팹

	[SerializeField]
	private Transform			fairyTransform;								// 요정 트랜스폼
	
	[SerializeField]
	private int					maxCreatableBlockCount;						// 최대 생성 가능 블럭 갯수


	private List<GameObject>	createdBlockList;							// 생성된 블럭들의 목록

	private Transform			currentCreatingTargetBlock = null;			// 현재 생성중인 블럭
	private Vector3				targetPosition;                             // 생성 위치

	private bool				setblockAxisInUse = false;                  // 블럭설치 키 사용 플래그
	private IEnumerator			clickTimer;									// 클릭 타이머 코루틴


	// 초기화
	private void Awake()
	{
		createdBlockList = new List<GameObject>();

		ResourceLoader.Load("Effects/Block");

		ObjectPoolManager.Create("BlockCreated", ResourceLoader.Get("BlockCreated"), 10);
		ObjectPoolManager.Create("CountExcess", ResourceLoader.Get("CountExcess"), 20);
		ObjectPoolManager.Create("SpaceNarrower", ResourceLoader.Get("SpaceNarrower"), 20);

		clickTimer = ClickTimer();
	}

	// 프레임
	private void Update()
	{
		// (!! 여기 입력부분 나중에 패드 호환 가능하게 변경 !!)

		//// 클릭 시작
		//if (Input.GetMouseButtonDown(0))
		//{
		//	if (CanCreate())
		//	{
		//		CreateBlock();
		//	}
		//	else
		//	{
		//	}
		//}

		//// 클릭 중
		//if (Input.GetMouseButton(0) && currentCreatingTargetBlock != null)
		//{
		//	StartCoroutine("ClickTimer");
		//}

		//// 클릭 끝
		//if (Input.GetMouseButtonUp(0))
		//{
		//	StopCoroutine("ClickTimer");
		//	currentCreatingTargetBlock = null;
		//}


		if (Input.GetAxis("Setblock") != 0)
		{
			if (setblockAxisInUse == false)
			{
				// 설치가 가능하면
				if (CanCreate())
				{
					// 클릭 타이머 중지
					StopCoroutine(clickTimer);

					// 블럭 설치
					CreateBlock();

					// 클릭 타이머 시작
					clickTimer = ClickTimer();
					StartCoroutine(clickTimer);
				}

				setblockAxisInUse = true;
			}
		}

		if (Input.GetAxis("Setblock") == 0)
		{
			// 블럭 설치 해제
			//StopCoroutine(clickTimer);
			currentCreatingTargetBlock = null;

			setblockAxisInUse = false;
		}
	}

	// 블럭 생성 가능상태 확인
	private bool CanCreate()
	{
		if (Pause.isPause)
		{
			return false;
		}

		targetPosition = fairyTransform.position;
		targetPosition.z = Depth.CustomBlock;

		// 최대 갯수를 초과해서 설치를 못하는 경우
		if (createdBlockList.Count >= maxCreatableBlockCount)
		{
			// 블럭 설치가 불가능하다는 이펙트 표시
			targetPosition.z = Depth.Effect;

			ObjectPoolManager.GetGameObject("CountExcess", targetPosition);
			//Instantiate(countexcessEffect, targetPosition, Quaternion.identity);

			return false;
		}

		// 위치가 협소해서 블럭을 설치 못하는 경우
		Collider2D[] hitColliders2D = Physics2D.OverlapBoxAll(targetPosition, new Vector2(1f, 1f), 0);

		foreach (Collider2D collider in hitColliders2D)
		{
			if (collider.CompareTag("Block") || collider.CompareTag("CustomBlock") || 
				collider.CompareTag("DangerBlock") || collider.CompareTag("NoCreate") || 
				collider.CompareTag("Ball") || collider.CompareTag("SoilBlock"))
			{
				// 블럭 설치가 불가능하다는 이펙트 표시
				targetPosition.z = Depth.Effect;

				ObjectPoolManager.GetGameObject("SpaceNarrower", targetPosition);
				//Instantiate(spacenarrowerEffect, targetPosition, Quaternion.identity);

				return false;
			}
		}

		//customBlockPrefab = InventoryManager.instance.UseItem(slotNumber);

		//if (customBlockPrefab == null)
		//{
		//	return false;
		//}

		return true;
	}

	// 블럭 초기화
	public void ResetBlocks()
	{
		foreach (GameObject target in createdBlockList)
		{
			// 블럭 삭제
			Destroy(target);
		}

		createdBlockList.Clear();
	}

	// 블럭 생성
	private void CreateBlock()
	{
		// 생성 이펙트
		ObjectPoolManager.GetGameObject("BlockCreated", targetPosition);
		//Instantiate(itemCreateEffect, targetPosition, Quaternion.identity, transform);

		// 블럭 생성
		currentCreatingTargetBlock = Instantiate(customBlockPrefab, targetPosition, Quaternion.identity, transform).transform;

		createdBlockList.Add(currentCreatingTargetBlock.gameObject);
	}

	// 블럭 회전
	private void RotateBlock()
	{
		Vector3 position = Camera.main.ScreenToWorldPoint(Input.mousePosition);
		Vector2 targetPos;

		targetPos.x = position.x - currentCreatingTargetBlock.position.x;
		targetPos.y = position.y - currentCreatingTargetBlock.position.y;

		float angle = -1 * Mathf.Rad2Deg * Mathf.Atan2(targetPos.x, targetPos.y) + 90;

		currentCreatingTargetBlock.rotation = Quaternion.Euler(new Vector3(0, 0, angle));
	}

	// 클릭 시간 코루틴
	private IEnumerator ClickTimer()
	{
		yield return new WaitForSeconds(1f);

		while (setblockAxisInUse && currentCreatingTargetBlock != null)
		{
			RotateBlock();

			yield return null;
		}
	}
}
