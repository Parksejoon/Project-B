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
	private float					createZPosition;					// 생성 z포지션


	// 프레임
	private void Update()
	{
		// 클릭
		if (Input.GetMouseButtonDown(0) && canCreate)
		{
			CreateBlock();
		}
	}

	// 블럭 생성
	private void CreateBlock()
	{
		Vector3 position = Camera.main.ScreenToWorldPoint(Input.mousePosition);

		position.z = createZPosition;
		Instantiate(customBlockPrefab, position, Quaternion.identity, transform);
	}
}
