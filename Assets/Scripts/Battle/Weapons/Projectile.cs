using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
	private float		speed;			// 속도
	private Vector2		shotWay;        // 방향

	private float		lifeTime;       // 투사체 생존 시간
	private string		objectName;		// 오브젝트 풀에서 사용되는 오브젝트 이름


	// 프레임
	private void Update()
	{
		transform.Translate(shotWay * speed * Time.deltaTime);
	}
	
	/// <summary>
	/// Initialize variables.
	/// </summary>
	/// <param name="_speed">speed of projectile.</param>
	/// <param name="_shotWay">direction of projectile.</param>
	/// <param name="_lifeTime">life time of projectile.</param>
	/// <param name="_objectName">object's name in object pool</param>
	public void Init(float _speed, Vector2 _shotWay, float _lifeTime, string _objectName)
	{
		speed = _speed;
		shotWay = _shotWay.normalized;
		lifeTime = _lifeTime;
		objectName = _objectName;

		StartCoroutine(ReleaseTimer());
	}

	// 일정 시간 후 오브젝트가 릴리즈되는 코루틴
	private IEnumerator ReleaseTimer()
	{
		yield return new WaitForSeconds(lifeTime);

		ObjectPoolManager.Release(objectName, gameObject);
	}
}
