using UnityEngine;

public class NormalMonsterMove : MonoBehaviour
{
	[SerializeField]
	private BoxCollider2D	frontCollider;								// 전면 콜라이더
	[SerializeField]
	private BoxCollider2D	bottomFrontCollider;                        // 밑면 앞쪽 콜라이더
	[SerializeField]
	private BoxCollider2D	bottomCollider;		                        // 밑면 콜라이더
	[SerializeField]
	private SpriteRenderer	sprite;										// 몬스터 sprite
	[SerializeField]
	private float			speed = 0.05f;								// 속도

	private Collider2D[]	colliders = new Collider2D[10];				// 충돌체 모음
	private ContactFilter2D contactFilter = new ContactFilter2D();      // Contact Filter

	public	int				directionSpeed = 1;							// 방향 속도

	// 초기화
	private void Awake()
	{
		contactFilter.SetLayerMask(1 << 8);
	}

	// 프레임 
	private void Update()
	{
		// 전면 콜라이더 체크
		if (0 < frontCollider.OverlapCollider(contactFilter, colliders))
		{
			Reverse();
		}

		// 밑면 콜라이더 체크
		if (0 >= bottomFrontCollider.OverlapCollider(contactFilter, colliders)
			&& 0 < bottomCollider.OverlapCollider(contactFilter, colliders))
		{
			Reverse();
		}

		// 이동
		transform.Translate(Vector3.right * directionSpeed * speed);
	}

	// 좌우 반전
	private void Reverse()
	{
		directionSpeed *= -1;

		Vector2 frontOffset = frontCollider.offset;
		Vector2 bottomOffset = bottomFrontCollider.offset;

		frontOffset.x *= -1;
		bottomOffset.x *= -1;

		frontCollider.offset = frontOffset;
		bottomFrontCollider.offset = bottomOffset;

		sprite.flipX = !sprite.flipX;
	}
}
