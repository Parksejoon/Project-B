using System.Collections;
using UnityEngine;

// 플레이어 매니저
// 스테이터스 관리, 피격, 타격
public class PlayerManager : Character
{
	// (@@ 여기서 싱글톤 사용 금지 @@)

	private bool isInvincibility;                   // 무적 상태인지 체크 플래그
	public bool IsInvincibility                     // 접근자
	{
		get { return isInvincibility; }
		private set { isInvincibility = value; }
	}

	[SerializeField]
	private PlayerAnimation playerAnimation;        // 플레이어 애니메이션

	private new Rigidbody2D rigidbody2D;            // 리짓바디

	[SerializeField]
	private float		invincibleTime;				// 무적 시간


	// 초기화
	private void Awake()
	{
		// 스텟 초기화 ( 임시 )
		Statistics stats;

		stats.max_health_point = 10;
		stats.attack_damage = 1;
		stats.move_speed = 7f;
		stats.attack_speed = 1.0f;
		stats.abillity_power = 0;
		stats.defensive_power = 1.0f;

		SetStats(stats);

		// 초기화
		rigidbody2D = GetComponent<Rigidbody2D>();

		if (playerAnimation == null)
		{
			playerAnimation = GetComponent<PlayerAnimation>();
		}

		IsInvincibility = false;
	}

	// 피격
	public override void Dealt(int damage, Vector3 attackPosition)
	{
		// 무적상태 확인
		if (!IsInvincibility)
		{
			// 대미지 계산
			base.Dealt(damage, attackPosition);

			// 플레이어 넉백
			float shotWay = transform.position.x - attackPosition.x;
			Vector2 shotVec2 = new Vector2(shotWay, 0.6f);

			shotVec2 = shotVec2.normalized;
			shotVec2 *= 7f;

			rigidbody2D.velocity = Vector2.zero;
			rigidbody2D.AddForce(shotVec2, ForceMode2D.Impulse);

			// 무적상태
			StartCoroutine(Invincible());
		}
	}

	// 무적 상태
	private IEnumerator Invincible()
	{
		IsInvincibility = true;
		gameObject.layer = 12;

		playerAnimation.StartBlinking();
		yield return new WaitForSeconds(invincibleTime);
		playerAnimation.StopBlinking();

		gameObject.layer = 9;
		IsInvincibility = false;
	}
}
