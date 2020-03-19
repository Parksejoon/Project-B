using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 스탯
public struct Statistics
{
	public int		health_point;       // 체력
	public int		attack_damage;      // 공격력
	public float	move_speed;         // 이동 속도
	public float	attack_speed;       // 공격 속도
	public int		abillity_power;     // 마력
	public float	defensive_power;    // 방어력
}

public class Character : MonoBehaviour
{
	private Statistics	stats;            // 스탯
	public Statistics Stats
	{
		get { return stats; }
		private set { stats = value; }
	}


	// 대미지 받음
	public virtual void Dealt(int damage, Vector3 attackPosition)
	{
		stats.health_point -= damage;

		if (stats.health_point <= 0)
		{
			stats.health_point = 0;
			Dead();
		}
	}

	// 사망
	protected virtual void Dead()
	{
		Debug.Log("<" + gameObject.name + "> is dead.");
	}

	// 스탯 초기화
	public void SetStats(Statistics _stats)
	{
		stats = _stats;
	}

	// 스탯 추가
	public void AddStats(Statistics stats)
	{
		stats.health_point += stats.health_point;
		stats.attack_damage += stats.attack_damage;
		stats.move_speed += stats.move_speed;
		stats.attack_speed += stats.attack_speed;
		stats.abillity_power += stats.abillity_power;
		stats.defensive_power += stats.defensive_power;
	}
}
