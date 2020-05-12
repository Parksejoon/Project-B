using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeWeapon : Weapon
{
	[SerializeField]
	private Transform	slashTransform;		// 검격 transform
	private GameObject	attackCore;			// 공격 코어
	private Animator	attackAnimator;		// 공격 애니메이터

	[SerializeField]
	private string targetTag = "Monster";   // 타겟 태그


	// 초기화
	protected override void Awake()
	{
		base.Awake();

		attackCore = slashTransform.Find("AttackCore").gameObject;
		attackAnimator = slashTransform.GetComponentInChildren<Animator>();

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
		float angle = Mathf.Atan2(mousePosition.y - currentPosition.y, mousePosition.x - currentPosition.x);
		slashTransform.eulerAngles = new Vector3(0, 0, (angle * (180f / Mathf.PI)) - 90f);

		StartCoroutine(CloseAttack());
	}

	// 근접공격
	private IEnumerator CloseAttack()
	{
		attackCore.SetActive(true);
		attackAnimator.SetTrigger("Attack");

		yield return new WaitForSeconds(0.1f);

		attackCore.SetActive(false);
	}
}
