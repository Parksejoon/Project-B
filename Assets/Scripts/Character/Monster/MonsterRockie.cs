using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterRockie : Monster
{
	private int		directionSpeed = 1;                 // 방향 속도

	// 충돌 필터
	private ContactFilter2D		moveContactFilter;      // 이동시 사용할 contactFilter

	// 플래그
	private bool		moveFlag = false;   // 이동 플래그


	// 초기화
	private void Awake()
	{
		// 필터의 레이어 마스크 설정(레이어 8번 map)
		moveContactFilter.SetLayerMask(1 << 8);

		// 스텟 초기화 ( 임시 )
		Statistics stats;

		stats.max_health_point = 5;
		stats.attack_damage = 1;
		stats.move_speed = 0.1f;
		stats.attack_speed = 1.0f;
		stats.abillity_power = 0;
		stats.defensive_power = 1.0f;

		SetStats(stats);

		// 패턴 초기화 ( 임시 )
		singlePattern = new[]
		{
			new MonsterPattern(Move)
		};


		Init();
	}

	// 시작
	private void Start()
	{
		StartCoroutine(RunPattern());
	}

	// 사망
	protected override void Dead()
	{
		Destroy(gameObject);

		Debug.Log("Rockie is dead.");
	}

	// 좌우 반전
	protected override void Reverse()
	{
		directionSpeed *= -1;

		base.Reverse();
	}

	// 몬스터 패턴 실행
	protected override IEnumerator RunPattern()
	{
		while (true)
		{
			yield return StartCoroutine(singlePattern[0]());
		}
	}

	// 이동 메인
	private IEnumerator Move()
	{
		StartCoroutine(FlagTimer(MoveFlagFunc, Random.Range(3f, 5f)));

		while (moveFlag)
		{
			Collider2D[] overlapColliderResult = new Collider2D[10];
			RaycastHit2D raycastHit2D;

			// 이동
			transform.Translate(Vector3.right * Stats.move_speed * directionSpeed);

			// 앞쪽 벽면 레이캐스트
			Debug.DrawRay(transform.position, transform.right * directionSpeed * 1f, Color.red);

			raycastHit2D = Physics2D.Raycast(transform.position, transform.right * directionSpeed, 1f, (1 << 8));

			if (raycastHit2D)
			{
				Reverse();
			}

			yield return null;
		}
		
		// 1초 딜레이
		yield return new WaitForSeconds(1f);
	}

	private void MoveFlagFunc(bool isTrue)
	{
		moveFlag = isTrue;
	}
}
