using UnityEngine;

// 플레이어 점프 매니저
// 착지 판정, 체공 판정
public class PlayerJumpManager : MonoBehaviour
{
	private static bool isGround;		// 바닥인지
	public static bool IsGround			// 접근자
	{
		get
		{
			return isGround;
		}
		private set
		{
			isGround = value;
		}
	}

	[SerializeField]
	private PlayerController	playerController;       // 플레이어 컨트롤
	[SerializeField]
	private CustomBlockBuilder	customBlockBuilder;		// 커스텀 블럭 빌더
	[SerializeField]
	private MoveParticle		moveParticle;           // 움직임 파티클
	

	// 초기화
	private void Awake()
	{
		IsGround = true;

		if (playerController == null)
		{
			playerController = GetComponent<PlayerController>();
		}
	}

	// 트리거 진입
	private void OnTriggerEnter2D(Collider2D collision)
	{
		// 모든 착지 가능한 블록일 경우
		if (collision.CompareTag("Block") || collision.CompareTag("DangerBlock") 
			|| collision.CompareTag("Ball") || collision.CompareTag("SoilBlock") 
			|| collision.CompareTag("CustomBlock"))
		{
			playerController.ResetJump();

			// 이펙트 플래그 설정
			moveParticle.flagArray[1] = true;

			// 플래그 설정
			if (!collision.CompareTag("CustomBlock") && !collision.CompareTag("Ball"))
			{
				IsGround = true;
			}
		}

		// 커스텀 블럭을 제외한 착지 가능한 블록일 경우 블록 초기화
		if (collision.CompareTag("Block") || collision.CompareTag("DangerBlock")
			|| collision.CompareTag("Ball") || collision.CompareTag("SoilBlock"))
		{
			if (customBlockBuilder != null) customBlockBuilder.ResetBlocks();
		}
	}

	// 트리거 안에 존재
	private void OnTriggerStay2D(Collider2D collision)
	{
		// 블록일 경우
		if (collision.CompareTag("Block") || collision.CompareTag("DangerBlock") 
			|| collision.CompareTag("Ball") || collision.CompareTag("SoilBlock") 
			|| collision.CompareTag("CustomBlock"))
		{
			playerController.ResetJump();
		}
	}

	// 트리거 탈출
	private void OnTriggerExit2D(Collider2D collision)
	{
		// 블록일 경우
		if (collision.CompareTag("Block") || collision.CompareTag("DangerBlock") 
			|| collision.CompareTag("Ball") || collision.CompareTag("SoilBlock") 
			|| collision.CompareTag("CustomBlock"))
		{
			playerController.Flight();

			// 이펙트 플래그 설정
			moveParticle.flagArray[1] = false;

			// 플래그 설정
			if (!collision.CompareTag("CustomBlock") && !collision.CompareTag("Ball"))
			{
				IsGround = false;
			}
		}
	}
}
