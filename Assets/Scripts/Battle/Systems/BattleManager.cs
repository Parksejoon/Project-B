using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleManager : MonoBehaviour
{
	public static BattleManager instance;				// 싱글톤

	public GameObject		targetMonsterPrefab;		// 소환될 몬스터의 프리팹

	[SerializeField]
	private Transform		objectsTransform;           // 오브젝트 집합의 transform
	[SerializeField]
	private PortalSpawner	portalSpawner;              // 포탈 스포너


	// 초기화
	private void Awake()
	{
		if (instance == null)
		{
			instance = this;
		}
		else
		{
			Destroy(this);
		}
	}

	// 프레임
	private void Update()
	{
		// (!! 임시로 몬스터 생성 !!)
		if (Input.GetKeyDown(KeyCode.Y))
		{
			SpawnMonster();
		}
	}

	// 몬스터 스폰
	public void SpawnMonster()
	{
		Instantiate(targetMonsterPrefab, Vector3.zero, Quaternion.identity, objectsTransform);
	}

	// 몬스터 사망
	public void OpenPortal()
	{
		portalSpawner.SpawnPortal(Vector3.zero);
	}
}
