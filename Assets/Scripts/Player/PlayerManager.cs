using UnityEngine;

public class PlayerManager : MonoBehaviour
{
	// 인스펙터 노출 변수
	// 일반
	[SerializeField]
	private PlayerController				playerControl;              // 플레이어 컨트롤


	// 초기화
	private void Awake()
	{
		if (playerControl == null)
		{
			playerControl = GetComponent<PlayerController>();
		}
	}

	// 충돌체 진입
	private void OnCollisionEnter2D(Collision2D collision)
	{
		// 블록일 경우
		if (collision.gameObject.layer == 8)
		{
			playerControl.ResetJump();
		}
	}
}
