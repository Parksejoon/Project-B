using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponManager : MonoBehaviour
{
	private GameObject	weaponPrefab;		// 무기 프리팹
	private GameObject	currentWeapon;      // 현재 무기


	// 시작
	public void Start()
	{
		
	}

	// 무기 장착
	public void SetWeapon(GameObject weapon)
	{
		weaponPrefab = weapon;
	}

	// 무기 삭제
	public void DeleteWeapon()
	{
		Destroy(currentWeapon);
	}

	// 무기 생성
	public void CreateWeapon()
	{
		currentWeapon = Instantiate(weaponPrefab, Vector3.zero, Quaternion.identity, transform);
		currentWeapon.transform.localPosition = new Vector3(0, 0, (int)RenderPriority.Weapon);
	}
}
