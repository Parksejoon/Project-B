using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public abstract class Weapon : MonoBehaviour
{
	private bool attackAxisInUse = false;			// 공격 키 사용 플래그

	protected PlayerManager playerManager;			// 플레이어 매니저


	// 초기화
	protected virtual void Awake()
	{
		GetPlayerManagerFromParent();
	}

	// 업데이트
	private void Update()
	{
		CheckKey();
	}

	// 입력 확인
	private void CheckKey()
	{
		if (Input.GetAxis("Attack") != 0)
		{
			if (attackAxisInUse == false)
			{
				Attack(transform.position, Camera.main.ScreenToWorldPoint(Input.mousePosition));

				attackAxisInUse = true;
			}
		}

		if (Input.GetAxis("Attack") == 0)
		{
			attackAxisInUse = false;
		}
	}

	// player manager 초기화
	private void GetPlayerManagerFromParent()
	{
		playerManager = GetComponentInParent<PlayerManager>();
	}

	// 공격
	public abstract void Attack(Vector2 currentPosition, Vector2 mousePosition);
}