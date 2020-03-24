﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeWeapon : Weapon
{
	[SerializeField]
	private GameObject attackCore;

	[SerializeField]
	private string targetTag = "Monster";       // 타겟 태그


	// 초기화
	protected override void Awake()
	{
		base.Awake();

		attackCore.GetComponent<AttackCore>().SetAttack(playerManager.Stats.attack_damage, targetTag);
	}

	// 시작
	private void Start()
	{
		// 어택코어 비활성화
		attackCore.SetActive(false);
	}

	// 공격
	public override void Attack(Vector2 currentPosition, Vector2 mousePosition)
	{
		StartCoroutine(CloseAttack());
	}

	// 근접공격
	private IEnumerator CloseAttack()
	{
		attackCore.SetActive(true);

		yield return new WaitForSeconds(0.1f);

		attackCore.SetActive(false);
	}
}
