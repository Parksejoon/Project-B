using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterRockie : Monster
{

	//[SerializeField]
	//private BoxCollider2D	frontCollider;								// 전면 콜라이더
	//[SerializeField]
	//private BoxCollider2D	bottomFrontCollider;                        // 밑면 앞쪽 콜라이더
	//[SerializeField]
	//private BoxCollider2D	bottomCollider;		                        // 밑면 콜라이더

	//public	int				directionSpeed = 1;							// 방향 속도


	// 초기화
	private void Awake()
	{
		// 필터의 레이어 마스크 설정(레이어 8번 map)
		contactFilter.SetLayerMask(1 << 8);

		Statistics stats;

		stats.health_point = 100;
		stats.attack_damage = 1;
		stats.move_speed = 1;
		stats.attack_speed = 1.0f;
		stats.abillity_power = 0;
		stats.defensive_power = 1.0f;

		SetStatistics(stats);

		Init();
	}

	// 시작
	private void Start()
	{
		StartCoroutine(RunPattern());
	}

	// 몬스터 패턴 실행
	protected override IEnumerator RunPattern()
	{
		yield return StartCoroutine(Move());
	}

	// 이동
	private IEnumerator Move()
	{
		// 이동
		while (true)
		{
			transform.Translate(Vector3.right * statistics.move_speed * 0.1f);

			yield return null;
		}
	}
}
