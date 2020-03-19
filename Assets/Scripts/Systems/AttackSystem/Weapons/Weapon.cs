using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public abstract class Weapon : MonoBehaviour
{

	private bool attackAxisInUse = false;		// 공격 키 사용 플래그


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
				Attack(Camera.main.ScreenToWorldPoint(Input.mousePosition));

				attackAxisInUse = true;
			}
		}

		if (Input.GetAxis("Attack") == 0)
		{
			attackAxisInUse = false;
		}
	}

	// 공격
	public abstract void Attack(Vector3 mousePosition);
}