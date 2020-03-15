using System.Collections;
using UnityEngine;

// 플레이어 매니저
// 스테이터스 관리, 피격, 타격
public class PlayerManager : Character
{
	// (@@ 여기서 싱글톤 사용 금지 @@)

	private bool isInvincibility;						// 무적 상태인지 체크 플래그
	public	bool IsInvincibility						// 접근자
	{
		get { return isInvincibility; }
		private set { isInvincibility = value; }
	}

	private	new Rigidbody2D	rigidbody2D;			// 리짓바디

	[SerializeField]
	private	SpriteRenderer	charSprite;				// 캐릭터 스프라이트

	[SerializeField]
	private	float			invincibleTime;         // 무적 시간

	private IEnumerator		blinkingSprite;         // 스프라이트 깜빡거림 코루틴


	// 초기화
	private void Awake()
	{
		rigidbody2D = GetComponent<Rigidbody2D>();

		blinkingSprite = BlinkingSprite();
		IsInvincibility = false;
	}

	// 피격
	public override void Dealt(int damage, Vector3 attackPosition)
	{
		// 무적상태 확인
		if (!IsInvincibility)
		{
			StartCoroutine(Invincible());

			// 플레이어 넉백
			float shotWay = Mathf.Round(attackPosition.x - attackPosition.x);
			Vector2 shotVec2 = new Vector2(shotWay, 0.6f) * 4;

			rigidbody2D.velocity = Vector2.zero;
			rigidbody2D.AddForce(shotVec2, ForceMode2D.Impulse);

			// 대미지 계산
			base.Dealt(damage, attackPosition);
		}
	}

	// 스프라이트 깜빡거림 (!! 이거 나중에 플레이어 애니메이션 관련 스크립트로 이동 !!)
	private IEnumerator BlinkingSprite()
	{
		while (IsInvincibility)
		{
			yield return new WaitForSeconds(0.1f);

			charSprite.enabled = !charSprite.enabled;
		}

		charSprite.enabled = true;
	}

	// 무적 상태
	private IEnumerator Invincible()
	{
		StopCoroutine(blinkingSprite);

		IsInvincibility = true;
		gameObject.layer = 12;
		blinkingSprite = BlinkingSprite();

		StartCoroutine(blinkingSprite);
		yield return new WaitForSeconds(invincibleTime);

		gameObject.layer = 9;
		IsInvincibility = false;
	}
}
