using UnityEngine;
using System.Collections;

public abstract class Monster : Character
{
	public delegate IEnumerator		MonsterPattern();		// 몬스터 패턴 델리게이트
	public delegate void			FlagFunc(bool isTrue);	// 플래그 델리게이트


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
