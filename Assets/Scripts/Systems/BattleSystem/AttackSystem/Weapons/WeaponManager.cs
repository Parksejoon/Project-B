using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class WeaponManager : MonoBehaviour
{
	[SerializeField]
	private Transform	playerTransform;    // 플레이어 오브젝트
	[SerializeField]
	private ItemSlot	weaponItemUI;		// 무기 아이템 슬롯
	
	private GameObject	equippedWeapon;     // 현재 장착중인 실체화된 무기

	private const string dataKeyOfEquippedWeapon = "EquippedWeapon";


	// 초기화 
	private void Awake()
	{
		var itemCode = DataCache<int>.GetData(dataKeyOfEquippedWeapon);

		// 데이터 셋
		weaponItemUI.SetItem(ItemParser.GetItemByCode(itemCode));
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
		DataCache<int>.SaveData(dataKeyOfEquippedWeapon, weaponItemUI.GetItemCode());
	}

	// 무기 생성
	public void CreateWeapon(GameObject weaponPrefab)
	{
		if (weaponPrefab == null)
		{
			return;
		}

		equippedWeapon = Instantiate(weaponPrefab, Vector3.zero, Quaternion.identity, playerTransform);
		equippedWeapon.transform.localPosition = new Vector3(0, 0, (int)RenderPriority.Weapon);
	}
}
