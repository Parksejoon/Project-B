using UnityEngine;
using System.Collections;

// 스탯
public struct Statistics
{
	public int		health_point;			// 체력
	public int		attack_damage;			// 공격력
	public float	move_speed;				// 이동 속도
	public float	attack_speed;			// 공격 속도
	public int		abillity_power;			// 마력
	public float	defensive_power;		// 방어력
}

public abstract class Monster : MonoBehaviour
{
	public delegate IEnumerator		MonsterPattern();		// 몬스터 패턴 델리게이트
	public delegate void			FlagFunc(bool isTrue);	// 플래그 델리게이트

	protected Statistics		statistics;			// 스탯

	[SerializeField]
	protected SpriteRenderer	sprite;             // 몬스터 sprite

	[SerializeField]
	protected Collider2D[]		colliders;          // 충돌체 모음

	protected MonsterPattern[]	singlePattern;		// 기본 반복 패턴 모음


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
	public virtual void Dealt(int damage)
	{
		statistics.health_point -= damage;

		if (statistics.health_point <= 0)
		{
			statistics.health_point = 0;
		}
	}

	// 사망
	protected virtual void Dead()
	{
		Debug.Log(gameObject.name + ")_ Monster dead.");
	}

	// 좌우 반전
	protected virtual void Reverse()
	{
		sprite.flipX = !sprite.flipX;
	}

	// 플래그 타이머
	protected IEnumerator FlagTimer(FlagFunc flagFunc, float time)
	{
		flagFunc(true);
		
		yield return new WaitForSeconds(time);

		flagFunc(false);
	}

	// 몬스터 패턴 실행
	protected abstract IEnumerator RunPattern();
}
