using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleManager : MonoBehaviour
{
	private WeaponManager weaponManager;    // 무기 매니저


	// 초기화
	private void Awake()
	{
		weaponManager = GetComponent<WeaponManager>();
	}

	// 시작
	private void Start()
	{
		weaponManager.CreateWeapon();
	}
}
