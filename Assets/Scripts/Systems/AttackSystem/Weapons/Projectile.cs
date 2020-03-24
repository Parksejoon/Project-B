using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
	private float		speed;			// 속도
	private Vector2		shotWay;		// 방향


	// 프레임
	private void Update()
	{
		transform.Translate(shotWay * speed * Time.deltaTime);
	}

	// 방향과 속도 초기화 함수
	public void SetVector(float _speed, Vector2 way)
	{
		speed = _speed;
		shotWay = way.normalized;
	}

}
