using UnityEngine;

public class MonsterMove : MonoBehaviour
{
	// 인스펙터 노출 변수
	// 일반
	[SerializeField]
	private BoxCollider2D	frontCollider;                          // 전면 콜라이더
	[SerializeField]
	private BoxCollider2D	bottomCollider;							// 밑면 콜라이더

	// 수치
	[SerializeField]
	private float			speed = 0.05f;							// 속도

	public int				directionSpeed = 1;						// 방향 속도

	// 인스펙터 비노출 변수
	// 일반
	private Collider2D[]	collider2ds = new Collider2D[10];       // 충돌체 모음
	private ContactFilter2D contactFilter2D = new ContactFilter2D();// Contact필터

	// 수치

	// 초기화
	private void Awake()
	{
		contactFilter2D.SetLayerMask(1<<8);
	}

	// 픽스 프레임 
	private void Update()
	{
		// 전면 콜라이더 체크 및 밑면 콜라이더 체크
		if (0 < frontCollider.OverlapCollider(contactFilter2D, collider2ds)
			|| 0 >= bottomCollider.OverlapCollider(contactFilter2D, collider2ds))
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
		Vector2 bottomOffset = bottomCollider.offset;

		frontOffset.x *= -1;
		bottomOffset.x *= -1;

		frontCollider.offset = frontOffset;
		bottomCollider.offset = bottomOffset;
	}
}
