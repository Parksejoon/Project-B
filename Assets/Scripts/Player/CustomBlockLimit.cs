using UnityEngine;

public class CustomBlockLimit : MonoBehaviour
{
	// 인스펙터 노출 변수
	// 일반
	[SerializeField]
	private CustomBlockGenerator		customBlockGenerator;		// 커스텀 블럭 생성기


	// 트리거안에 유지
	private void OnTriggerStay2D(Collider2D collision)
	{
		if (collision.CompareTag("Block"))
		{
			customBlockGenerator.canCreate = false;
		}
	}
	
	// 트리거 탈출
	private void OnTriggerExit2D(Collider2D collision)
	{
		if (collision.CompareTag("Block"))
		{
			customBlockGenerator.canCreate = true;
		}
	}
}
	