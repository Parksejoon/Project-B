using UnityEngine;

public abstract class Monster : MonoBehaviour
{
	[SerializeField]
	protected SpriteRenderer	sprite;                                     // 몬스터 sprite

	protected Collider2D[]	colliders = new Collider2D[10];					// 충돌체 모음
	protected ContactFilter2D contactFilter = new ContactFilter2D();		// Contact Filter


	// 초기화
	private void Awake()
	{
		contactFilter.SetLayerMask(1 << 8);
	}

	// 프레임 
	private void Update()
	{
		RunPattern();
	}

	// 좌우 반전
	private void Reverse()
	{
		sprite.flipX = !sprite.flipX;
	}

	// 몬스터 패턴 실행
	public abstract void RunPattern();
}
