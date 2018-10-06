using System.Collections;
using UnityEngine;

public class CustomBlockGenerator: MonoBehaviour
{
	// 인스펙터 노출 변수
	// 일반
	public	GameObject				customBlockPrefab;                  // 커스텀 블럭 프리팹

	// 수치
	[SerializeField]
	private float					createZPosition;                    // 생성 z포지션

	// 인스펙터 비노출 변수
	// 일반
	private Transform				targetBlock = null;                 // 생성한 블럭

	// 수치
	private Vector3					mousePosition;                      // 마우스 위치
	private int						slotNumber = 1;						// 슬롯 번호


	// 프레임
	private void Update()
	{
		// 클릭 시작
		if (Input.GetMouseButtonDown(0) && CanCreate())
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

		// 슬롯 변경
		if (Input.GetKeyDown(KeyCode.Alpha1))
		{
			slotNumber = 1;
		}
		else if (Input.GetKeyDown(KeyCode.Alpha2))
		{
			slotNumber = 2;
		}
		else if (Input.GetKeyDown(KeyCode.Alpha3))
		{
			slotNumber = 3;
		}
	}

	// 블럭 생성
	private void CreateBlock()
	{
		targetBlock = Instantiate(customBlockPrefab, mousePosition, Quaternion.identity, transform).transform;
	}

	// 블럭 생성 가능상태 확인
	private bool CanCreate()
	{
		mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
		mousePosition.z = createZPosition;

		Collider2D[] hitColliders2D = Physics2D.OverlapBoxAll(mousePosition, new Vector2(1f, 1f), 0);

		foreach (Collider2D collider in hitColliders2D)
		{
			if (collider.CompareTag("Block") || collider.CompareTag("NoCreate") || collider.CompareTag("SoilBlock"))
			{
				return false;
			}
		}

		customBlockPrefab = InventoryManager.instance.UseItem(slotNumber);

		if (customBlockPrefab == null)
		{
			return false;
		}

		return true;
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
