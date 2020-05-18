using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour
{
	public static Transform		playerTransform;            // 플레이어 트랜스폼
	[SerializeField]
	private Rigidbody2D			playerRigidbody2D;          // 플레이어 리지드바디 2D
	[SerializeField]
	private SpriteRenderer		spriteRenderer;             // 플레이어 스프라이트 렌더러
	[SerializeField]
	private PlayerManager		playerManager;				// 플레이어 매니저

	[SerializeField]
	private MoveParticle		moveParticle;               // 이동 파티클

	[SerializeField]
	private float				horizontalMove;             // 좌우 이동
	private float				verticalMove;				// 상하 이동

	[SerializeField]
	private float				jumpPower;                  // 점프 파워
	[SerializeField]
	private GameObject			jumpEffect;                 // 점프 이펙트
	public	int					jumpCountLimit;				// 최대 점프 횟수
	private int					jumpCount;                  // 점프 횟수
	private float				jumpTimer;                  // 점프 타이머
	private float				jumpTimeLimit = 0.1f;		// 최대 점프 시간


	// 초기화
	private void Awake()
	{
		if (playerTransform == null)
		{
			playerTransform = transform;
		}

		if (playerRigidbody2D == null)
		{
			playerRigidbody2D = GetComponent<Rigidbody2D>();
		}

		ResetJump();
	}

	// 프레임
	private void Update()
	{
		horizontalMove	= InputManager.GetAxis("Horizontal");
		//verticalMove	= InputManager.GetAxis("Vertical");

		if (InputManager.GetButtonDown("Jump"))
		{
			Jump();
		}
	}

	// 고정 프레임
	private void FixedUpdate()
	{
		Move();
	}

	// 비활성화
	public void OnDisable()
	{
		moveParticle.SetParticleFlag(MoveParticle.MoveFlagType.NoMove, false);
	}

	// 점프
	private void Jump()
	{
		if (jumpCount >= jumpCountLimit) return;

		// 2번째 점프부터는 일반점프
		if (jumpCount >= 1)
		{
			//Debug.Log("Extra Jump : " + (jumpCount));

			// 이펙트
			Vector3 position = transform.position;

			position.y -= 0.5f;
			position.z = Depth.Effect;

			Instantiate(jumpEffect, position, Quaternion.identity);

			// 점프
			playerRigidbody2D.velocity = Vector2.zero;
			playerRigidbody2D.AddForce(Vector2.up * jumpPower * 3f, ForceMode2D.Impulse);

			// 점프 갱신
			jumpCount++;

			return;
		}
		// 일반 점프
		else
		{
			// 이펙트
			Vector3 position = transform.position;

			position.y -= 0.5f;
			position.z = Depth.Effect;

			Instantiate(jumpEffect, position, Quaternion.identity);

			// 코루틴
			StartCoroutine(JumpFlight());

			return;
		}
	}

	// 공중에 뜬 상태가 될시 jump count를 1로 초기화
	public void Flight()
	{
		jumpCount = 1;
	}

	// 점프 리셋
	public void ResetJump()
	{
		jumpCount = 0;
		jumpTimer = 0;
	}

	// 움직임
	private void Move()
	{
		Vector2 position = playerTransform.position;
		Vector3 velocity = playerRigidbody2D.velocity;

		position.x += horizontalMove * Time.deltaTime * playerManager.Stats.move_speed;
		playerTransform.position = position;
		moveParticle.SetParticleFlag(MoveParticle.MoveFlagType.NoMove, false);

		if (horizontalMove < 0)
		{
			spriteRenderer.flipX = true;
			moveParticle.SetParticleFlag(MoveParticle.MoveFlagType.NoMove, true);

			if (velocity.x > 0)
			{
				playerRigidbody2D.velocity = new Vector3(velocity.x - 0.5f, velocity.y);
			}
		}
		else if (horizontalMove > 0)
		{
			spriteRenderer.flipX = false;
			moveParticle.SetParticleFlag(MoveParticle.MoveFlagType.NoMove, true);

			if (velocity.x < 0)
			{
				playerRigidbody2D.velocity = new Vector3(velocity.x + 0.5f, velocity.y);
			}
		}

		if (verticalMove < 0)
		{
			playerRigidbody2D.AddForce(Vector3.down, ForceMode2D.Impulse);
		}
	}

	// 점프 체공 코루틴
	private IEnumerator JumpFlight()
	{
		while (InputManager.GetButton("Jump") && jumpTimer < jumpTimeLimit)
		{
			// 점프
			playerRigidbody2D.velocity = Vector2.zero;
			playerRigidbody2D.AddForce(Vector2.up * jumpPower * ((jumpTimer * 20) + 1f), ForceMode2D.Impulse);

			// 점프 타이머 증가
			jumpTimer += Time.deltaTime;

			yield return new WaitForFixedUpdate();
		}
	}
}
