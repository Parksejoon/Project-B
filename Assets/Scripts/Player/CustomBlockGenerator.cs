using System.Collections;
using UnityEngine;

public class CustomBlockGenerator: MonoBehaviour
{
	// 인스펙터 노출 변수
	// 일반
	[SerializeField]
	private GameObject				customBlockPrefab;                  // 커스텀 블럭 프리팹

	// 수치
	public	bool					canCreate = true;	                // 생성 가능상태인지

	[SerializeField]
	private float					createZPosition;                    // 생성 z포지션

	// 인스펙터 비노출 변수
	// 일반
	private Transform				targetBlock = null;                 // 생성한 블럭


	// 프레임
	private void Update()
	{
		// 클릭 시작
		if (Input.GetMouseButtonDown(0) && canCreate)
		{
			CreateBlock();
		}

		// 클릭 중
		if (Input.GetMouseButton(0) && targetBlock != null)
		{
			StartCoroutine("ClickTimer");
		}

		// 클릭 끝
		if (Input.GetMouseButtonUp(0))
		{
			StopCoroutine("ClickTimer");
			targetBlock = null;
		}
	}

	// 블럭 생성
	private void CreateBlock()
	{
		Vector3 position = Camera.main.ScreenToWorldPoint(Input.mousePosition);
		
		position.z = createZPosition;
		targetBlock = Instantiate(customBlockPrefab, position, Quaternion.identity, transform).transform;
	}

	// 블럭 회전
	private void RotateBlock()
	{
		Vector3 position = Camera.main.ScreenToWorldPoint(Input.mousePosition);
		Vector2 targetPos;

		targetPos.x = position.x - targetBlock.position.x;
		targetPos.y = position.y - targetBlock.position.y;

		float angle = -1 * Mathf.Rad2Deg * Mathf.Atan2(targetPos.x, targetPos.y) + 90;

		targetBlock.rotation = Quaternion.Euler(new Vector3(0, 0, angle));
	}

	// 클릭 시간 코루틴
	private IEnumerator ClickTimer()
	{
		yield return new WaitForSeconds(1f);

		if (Input.GetMouseButton(0) && targetBlock != null)
		{
			RotateBlock();
		}
	}
}
