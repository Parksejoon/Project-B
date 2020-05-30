using UnityEngine;
using System.Collections;

public abstract class Monster : Character
{
	public delegate IEnumerator		MonsterPattern();		// 몬스터 패턴 델리게이트
	public delegate void			FlagFunc(bool isTrue);	// 플래그 델리게이트

	
	protected SpriteRenderer	sprite;                 // 몬스터 sprite
	protected ItemDropper		itemDropper;			// 아이템 드로퍼

	[SerializeField]
	protected Collider2D[]		colliders;				// 충돌체 모음

	protected MonsterPattern[]	singlePattern;			// 기본 반복 패턴 모음

	protected bool				linkHpGauge = false;	// 체력바를 링킹 할 것인지


	// 초기화
	protected void Init()
	{
		if (sprite == null) sprite = GetComponentInChildren<SpriteRenderer>();
		if (itemDropper == null) itemDropper = GetComponent<ItemDropper>();

		AttackCore attackCore = GetComponentInChildren<AttackCore>();
		if (attackCore != null)
		{
			attackCore.SetAttack(Stats.attack_damage, "Player");
		}
	}

	// 피격
	public override void Dealt(int damage, Vector3 attackPosition)
	{
		// 대미지 계산
		base.Dealt(damage, attackPosition);

		if (linkHpGauge) SyncHpGauge();

		StartCoroutine(DealtColorAnimation());
	}

	// 현재 존재하는 HP게이지와 동기화
	public void SyncHpGauge()
	{
		HPGauge.instance.SetHpSlider((float)HealthPoint / Stats.max_health_point);
	}
	
	// 아이템 드랍
	protected void DropItem()
	{
		itemDropper.DropRandomItem();
	}

	// 좌우 반전
	protected virtual void Reverse()
	{
		sprite.flipX = !sprite.flipX;
	}

	// 사망
	protected override void Dead()
	{
		BattleManager.instance.OpenPortal();
		DropItem();
		Destroy(gameObject);
	}

	// 플래그 타이머
	protected IEnumerator FlagTimer(FlagFunc flagFunc, float time)
	{
		flagFunc(true);
		
		yield return new WaitForSeconds(time);

		flagFunc(false);
	}

	// 피격시 색변경 코루틴
	protected IEnumerator DealtColorAnimation()
	{
		sprite.color = Color.red;

		yield return new WaitForSeconds(0.1f);

		sprite.color = Color.white;
	}

	// 몬스터 패턴 실행
	protected abstract IEnumerator RunPattern();
}
