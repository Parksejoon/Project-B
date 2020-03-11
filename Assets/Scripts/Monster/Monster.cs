using UnityEngine;
using System.Collections;

// 스탯
public struct Statistics
{
	public int		health_point;			// 체력
	public int		attack_damage;			// 공격력
	public int		move_speed;				// 이동 속도
	public float	attack_speed;			// 공격 속도
	public int		abillity_power;			// 마력
	public float	defensive_power;		// 방어력
}

public abstract class Monster : MonoBehaviour
{
	protected Statistics		statistics;			// 스탯

	[SerializeField]
	protected SpriteRenderer	sprite;             // 몬스터 sprite

	[SerializeField]
	protected Collider2D[]		colliders;			// 충돌체 모음
	protected ContactFilter2D	contactFilter		// 수동 충돌 처리를 할때 필요한 Contact Filter
		= new ContactFilter2D();


	// 초기화
	protected void Init()
	{
		sprite = GetComponentInChildren<SpriteRenderer>();
	}

	// 스탯 초기화
	public void SetStatistics(Statistics stats)
	{
		statistics = stats;
	}

	// 대미지 받음
	public void Dealt(int damage)
	{
		statistics.health_point -= damage;

		if (statistics.health_point <= 0)
		{
			statistics.health_point = 0;
		}
	}

	// 좌우 반전
	protected void Reverse()
	{
		sprite.flipX = !sprite.flipX;
	}

	// 몬스터 패턴 실행
	protected abstract IEnumerator RunPattern();
}
