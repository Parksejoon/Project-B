using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttackManager : MonoBehaviour
{
	[SerializeField]
	private GameObject attackCore;

	private bool attackAxisInUse = false;
	

	// 프레임
	private void Update()
	{
		if (Input.GetAxis("Attack") != 0)
		{
			if (attackAxisInUse == false)
			{
				Attack();

				attackAxisInUse = true;
			}
		}

		if (Input.GetAxis("Attack") == 0)
		{
			attackAxisInUse = false;
		}
	}

	// 공격
	private void Attack()
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
