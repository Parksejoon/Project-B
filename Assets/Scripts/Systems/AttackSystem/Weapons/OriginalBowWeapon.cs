using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OriginalBowWeapon : Weapon
{
	[SerializeField]
	private GameObject	ammo;				// 탄

	public	float		ammoSpeed = 1;		// 탄속


	// 공격
	public override void Attack(Vector3 mousePosition)
	{
		GameObject newAmmo = Instantiate(ammo, transform.position, Quaternion.identity, transform);

		newAmmo.GetComponent<Rigidbody2D>().AddForce(mousePosition.normalized * ammoSpeed);
	}

}
