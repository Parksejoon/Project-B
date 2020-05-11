﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterSpawnSystem : MonoBehaviour
{
	public GameObject	targetMonsterPrefab;		// 소환될 몬스터의 프리팹

	[SerializeField]
	private Transform	objectsTransform;           // 오브젝트 집합의 transform


	// 프레임
	private void Update()
	{
		if (Input.GetKeyDown(KeyCode.I))
		{
			SpawnMonster();
		}
	}

	// 몬스터 스폰
	public void SpawnMonster()
	{
		Instantiate(targetMonsterPrefab, Vector3.zero, Quaternion.identity, objectsTransform);
	}
}