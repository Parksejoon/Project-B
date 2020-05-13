using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponUI : ItemUI
{
	[SerializeField]
	private WeaponManager weaponManager;        // 무기 매니저

	
	// refresh
	protected override void Refresh()
	{
		weaponManager.SetWeapon(itemData.prefab);

		base.Refresh();
	}
}
