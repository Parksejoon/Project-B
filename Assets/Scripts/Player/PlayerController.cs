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
	private MoveParticle		moveParticle;               // 이동 파티클

	[SerializeField]
	private float				moveSpeed;                  // 이동 속도
	private float				horizontalMove;             // 좌우 이동
	private float				verticalMove;				// 상하 이동

	[SerializeField]
	private float				jumpPower;                  // 점프 파워
	[SerializeField]
	private GameObject			jumpEffect;                 // 점프 이펙트
	public	int					originJumpCount;			// 기본 점프 카운트
	private int					jumpCount;                  // 점프 카운트
	private bool				isJumping = false;          // 점프 여부
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
		horizontalMove	= Input.GetAxis("Horizontal");
		verticalMove	= Input.GetAxis("Vertical");

		if (Input.GetButtonDown("Jump") && jumpCount > 0)
		{
			isJumping = true;
		}
	}

	// 고정 프레임
	private void FixedUpdate()
	{
		Move();
		
		if (isJumping) Jump();
	}

	// 점프
	private void Jump()
	{
		// 2번째 점프부터는 체공없이 일반점프
		if (jumpCount < originJumpCount)
		{
			// 이펙트
			Vector3 position = transform.position;

			position.y -= 0.5f;
			position.z = -15;

			Instantiate(jumpEffect, position, Quaternion.identity);

			// 점프
			playerRigidbody2D.velocity = Vector2.zero;
			playerRigidbody2D.AddForce(Vector2.up * jumpPower * 1.5f, ForceMode2D.Impulse);

			// 점프 갱신
			isJumping = false;
			jumpCount--;

			return;
		}

		// 점프 시작시
		if (jumpTimer == 0)
		{
			// 이펙트
			Vector3 position = transform.position;

			position.y -= 0.5f;
			position.z = -15;

			Instantiate(jumpEffect, position, Quaternion.identity);
		}

		// 점프 체공시간을 초과했거나, 점프키를 떼면
		if (!Input.GetButton("Jump") || jumpTimer >= jumpTimeLimit)
		{
			// 점프 갱신
			isJumping = false;
			jumpCount--;

			return;
		}
		
		// 점프
		playerRigidbody2D.velocity = Vector2.zero;
		playerRigidbody2D.AddForce(Vector2.up * jumpPower * ((jumpTimer * 10) + 1f), ForceMode2D.Impulse);

		// 점프 타이머 증가
		jumpTimer += Time.deltaTime;
	}

	// 점프 리셋
	public void ResetJump()
	{
		jumpCount = originJumpCount;
		jumpTimer = 0;
	}

	// 움직임
	private void Move()
	{
		Vector2 position = playerTransform.position;
		Vector3 velocity = playerRigidbody2D.velocity;

		position.x += horizontalMove * Time.deltaTime * moveSpeed;
		playerTransform.position = position;
		moveParticle.flagArray[0] = false;

		if (horizontalMove < 0)
		{
			spriteRenderer.flipX = true;
			moveParticle.flagArray[0] = true;

			if (velocity.x > 0)
			{
				playerRigidbody2D.velocity = new Vector3(velocity.x - 0.5f, velocity.y);
			}
		}
		else if (horizontalMove > 0)
		{
			spriteRenderer.flipX = false;
			moveParticle.flagArray[0] = true;

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

	// 테스트 코루틴
	private IEnumerator TestCoroutine()
	{
		yield return new WaitForSeconds(0.2f);

		isJumping = true;
	}
}
