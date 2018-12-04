using UnityEngine;

public class MonsterMove : MonoBehaviour
{
	// 인스펙터 노출 변수
	// 일반
	[SerializeField]
	private BoxCollider2D	topCollider;					// 윗면 콜라이더
	[SerializeField]
	private BoxCollider2D	frontCollider;                  // 전면 콜라이더
	[SerializeField]
	private BoxCollider2D	bottomCollider;					// 밑면 콜라이더

	// 수치
	[SerializeField]
	private float			speed = 0.05f;					// 속도

	public int				directionSpeed = 1;				// 방향 속도

	// 인스펙터 비노출 변수
	// 일반
	private Collider2D[]	colliders = new Collider2D[10];				// 충돌체 모음
	private ContactFilter2D mapContactFilter = new ContactFilter2D();   // Map Contact Filter
	private ContactFilter2D playerContactFilter = new ContactFilter2D();// Player Contact Filter

	// 수치

	// 초기화
	private void Awake()
	{
		mapContactFilter.SetLayerMask(1 << 8);
		playerContactFilter.SetLayerMask(1 << 9);
	}

	// 픽스 프레임 
	private void Update()
	{
		// 윗면 콜라이더 체크
		if (0 < topCollider.OverlapCollider(playerContactFilter, colliders))
		{
			Death();
		}

		// 전면 콜라이더 체크 및 밑면 콜라이더 체크
		if (0 < frontCollider.OverlapCollider(mapContactFilter, colliders)
			|| 0 >= bottomCollider.OverlapCollider(mapContactFilter, colliders))
		{
			Reverse();
		}

		// 이동
		transform.Translate(Vector3.right * directionSpeed * speed);
	}

	// 충돌체 진입
	private void OnCollisionEnter2D(Collision2D collision)
	{
		if (collision.gameObject.CompareTag("Player"))
		{
			// 플레이어 넉백
			Rigidbody2D playerRigidbody = collision.rigidbody;

			float shotWay = Mathf.Round(playerRigidbody.position.x - transform.position.x);
			Vector2 shotVec2 = new Vector2(shotWay, 0.6f) * 4;

			playerRigidbody.velocity = Vector2.zero;
			playerRigidbody.AddForce(shotVec2, ForceMode2D.Impulse);

			// 플레이어 피격
			PlayerManager.instance.Hit(shotWay);
		}
	}

	// 좌우 반전
	private void Reverse()
	{
		directionSpeed *= -1;

		Vector2 frontOffset = frontCollider.offset;
		Vector2 bottomOffset = bottomCollider.offset;

		frontOffset.x *= -1;
		bottomOffset.x *= -1;

		frontCollider.offset = frontOffset;
		bottomCollider.offset = bottomOffset;
	}

	// 죽음 
	private void Death()
	{
		Rigidbody2D playerRigidbody = PlayerController.playerTransform.GetComponent<Rigidbody2D>();

		playerRigidbody.velocity = Vector2.zero;
		playerRigidbody.AddForce(Vector2.up * 5, ForceMode2D.Impulse);

		//Destroy(gameObject);
	}
}
