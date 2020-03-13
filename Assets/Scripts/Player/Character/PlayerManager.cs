using System.Collections;
using UnityEngine;

// 플레이어 매니저
// 스테이터스 관리, 피격, 타격
public class PlayerManager : MonoBehaviour
{
	// (@@ 여기서 싱글톤 사용 금지 @@)

	private bool isInvincibility;						// 무적 상태인지
	public	bool IsInvincibility						// 접근자
	{
		get { return isInvincibility; }
		private set { isInvincibility = value; }
	}

	[SerializeField]
	private	GameObject		normalBlockItem;			// 드랍용 노말 블럭
	[SerializeField]
	private	SpriteRenderer	charSprite;					// 캐릭터 스프라이트
	[SerializeField]
	private	float			invincibleTime;             // 무적 시간

	private IEnumerator		blinkingSprite;				// 스프라이트 깜빡거림 코루틴


	// 초기화
	private void Awake()
	{
		blinkingSprite = BlinkingSprite();
		IsInvincibility = false;
	}

	// 노말 블럭 드랍
	private void DropNormalBlock(float shotWay)
	{
		if (InventoryManager.instance.UseItem(1) != null)
		{
			GameObject target = Instantiate(normalBlockItem, transform.position, Quaternion.identity);

			target.GetComponent<Rigidbody2D>().AddForce(new Vector2(shotWay * 2, 1.5f) * 5, ForceMode2D.Impulse);
		}
	}

	// 피격
	public void Dealt(float shotWay)
	{
		if (!IsInvincibility)
		{
			//DropNormalBlock(shotWay);

			StartCoroutine(Invincible());
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
