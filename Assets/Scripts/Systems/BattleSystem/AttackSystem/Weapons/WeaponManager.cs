using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class WeaponManager : MonoBehaviour
{
	[SerializeField]
	private Transform	playerTransform;	// 플레이어 오브젝트

	private GameObject	weaponPrefab;		// 무기 프리팹
	private GameObject	currentWeapon;      // 현재 무기


	// 시작
	public void Start()
	{
		if (SceneManager.GetActiveScene().buildIndex == (int)SceneNumber.BattleScene)
		{
			weaponPrefab = DataCache.GetData("CurrentWeapon") as GameObject;
			CreateWeapon();
		}
	}

	// 무기 장착
	public void SetWeapon(GameObject weapon)
	{
		weaponPrefab = weapon;
		DataCache.SaveData("CurrentWeapon", weapon);
	}

	// 무기 삭제
	public void DeleteWeapon()
	{
		Destroy(currentWeapon);
	}

	// 무기 생성
	public void CreateWeapon()
	{
		if (weaponPrefab == null) return;

		currentWeapon = Instantiate(weaponPrefab, Vector3.zero, Quaternion.identity, playerTransform);
		currentWeapon.transform.localPosition = new Vector3(0, 0, (int)RenderPriority.Weapon);
	}
}
