using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponUI : ItemUI
{
	[SerializeField]
	private WeaponManager weaponManager;        // 무기 매니저


	//// 초기화
	//private void Awake()
	//{
	//	Init();
	//}

	// refresh
	protected override void Refresh()
	{
		weaponManager.DeleteWeapon();

		if (itemData.code != 0)
		{
			weaponManager.SetWeapon(itemData.prefab);
			weaponManager.CreateWeapon();
		}

		base.Refresh();
	}
}
