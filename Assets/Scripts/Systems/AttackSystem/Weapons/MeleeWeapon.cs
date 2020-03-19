using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeWeapon : Weapon
{
	[SerializeField]
	private GameObject attackCore;


	// 공격
	public override void Attack(Vector3 mousePosition)
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
