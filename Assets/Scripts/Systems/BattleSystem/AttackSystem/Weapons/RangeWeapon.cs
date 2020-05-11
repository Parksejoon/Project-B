using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangeWeapon : Weapon
{
	[SerializeField]
	private GameObject	projectile;					// 발사체
	[SerializeField]
	private Transform	projectileParent;			// 발사체 생성시 부모
	public	float		projectileSpeed = 20f;      // 발사체 속도

	[SerializeField]
	private string		targetTag = "Monster";      // 타겟 태그


	// 시작
	private void Start()
	{
		ObjectPoolManager.Create("RangeWeapon_projectile", projectile, 50, null);
	}

	// 공격
	public override void Attack(Vector2 currentPosition, Vector2 mousePosition)
	{
		GameObject newProjectile = ObjectPoolManager.GetGameObject("RangeWeapon_projectile", transform.position);

		if (newProjectile == null)
		{
			Debug.Log("No projectile exist.");
			return;
		}

		newProjectile.GetComponent<AttackCore>().SetAttack(playerManager.Stats.attack_damage, targetTag);
		newProjectile.GetComponent<Projectile>().Init(projectileSpeed, mousePosition - currentPosition, 1f, "RangeWeapon_projectile");
	}
}
