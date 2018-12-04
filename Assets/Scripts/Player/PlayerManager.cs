using System.Collections;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
	public static PlayerManager instance;				// 싱글톤 인스턴스

	[SerializeField]
	private	GameObject		normalBlockItem;			// 드랍용 노말 블럭
	[SerializeField]
	private	SpriteRenderer	charSprite;					// 캐릭터 스프라이트
	[SerializeField]
	private	float			invincibleTime;				// 무적 시간

	private	bool isInvincibility;						// 무적 상태인지
	public bool	IsInvincibility							// 접근자
	{
		get { return isInvincibility; }
		private set { isInvincibility = value; }
	}


	// 초기화
	private void Awake()
	{
		IsInvincibility = false;

		if (instance == null)
		{
			instance = this;
		}
	}

	// 피격
	public void Hit(float shotWay)
	{
		if (!IsInvincibility)
		{
			DropNormalBlock(shotWay);

			StartCoroutine(Invincible());
		}
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

	// 무적 상태
	private IEnumerator Invincible()
	{
		IsInvincibility = true;
		gameObject.layer = 12;

		StartCoroutine(BlinkingSprite());
		yield return new WaitForSeconds(invincibleTime);

		gameObject.layer = 9;
		IsInvincibility = false;
	}

	// 스프라이트 깜빡거림
	private IEnumerator BlinkingSprite()
	{
		while (IsInvincibility)
		{
			yield return new WaitForSeconds(0.1f);

			charSprite.enabled = !charSprite.enabled;
		}

		charSprite.enabled = true;
	}
}
