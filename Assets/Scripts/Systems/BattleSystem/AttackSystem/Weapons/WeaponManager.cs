using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class WeaponManager : MonoBehaviour
{
	[SerializeField]
	private Transform	playerTransform;    // 플레이어 오브젝트
	[SerializeField]
	private ItemUI		weaponItemUI;		// 무기 아이템 슬롯
	
	private GameObject	currentWeapon;      // 현재 장착중인 실체화된 무기

	private const string dataCacheEquippedWeaponKey = "EquippedWeapon";


	// 초기화 
	private void Awake()
	{
		// 데이터 셋
		weaponItemUI.SetItemCode(DataCache<int>.GetData(dataCacheEquippedWeaponKey));

		// 슬롯 초기화
		weaponItemUI.Init();
	}

	// 시작
	public void Start()
	{
		// 배틀 씬이면
		if (SceneManager.GetActiveScene().buildIndex == (int)SceneNumber.BattleScene)
		{
			CreateWeapon(weaponItemUI.GetPrefab());
		}
	}
	
	// 오브젝트 비활성화 시
	private void OnDisable()
	{
		SaveData();
	}

	// 무기 데이터 저장
	private void SaveData()
	{
		DataCache<int>.SaveData(dataCacheEquippedWeaponKey, weaponItemUI.GetItemCode());
	}

	// 무기 생성
	public void CreateWeapon(GameObject weaponPrefab)
	{
		if (weaponPrefab == null) return;

		currentWeapon = Instantiate(weaponPrefab, Vector3.zero, Quaternion.identity, playerTransform);
		currentWeapon.transform.localPosition = new Vector3(0, 0, (int)RenderPriority.Weapon);
	}
}
