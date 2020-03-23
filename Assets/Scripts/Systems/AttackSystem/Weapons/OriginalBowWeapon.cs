using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OriginalBowWeapon : Weapon
{
	[SerializeField]
	private GameObject	ammo;               // 탄
	[SerializeField]
	private Transform	ammoParent;			// 탄 생성시 부모
	public	float		ammoSpeed = 20f;	// 탄속


	// 공격
	public override void Attack(Vector2 currentPosition, Vector2 mousePosition)
	{
		GameObject newAmmo = Instantiate(ammo, transform.position, Quaternion.identity);
		
		newAmmo.GetComponent<Ammo>().SetVector(ammoSpeed, mousePosition - currentPosition);
	}

}
