using UnityEngine;

public class PlayerManager : MonoBehaviour
{
	public static PlayerManager instance;	// 싱글턴 인스턴스

	// 인스펙터 노출 변수
	// 일반
	[SerializeField]
	private PlayerController	playerControl;              // 플레이어 컨트롤
	[SerializeField]
	private MoveParticle		moveParticle;               // 움직임 파티클

	// 인스펙터 비노출 변수
	// 수치
	[HideInInspector]
	public	bool				isGround = false;			// 바닥인지


	// 초기화
	private void Awake()
	{
		if (instance == null)
		{
			instance = this;
		}

		if (playerControl == null)
		{
			playerControl = GetComponent<PlayerController>();
		}
	}

	// 트리거 진입
	private void OnTriggerEnter2D(Collider2D collision)
	{
		// 블록일 경우
		if (collision.CompareTag("Block") || collision.CompareTag("DangerBlock") || collision.CompareTag("Ball") || collision.CompareTag("SoilBlock") || collision.CompareTag("CustomBlock"))
		{
			playerControl.ResetJump();

			// 움직임 파티클 플래그 설정
			moveParticle.flagArray[1] = true;

			// 스킬 플래그 설정
			if (!collision.CompareTag("CustomBlock") && !collision.CompareTag("Ball"))
			{
				isGround = true;
			}
		}
	}

	// 트리거 안에 존재
	private void OnTriggerStay2D(Collider2D collision)
	{
		// 블록일 경우
		if (collision.CompareTag("Block") || collision.CompareTag("DangerBlock") || collision.CompareTag("Ball") || collision.CompareTag("SoilBlock") || collision.CompareTag("CustomBlock"))
		{
			playerControl.ResetJump();
		}
	}

	// 트리거 탈출
	private void OnTriggerExit2D(Collider2D collision)
	{
		// 블록일 경우
		if (collision.CompareTag("Block") || collision.CompareTag("DangerBlock") || collision.CompareTag("Ball") || collision.CompareTag("SoilBlock") || collision.CompareTag("CustomBlock"))
		{
			moveParticle.flagArray[1] = false;

			// 스킬 플래그 설정
			if (!collision.CompareTag("CustomBlock") && !collision.CompareTag("Ball"))
			{
				isGround = false;
			}
		}
	}
}
