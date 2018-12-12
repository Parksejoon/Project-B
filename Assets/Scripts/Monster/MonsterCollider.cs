using UnityEngine;

public class MonsterCollider : MonoBehaviour
{
	[SerializeField]
	private BoxCollider2D	topCollider;								// 윗면 콜라이더

	private Collider2D[] colliders = new Collider2D[10];                // 충돌체 모음
	private ContactFilter2D contactFilter = new ContactFilter2D();		// Contact Filter


	// 초기화
	private void Awake()
	{
		contactFilter.SetLayerMask((1 << 9) | (1 << 12));
	}

	// 프레임 
	private void Update()
	{
		// 윗면 콜라이더 체크
		if (0 < topCollider.OverlapCollider(contactFilter, colliders))
		{
			StepOn();
		}
	}

	// 충돌체 진입
	private void OnTriggerEnter2D(Collider2D collision)
	{
		if (collision.CompareTag("Player"))
		{
			// 플레이어 넉백
			Rigidbody2D playerRigidbody = collision.GetComponent<Rigidbody2D>();

			float shotWay = Mathf.Round(playerRigidbody.position.x - transform.position.x);
			Vector2 shotVec2 = new Vector2(shotWay, 0.6f) * 4;

			playerRigidbody.velocity = Vector2.zero;
			playerRigidbody.AddForce(shotVec2, ForceMode2D.Impulse);

			// 플레이어 피격
			PlayerManager.instance.Hit(shotWay);
		}
	}

	// 밟힘
	private void StepOn()
	{
		Rigidbody2D playerRigidbody = PlayerController.playerTransform.GetComponent<Rigidbody2D>();

		playerRigidbody.velocity = Vector2.zero;
		playerRigidbody.AddForce(Vector2.up * 5, ForceMode2D.Impulse);

		Death();
	}

	// 죽음
	public void Death()
	{
		Debug.Log("MOOO!");
		//Destroy(gameObject);
	}
}
